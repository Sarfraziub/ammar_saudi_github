using Application.Features.Common.Mappings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using File = Domain.DbModel.File;

namespace Application.Files.Commands.Upload;

public class UploadCommand : IRequest<long>, IMapFrom<File>
{
	public IFormFile File { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<UploadCommand, File>();
	}
}


