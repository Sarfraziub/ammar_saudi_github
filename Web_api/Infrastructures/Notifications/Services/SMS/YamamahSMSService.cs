using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Notifications.Models;
using Infrastructures.Notifications.Configurations;
using Microsoft.Extensions.Options;
using IO.ClickSend.ClickSend.Model;
using Newtonsoft.Json;

namespace Infrastructures.Notifications.Services.SMS;

public class YamamahSMSService : ISMSService
{
	private readonly YamamahSMSConfigurations _config;
	private readonly SmsSetting2 _config2;

	public YamamahSMSService(
        IOptions<YamamahSMSConfigurations> config, 
        IOptions<SmsSetting2> config2)
    {
        _config2 = config2.Value;
        _config = config.Value;
    }

	public async Task<string> SendSmsAsync(SMSMessageDto messageDto)
    {
        var acceptedMobileCodes = _config.AcceptedMobileCodes;

        //if (acceptedMobileCodes.Contains(messageDto.To.Substring(0, 3).Trim(new char[]{'+'})))
        //{
        //    return SendMessageFromLocal(messageDto);
        //}
        //return await SendSmsFromGlobal(messageDto);
        return SendMessageFromLocal(messageDto);
    }

    private string SendMessageFromLocal(SMSMessageDto messageDto)
    {
        Taqnyat.Taqnyat taqnyt = new Taqnyat.Taqnyat();
        string bearer = "038402b4de4d497ad629708bfa416619";
        string body = messageDto.Content;
        string recipients = messageDto.To;
        string sender = "QWaterAp";

        var message = taqnyt.SendMessage(bearer, recipients, sender, body);
        return message;
    }

    private async Task<string> SendSmsFromGlobal(SMSMessageDto messageDto)
    {
        var listOfSms = new List<SmsMessage>
        {
            new SmsMessage(
                to: messageDto.To,
                body: messageDto.Content,
                source: "sdk"
            )
        };
        var smsCollection = new SmsMessageCollection(listOfSms);

        using var client = new HttpClient();

        client.BaseAddress = new Uri(_config2.BaseUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var plainTextBytes = Encoding.UTF8.GetBytes($"{_config2.Username}:{_config2.Password}");
        string val = Convert.ToBase64String(plainTextBytes);

        client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);

        HttpResponseMessage response = await client.PostAsync($"sms/send", new StringContent(JsonConvert.SerializeObject(smsCollection), Encoding.UTF8, "application/json"));
        if (response.IsSuccessStatusCode)
        {
            return (await response.Content.ReadFromJsonAsync<dynamic>())?.ToString();
        }
        return String.Empty;
    }
}
