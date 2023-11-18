using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Firebase;
using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;
using MediatR;

namespace Application.NotificationTemplates.Commands.SendNotificationTemplate;

public class Handler : IRequestHandler<SendNotificationTemplateCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IFirebaseService _firebaseService;
	private readonly IMapper _mapper;
	private readonly IUserManager _userManager;

	public Handler(IApplicationDbContext context, IFirebaseService firebaseService, IMapper mapper, IUserManager userManager)
	{
		_context = context;
		_firebaseService = firebaseService;
		_mapper = mapper;
		_userManager = userManager;
	}

	public async Task<Unit> Handle(SendNotificationTemplateCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.NotificationTemplates.FindAsync(request.Id);
		var message = _mapper.Map<FirebaseMessage>(entity);
		var response = await _firebaseService.SendUsersTopic(message);

		
		_context.NotificationTemplateTransactions.Add(new NotificationTemplateTransaction
		{
			NotificationTemplateId = entity.Id,
			Response = response
		});

		var users = await _userManager.GetUsersInRoleAsync(ApplicationRoles.User.GetStringValue());
		foreach (var user in users)
		{
			_context.UserNotifications.Add(new UserNotification()
			{
				UserId = user.Id,
				Title = message.Title,
				Body = message.Body
			});
		}
		
		
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


