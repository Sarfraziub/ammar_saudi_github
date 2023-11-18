using MediatR;

namespace Application.Files.Queries.GetPhotoUrl;

public class GetFileUrlQuery : IRequest<string>
{
	public long Id { get; set; }
}


