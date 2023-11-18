using Application.Features.Common.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SiteConfiguration.Command
{
    public class UpdateSiteSettingsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string AndroidAppVersion { get; set; }
        public string IosAppVersion { get; set; }
        public bool IsMaintenanceMode { get; set; }
        public bool ForceUpdate { get; set; } = false;
    }
    public class Validator : AbstractValidator<UpdateSiteSettingsCommand>
    {
        public Validator()
        {
            RuleFor(e => e.AndroidAppVersion)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.IosAppVersion)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.IsMaintenanceMode)
                .NotNull()
                .NotEmpty();
            RuleFor(e => e.ForceUpdate)
                .NotNull()
                .NotEmpty();
        }
    }
    public class UpdateSiteSettingsCommandHandler : IRequestHandler<UpdateSiteSettingsCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSiteSettingsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSiteSettingsCommand command, CancellationToken cancellationToken)
        {
            var siteSettings = await _context.SiteConfigurations
                .SingleOrDefaultAsync(x => x.Active == 1, cancellationToken);

            siteSettings.AndroidAppVersion = command.AndroidAppVersion;
            siteSettings.IosAppVersion = command.IosAppVersion;
            siteSettings.IsMaintenanceMode = command.IsMaintenanceMode;
            siteSettings.ForceUpdate = command.ForceUpdate;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
