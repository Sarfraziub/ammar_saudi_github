using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Firebase;
using Infrastructures.CloudMessaging.Firebase.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers.V1;

public class FirebaseController : BaseController
{
	private readonly IFirebaseService _firebaseService;
	private readonly FirebaseConfigurations _config;

	public FirebaseController(IFirebaseService firebaseService,IOptions<FirebaseConfigurations> config)
	{
		_firebaseService = firebaseService;
		_config = config.Value;
	}


	[HttpGet]
	public ActionResult<FirebaseConfigurations> GetTopics()
	{
		return _config;
	}

	[HttpPost]
	public async Task<ActionResult> SendTopicToUsers()
	{
		var ss = await _firebaseService.SendUsersTopic(new FirebaseMessage { Title = "title", Body = "body from user topic" });
		return Ok(ss);
	}

	[HttpPost]
	public async Task<ActionResult> SendTopicToDriver()
	{
		var ss = await _firebaseService.SendDriversTopic(new FirebaseMessage { Title = "title", Body = "body from driver topic" });
		return Ok(ss);
	}

	[HttpPost]
	public async Task<ActionResult> SendUser(string token)
	{
		var ss = await _firebaseService.SendUser(new FirebaseMessage { Title = "title", Body = "body to user" }, token);
		return Ok(ss);
	}
}


