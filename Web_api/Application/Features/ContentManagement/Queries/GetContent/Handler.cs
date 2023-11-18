using Application.Features.Common.Interfaces;
using Application.Features.ContentSettings.Queries.GetContentSettings;
using Application.Features.Currency.Query;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContentManagement.Queries.GetContentManagment
{
    public class Handler : IRequestHandler<GetContentManagementQuery, List<GetContentManagementDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetContentManagementDto>> Handle(GetContentManagementQuery request, CancellationToken cancellationToken)
        {
            return await _context.ContentSettings
                    .Where(c => c.Active == 1 && (c.Key == "WhatsApp.PaymentDoneMessage"
                    || c.Key == "WhatsApp.DeliveryDoneMessage" || c.Key == "WhatsApp.SendGiftMessage"))
                    .ProjectTo<GetContentManagementDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

        }
    }
}


