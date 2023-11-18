using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.GetClientOrderImagesById;

public class Handler : IRequestHandler<GetClientOrderImagesByIdQuery, GetClientOrderImagesByIdVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper,
		IImageStorageService imageStorageService)
	{
		_context = context;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
	}

	public async Task<GetClientOrderImagesByIdVm> Handle(GetClientOrderImagesByIdQuery request,
		CancellationToken cancellationToken)
	{
		var vm = new GetClientOrderImagesByIdVm
		{
			ClientOrderImages = await _context.ClientOrderDeliverImages
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.ClientOrder.ClientOrderStatus == ClientOrderStatuses.Delivered
					&& c.ClientOrderId == request.Id
				)
				.ProjectTo<GetClientOrderImagesByIdDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};

		foreach (var item in vm.ClientOrderImages)
			item.Url = await _imageStorageService.GetImageURL(item.FileId);


		return vm;
	}
}


