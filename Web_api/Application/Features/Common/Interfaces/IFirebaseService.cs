using Application.Features.Common.Models.Firebase;

namespace Application.Features.Common.Interfaces;

public interface IFirebaseService
{
	Task<string> SendUsersTopic(FirebaseMessage firebaseMessage);
	Task<string> SendDriversTopic(FirebaseMessage firebaseMessage);
	Task<string> SendUser(FirebaseMessage firebaseMessage, string userToken);
}


