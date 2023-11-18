// using Application.Common.Interfaces;
// using Application.Notifications.Models;
// using Infrastructures.Notifications.Configurations;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Options;
// using RestSharp;
//
// namespace Infrastructures.Notifications.Services.SMS;
//
// public class GatwaysaSMSService : ISMSService
// {
// 	private readonly IConfiguration _configuration;
// 	private readonly bool _developmentEnv;
//
// 	public GatwaysaSMSService(IOptions<SMSConfigurations> options, IConfiguration configuration)
// 	{
// 		_configuration = configuration;
// 		Options = options.Value;
// 		_developmentEnv = bool.Parse(_configuration["development"]);
// 	}
//
// 	public SMSConfigurations Options { get; } // set only via Secret Manager
//
// 	public async Task<string> SendSmsAsync(SMSMessageDto messageDto)
// 	{
// 		var _baseUrl = "https://apps.gateway.sa/vendorsms/pushsms.aspx";
// 		var restClient = new RestClient(_baseUrl);
// 		var request = new RestRequest(Method.GET);
// 		request.AddQueryParameter("user", "ahmadezzeir");
// 		request.AddQueryParameter("password", "q1w2e3r4");
// 		request.AddQueryParameter("msisdn", "966540036791");
// 		request.AddQueryParameter("sid", "THEPLANET");
// 		request.AddQueryParameter("msg", messageDto.Content);
// 		request.AddQueryParameter("fl", "0");
// 		request.AddQueryParameter("dc", "8");
// 		var response = await restClient.ExecuteGetAsync(request);
// 		return response.Content.Replace("\"", "");
// 	}
// }



