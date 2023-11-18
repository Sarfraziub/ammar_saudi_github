using Microsoft.AspNetCore.Identity;

namespace Domain.DbModel;

public class ApplicationUser : IdentityUser<long>
{
    public ApplicationUser()
    {
        PopupShowingDate = DateTime.UtcNow.Date;
    }
	public string Name { get; set; }
	public File Image { get; set; }
	public long? ImageId { get; set; }
	public bool? FirstLogin { get; set; }
	public string Iban { get; set; }
	public string NationalId { get; set; }
	public string BankName { get; set; }

	public File NationalImage { get; set; }
	public long? NationalImageImageId { get; set; }

	public File IbanImage { get; set; }
	public long? IbanImageId { get; set; }
	public Languages Language { get; set; }
	public string Street { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public Country Country { get; set; }
	public long? CountryId { get; set; }
	public string ZipCode { get; set; }

	public bool? ActiveDriver { get; set; }
	public DateTime? ActivatedDate { get; set; }
	public bool Active { get; set; }
	public DateTime? PopupShowingDate { get; set; }

	public ICollection<ClientOrder> DriverClientOrders { get; set; }

	public ICollection<ClientOrder> ClientOrders { get; set; }

	public ICollection<UserDeviceToken> UserDeviceTokens { get; set; }
	public ICollection<DriverClaim> DriverClaims { get; set; }
	public ICollection<UserNotification> UserNotifications { get; set; }
	public ICollection<Otp> Otps { get; set; }
	public ICollection<LinkGeneration> LinkGenerations { get; set; }

    // public Languages Language { get; set; }
}


