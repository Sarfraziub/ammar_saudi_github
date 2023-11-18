namespace Domain.DbModel;

public class File : Entity
{
	public string Name { get; set; }
	public string Description { get; set; }
	public ICollection<SaleItem> SaleItems { get; set; }
	public ICollection<ApplicationUser> ApplicationUsers { get; set; }
	public ICollection<SliderItem> SliderItems { get; set; }

	public ICollection<ApplicationUser> NationalImages { get; set; }
	public ICollection<ApplicationUser> IbanImages { get; set; }
	public ICollection<ClientOrderDeliverImage> ClientOrderDeliverImages { get; set; }
	public ICollection<DriverClaim> DriverClaims { get; set; }
	public ICollection<HomePageIcon> HomePageIcons { get; set; }
	public ICollection<InfluencerVideo> InfluencerVideos { get; set; }
	public ICollection<Package> Packages { get; set; }
	public ICollection<LocationImage> LocationImages { get; set; }
	public ICollection<ContentSetting> ContentSettings { get; set; } 


}


