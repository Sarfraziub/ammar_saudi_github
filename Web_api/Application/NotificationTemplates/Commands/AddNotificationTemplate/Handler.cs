using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.NotificationTemplates.Commands.AddNotificationTemplate;

public class Handler : IRequestHandler<AddNotificationTemplateCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(AddNotificationTemplateCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<NotificationTemplate>(request);
		_context.NotificationTemplates.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


