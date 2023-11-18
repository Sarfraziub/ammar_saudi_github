
namespace Domain.DbModel
{
    public class SiteConfiguration : Entity
    {
        public string AndroidAppVersion { get; set; }
        public string IosAppVersion { get; set; }
        public bool IsMaintenanceMode { get; set; }
        public bool ForceUpdate { get; set; }
    }
}
