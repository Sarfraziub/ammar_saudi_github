using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Firebase;
using FirebaseAdmin.Messaging;
using Infrastructures.CloudMessaging.Firebase.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructures.CloudMessaging.Firebase.Services;

public class FirebaseService : IFirebaseService
{
	private readonly FirebaseConfigurations _config;

	public FirebaseService(IOptions<FirebaseConfigurations> config)
	{
		_config = config.Value;
	}

	public async Task<string> SendUsersTopic(FirebaseMessage firebaseMessage)
	{
		try
		{
			var message = new Message
			{
				Notification = new Notification
				{
					Title = firebaseMessage.Title,
					Body = firebaseMessage.Body
				},
				Data = firebaseMessage.Data,
				Topic = _config.UserTopic
			};
			var messaging = FirebaseMessaging.DefaultInstance;
			return await messaging.SendAsync(message);
		}
		catch //(Exception e)
		{
			// Console.WriteLine(e);
			return "";
			// throw;
		}

	}

	public async Task<string> SendDriversTopic(FirebaseMessage firebaseMessage)
	{
		try
		{
			var message = new Message
			{
				Notification = new Notification
				{
					Title = firebaseMessage.Title,
					Body = firebaseMessage.Body
				},
				Data = firebaseMessage.Data,
				Topic = _config.DriverTopic
			};
			var messaging = FirebaseMessaging.DefaultInstance;
			return await messaging.SendAsync(message);
		}
		catch //(Exception e)
		{
			// Console.WriteLine(e);
			return "";
			// throw;
		}

	}

	public async Task<string> SendUser(FirebaseMessage firebaseMessage, string userToken)
	{
		try
		{
			var message = new Message
			{

				Notification = new Notification
				{
					Title = firebaseMessage.Title,
					Body = firebaseMessage.Body
				},
				Data = firebaseMessage.Data,
				Token = userToken
			};
			var messaging = FirebaseMessaging.DefaultInstance;
			return await messaging.SendAsync(message);
		}
		catch //(Exception e)
		{
			// Console.WriteLine(e);
			return "";
			// throw;
		}

	}
}


