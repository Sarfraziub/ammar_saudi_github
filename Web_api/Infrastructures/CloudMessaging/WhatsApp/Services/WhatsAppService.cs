using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using Domain.Model.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Infrastructures.CloudMessaging.WhatsApp.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IServiceScopeFactory _scopeFactory;

        public WhatsAppService(
            IApplicationDbContext context, 
            IMediator mediator, 
            IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _mediator = mediator;
            _scopeFactory = scopeFactory;
        }

        public async Task<string> SendMessageAsync(SMSMessageDto messageDto)
        {
            var baseAddress = "https://w.smbotplus.com/api/";
            var client = new RestClient(baseAddress);

            try
            {
                var settings = await GetSettings();
                var firstTwoCharacters = messageDto.To.Substring(0, 2);
                if (firstTwoCharacters == "00")
                {
                    messageDto.To = messageDto.To.Remove(0, 2);
                }


                var json = JsonConvert.SerializeObject(settings);
                var whatsAppSettings = JsonConvert.DeserializeObject<WhatsAppSettings>(json);

                var request = new RestRequest("send");
                request.AddBody(new
                {
                    number = messageDto.To,
                    type = messageDto.Type,
                    message = messageDto.Content,
                    media_url = messageDto.FileUrl,
                    instance_id = whatsAppSettings.InstanceId,
                    access_token = whatsAppSettings.AccessToken
                });

                var response = await client.ExecutePostAsync(request);
                return response.Content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> SendMessageAsync(List<SMSMessageDto> messageDtos)
        {
            var baseAddress = "https://w.smbotplus.com/api/";
            
            try
            {
                var settings = await GetSettings();

                var json = JsonConvert.SerializeObject(settings);
                var whatsAppSettings = JsonConvert.DeserializeObject<WhatsAppSettings>(json);

                var client = new RestClient(baseAddress);


                foreach (var messageDto in messageDtos)
                {
                    var firstTwoCharacters = messageDto.To.Substring(0, 2);
                    if (firstTwoCharacters == "00")
                    {
                        messageDto.To = messageDto.To.Remove(0, 2);
                    }
                    var request = new RestRequest("send");
                    request.AddBody(new
                    {
                        number = messageDto.To,
                        type = "text",
                        message = messageDto.Content,
                        instance_id = whatsAppSettings.InstanceId,
                        access_token = whatsAppSettings.AccessToken
                    });
                    var response =  await Send(settings, client, request);
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return string.Empty;
        }







        public async Task<string> SendMessageAsync(Dictionary<string, string> settings, SMSMessageDto messageDto)
        {

            await Task.Run(async () =>
            {
                var baseAddress = "https://w.smbotplus.com/api/";
                var keys = new List<string>
                {
                    "WhatsAppSettings.InstanceId",
                    "WhatsAppSettings.AccessToken"
                };

                try
                {
                    String firstTwoCharacters = messageDto.To.Substring(0, 2);
                    if (firstTwoCharacters == "00")
                    {
                        messageDto.To = messageDto.To.Remove(0, 2);
                    }

                    string json = JsonConvert.SerializeObject(settings);
                    var whatsAppSettings = JsonConvert.DeserializeObject<WhatsAppSettings>(json);

                    var client = new RestClient(baseAddress);
                    var request = new RestRequest("send");
                    request.AddBody(new
                    {
                        number = messageDto.To,
                        type = "text",
                        message = messageDto.Content,
                        instance_id = whatsAppSettings.InstanceId,
                        access_token = whatsAppSettings.AccessToken
                    });
                    var response = await client.ExecutePostAsync(request);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }).ConfigureAwait(false);
            return string.Empty;
        }

        public async Task<string> SendMessageAsync(Dictionary<string, string> settings, List<SMSMessageDto> messageDtos)
        {

            await Task.Run(async () =>
            {
                var baseAddress = "https://w.smbotplus.com/api/";

                string json = JsonConvert.SerializeObject(settings);
                var whatsAppSettings = JsonConvert.DeserializeObject<WhatsAppSettings>(json);

                var client = new RestClient(baseAddress);


                foreach (var messageDto in messageDtos)
                {
                    String firstTwoCharacters = messageDto.To.Substring(0, 2);
                    if (firstTwoCharacters == "00")
                    {
                        messageDto.To = messageDto.To.Remove(0, 2);
                    }
                    var request = new RestRequest("send");
                    request.AddBody(new
                    {
                        number = messageDto.To,
                        type = "text",
                        message = messageDto.Content,
                        instance_id = whatsAppSettings.InstanceId,
                        access_token = whatsAppSettings.AccessToken
                    });
                    await Send(settings, client, request);
                }
            });

            return string.Empty;
        }





        private async Task<string> Send(Dictionary<string, string> settings,RestClient client, RestRequest request)
        {
            var response = await client.ExecutePostAsync(request);
            return response.Content;
        }

        private async Task<Dictionary<string, string>> GetSettings()
        {
            var keys = new List<string>
            {
                "WhatsAppSettings.InstanceId",
                "WhatsAppSettings.AccessToken"
            };
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var settings = (await dbContext.Settings.Where(x => keys.Contains(x.Key)).ToListAsync())
                .ToDictionary(x => x.Key.Split("WhatsAppSettings.")[1], x => x.Value);

            return settings;
        }

        public string GetEmailBody<T>(T emailBodyModel, string body)
        {
            var dict = emailBodyModel.GetType().GetProperties().ToDictionary(property => property.Name,
                property => property.GetValue(emailBodyModel));

            foreach (var key in dict.Keys)
            {
                body = body.Replace($"<<{key}>>", (string?)dict[key]);
            };

            return body;
        }
    }
}
