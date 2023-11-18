using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Errors;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Application.System.Services;

public class ErrorMessagesService : IErrorMessagesService
{
	private readonly List<ErrorMessage> _accountErrorMessages;
	private readonly List<ErrorMessage> _commonErrorMessages;
	private readonly List<ErrorMessage> _orderErrorMessages;
    private readonly List<ErrorMessage> _lookupsErrorMessages;

    public ErrorMessagesService(IWebHostEnvironment webHostEnvironment)
	{
		var jsonFile = webHostEnvironment.WebRootPath
		               + Path.DirectorySeparatorChar
		               + "Errors"
		               + Path.DirectorySeparatorChar
		               + "accountErrors.json";
		var jsonData = File.ReadAllText(jsonFile);
		_accountErrorMessages = JsonSerializer.Deserialize<List<ErrorMessage>>(jsonData);

		jsonFile = webHostEnvironment.WebRootPath
		           + Path.DirectorySeparatorChar
		           + "Errors"
		           + Path.DirectorySeparatorChar
		           + "commonErrors.json";
		jsonData = File.ReadAllText(jsonFile);
		_commonErrorMessages = JsonSerializer.Deserialize<List<ErrorMessage>>(jsonData);

		jsonFile = webHostEnvironment.WebRootPath
		           + Path.DirectorySeparatorChar
		           + "Errors"
		           + Path.DirectorySeparatorChar
		           + "ordersErrors.json";
		jsonData = File.ReadAllText(jsonFile);
		_orderErrorMessages = JsonSerializer.Deserialize<List<ErrorMessage>>(jsonData);

        jsonFile = webHostEnvironment.WebRootPath
                   + Path.DirectorySeparatorChar
                   + "Errors"
                   + Path.DirectorySeparatorChar
                   + "lookupsErrors.json";
        jsonData = File.ReadAllText(jsonFile);
        _lookupsErrorMessages = JsonSerializer.Deserialize<List<ErrorMessage>>(jsonData);
    }

	public string GetAccountErrorMessageById(int id)
	{
		return JsonConvert.SerializeObject(_accountErrorMessages.SingleOrDefault(s => s.Id == id));
	}

	public string GetCommonErrorMessageById(int id)
	{
		return JsonConvert.SerializeObject(_commonErrorMessages.SingleOrDefault(s => s.Id == id));
	}

	public string GetOrderErrorMessageById(int id)
	{
		return JsonConvert.SerializeObject(_orderErrorMessages.SingleOrDefault(s => s.Id == id));
    }

    public string GetLookupErrorMessageById(int id)
    {
	    var message = JsonConvert.SerializeObject(_lookupsErrorMessages.SingleOrDefault(s => s.Id == id));
        return message;
    }
}


