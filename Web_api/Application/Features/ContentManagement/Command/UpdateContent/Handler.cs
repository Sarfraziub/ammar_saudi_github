using Application.Features.Common.Interfaces;
using Application.Features.ContentManagement.Command.UpdateContentManagment;
using Application.Features.ContentManagement.Queries.GetContentManagment;
using Application.Features.ContentSettings.Commands.UpdateContentSettings;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContentManagement.Command.UpdateContent
{
    internal class Handler : IRequestHandler<UpdateContentManagmentCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContentManagmentCommand request, CancellationToken cancellationToken)
        {
            var contentSettings = await _context.ContentSettings.FindAsync(request.Id);
            if (contentSettings != null)
            {
                contentSettings.Content = request.Content;
                contentSettings.ArabicContent = request.ArabicContent;
                contentSettings.ImageId = request.ImageId;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}

