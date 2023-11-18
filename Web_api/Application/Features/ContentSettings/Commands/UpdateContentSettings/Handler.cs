using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ContentSettings.Commands.UpdateContentSettings;

public class Handler : IRequestHandler<UpdateContentSettingsCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(UpdateContentSettingsCommand request, CancellationToken cancellationToken)
	{
		var contentSettings = await _context.ContentSettings
			.Take(1).SingleOrDefaultAsync(c => c.Active == 1)!;
		if (contentSettings == null)
		{
			var entity = _mapper.Map<ContentSetting>(request);
			_context.ContentSettings.Add(entity);
			await _context.SaveChangesAsync(cancellationToken);
		}
		else
		{
			contentSettings.Content = request.Content;
			contentSettings.ArabicContent = request.ArabicContent;
			contentSettings.Title = request.Title;

			contentSettings.Phone = request.Phone;
			contentSettings.Email = request.Email;
			contentSettings.Address = request.Address;
			contentSettings.WhatsApp = request.WhatsApp;
			contentSettings.Facebook = request.Facebook;
			contentSettings.Instagram = request.Instagram;
			contentSettings.Twitter = request.Twitter;
			contentSettings.Snapchat = request.Snapchat;
			await _context.SaveChangesAsync(cancellationToken);

		}
		return Unit.Value;
	}
}
