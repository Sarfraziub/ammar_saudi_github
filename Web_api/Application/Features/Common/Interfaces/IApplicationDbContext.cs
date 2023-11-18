using Domain.DbModel;
using Microsoft.EntityFrameworkCore;
using File = Domain.DbModel.File;

namespace Application.Features.Common.Interfaces;

public interface IApplicationDbContext
{
	DbSet<ApplicationUser> ApplicationUsers { get; set; }
	DbSet<ApplicationRole> ApplicationRoles { get; set; }
	DbSet<Region> Regions { get; set; }
	DbSet<Domain.DbModel.Location> Locations { get; set; }
	DbSet<File> Files { get; set; }
	DbSet<SaleItem> SaleItems { get; set; }
	DbSet<ClientOrder> ClientOrders { get; set; }
	DbSet<ClientOrderDetail> ClientOrderDetails { get; set; }
	DbSet<ClientOrderLog> ClientOrderLogs { get; set; }
	DbSet<PromoCode> PromoCodes { get; set; }
	DbSet<SliderItem> SliderItems { get; set; }
	DbSet<UserDeviceToken> UserDeviceTokens { get; set; }
	DbSet<NotificationTemplate> NotificationTemplates { get; set; }

	DbSet<NotificationTemplateTransaction> NotificationTemplateTransactions { get; set; }

	DbSet<PaymentTry> PaymentTries { get; set; }
	DbSet<PaymentResponse> PaymentResponses { get; set; }
	DbSet<IpnResponse> IpnResponses { get; set; }

	DbSet<Country> Countries { get; set; }
	DbSet<DriverFee> DriverFees { get; set; }
	DbSet<DriverClaim> DriverClaims { get; set; }
	DbSet<ClientOrderPayment> ClientOrderPayments { get; set; }
	DbSet<ClientOrderDeliverImage> ClientOrderDeliverImages { get; set; }
	DbSet<ContentSetting> ContentSettings { get; set; }
	DbSet<HomePageIcon> HomePageIcons { get; set; }
	DbSet<InfluencerVideo> InfluencerVideos { get; set;  }
	DbSet<OrderSetting> OrderSettings { get; set; }
	DbSet<UserNotification> UserNotifications { get; set; }
	DbSet<Otp> Otps { get; set; }
	DbSet<Package> Packages { get; set; }
	DbSet<LocationImage> LocationImages { get; set; }
	DbSet<Domain.DbModel.PromotionalLink> PromotionalLinks { get; set; }
	DbSet<Domain.DbModel.ActionTracking> ActionTrackings { get; set; }
	DbSet<ActionTrackingHistory> ActionTrackingHistories { get; set; }
	DbSet<Domain.DbModel.Gift> Gifts { get; set; }
	DbSet<Domain.DbModel.LinkGeneration> LinkGenerations { get; set; }
	DbSet<Domain.DbModel.LinkGenerationValue> LinkGenerationValues { get; set; }
	DbSet<Domain.DbModel.Currency> Currencies { get; set; }
    DbSet<ExchangeRate> ExchangeRates { get; set; }
    DbSet<Settings> Settings { get; set; }
    DbSet<Domain.DbModel.SiteConfiguration> SiteConfigurations { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
