using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.UserDeviceTokens.Commands.AddUserDeviceToken;

public class Handler : IRequestHandler<AddUserDeviceTokenCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(AddUserDeviceTokenCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<UserDeviceToken>(request);
		entity.UserId = _currentUserService.UserId.Value;
		entity.UserType = UserTypes.User; 
		_context.UserDeviceTokens.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


