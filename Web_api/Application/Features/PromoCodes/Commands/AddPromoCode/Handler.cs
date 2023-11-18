using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PromoCodes.Commands.AddPromoCode;

public class Handler : IRequestHandler<AddPromoCodeCommand, Unit>
{
	private readonly IApplicationDbContext _context;
    private readonly IErrorMessagesService _errorMessagesService;
    private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper, IErrorMessagesService errorMessagesService)
	{
		_context = context;
		_mapper = mapper;
		_errorMessagesService = errorMessagesService;
    }

	public async Task<Unit> Handle(AddPromoCodeCommand request, CancellationToken cancellationToken)
	{
			
		if (await _context.PromoCodes.AnyAsync(a => a.Code.ToLower() == request.Code.ToLower() && a.Active == 1))
		{
            throw new AppValidationException(_errorMessagesService.GetLookupErrorMessageById(1));
        }
            
        var entity = _mapper.Map<PromoCode>(request);
		_context.PromoCodes.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


