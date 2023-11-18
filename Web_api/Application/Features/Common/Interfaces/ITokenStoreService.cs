namespace Application.Features.Common.Interfaces;

public interface ITokenStoreService
{
	bool StoreToken(string key, string token);
	bool RemoveToken(string key);
	string GetToken(string key);
}
