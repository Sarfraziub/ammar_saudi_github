using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InfluencerVideos.Queries.GetInfluencerVideos;

public class Handler : IRequestHandler<GetInfluencerVideosQuery, GetInfluencerVideosVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly IImageStorageService _imageStorageService;

	public Handler(IApplicationDbContext context, IMapper mapper, IImageStorageService imageStorageService)
	{
		_context = context;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
	}

	public async Task<GetInfluencerVideosVm> Handle(GetInfluencerVideosQuery request, CancellationToken cancellationToken)
	{
		var vm = new GetInfluencerVideosVm
		{
			InfluencerVideos = await _context.InfluencerVideos
				.AsNoTracking()
				.Where(c => c.Active == 1)
				.ProjectTo<GetInfluencerVideoDto>(_mapper.ConfigurationProvider)
				// .OrderBy(t => t.Order)
				.ToListAsync(cancellationToken)
		};

		foreach (var item in vm.InfluencerVideos)
			item.ImageUrl = await _imageStorageService.GetImageURL(item.FileId);
		return vm;
	}
}
