using AutoMapper;
using FluentValidation;
using MediatR;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Mappings;
using Application.Features.Common.Models.Notifications.Models;
using Domain.Model.Message;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Gift.Command.AddGift
{
    public class AddGiftCommand : IRequest<long>, IMapFrom<Domain.DbModel.Gift>
    {
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public string PhoneNumber { get; set; }
        public int ClientOrderId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddGiftCommand, Domain.DbModel.Gift>();
        }
    }

    public class Validator : AbstractValidator<AddGiftCommand>
    {
        public Validator()
        {
            RuleFor(e => e.ReceiverName)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.SenderName)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.PhoneNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.ClientOrderId)
                .NotNull()
                .NotEmpty();
        }
    }
    public class AddGiftCommandHandler : IRequestHandler<AddGiftCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWhatsAppService _whatsAppService;
        private readonly ICurrentUserService _currentUserService;

        public AddGiftCommandHandler(IApplicationDbContext context, 
            IMapper mapper, 
            IWhatsAppService whatsAppService, 
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _whatsAppService = whatsAppService;
            _currentUserService = currentUserService;
        }

        public async Task<long> Handle(AddGiftCommand request, CancellationToken cancellationToken)
        {
            var gift = await _context.Gifts.FirstOrDefaultAsync(x => x.ClientOrderId == request.ClientOrderId && x.Active == 1,cancellationToken);
            if (gift == null)
            {
                var entity = _mapper.Map<Domain.DbModel.Gift>(request);
                entity.PhoneNumber = entity.PhoneNumber.Remove(0, 1);
                await _context.Gifts.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }

            gift.ReceiverName = request.ReceiverName;
            gift.SenderName = request.SenderName;
            gift.PhoneNumber = request.PhoneNumber.Remove(0,1);
            await _context.SaveChangesAsync(cancellationToken);
            return gift.Id;
        }
    }
}
