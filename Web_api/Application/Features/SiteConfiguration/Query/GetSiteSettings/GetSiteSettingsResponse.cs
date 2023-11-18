using Application.Features.Common.Mappings;
using AutoMapper;

namespace Application.Features.SiteConfiguration.Query.GetSiteSettings
{
    public class GetSiteSettingsResponse : IMapFrom<Domain.DbModel.SiteConfiguration>
    {
        public string AndroidAppVersion { get; set; }
        public string IosAppVersion { get; set; }
        public bool IsMaintenanceMode { get; set; }
        public bool ForceUpdate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.DbModel.SiteConfiguration, GetSiteSettingsResponse>();
        }
    }
}

