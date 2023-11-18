namespace Application.Features.Common.Interfaces;

public interface IErrorMessagesService
{
	string GetAccountErrorMessageById(int id);
	string GetCommonErrorMessageById(int id);
	string GetOrderErrorMessageById(int id);
	string GetLookupErrorMessageById(int id);
}


