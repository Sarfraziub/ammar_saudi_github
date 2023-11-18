using Application.Features.Common.Interfaces;
using Domain.DbModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = Domain.DbModel.File;

namespace Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>, IApplicationDbContext
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IDateTime _dateTime;

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public ApplicationDbContext(
		DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService,
		IDateTime dateTime)
		: base(options)
	{
		_currentUserService = currentUserService;
		_dateTime = dateTime;
	}

	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<ApplicationRole> ApplicationRoles { get; set; }
	public DbSet<Region> Regions { get; set; }
	public DbSet<Location> Locations { get; set; }
	public DbSet<File> Files { get; set; }
	public DbSet<SaleItem> SaleItems { get; set; }
	public DbSet<ClientOrder> ClientOrders { get; set; }
	public DbSet<ClientOrderDetail> ClientOrderDetails { get; set; }
	public DbSet<ClientOrderLog> ClientOrderLogs { get; set; }
	public DbSet<PromoCode> PromoCodes { get; set; }
	public DbSet<SliderItem> SliderItems { get; set; }
	public DbSet<UserDeviceToken> UserDeviceTokens { get; set; }
	public DbSet<NotificationTemplate> NotificationTemplates { get; set; }

	public DbSet<NotificationTemplateTransaction> NotificationTemplateTransactions { get; set; }

	public DbSet<PaymentTry> PaymentTries { get; set; }
	public DbSet<PaymentResponse> PaymentResponses { get; set; }
	public DbSet<IpnResponse> IpnResponses { get; set; }
	public DbSet<Country> Countries { get; set; }
	public DbSet<DriverFee> DriverFees { get; set; }
	public DbSet<DriverClaim> DriverClaims { get; set; }
	public DbSet<ClientOrderPayment> ClientOrderPayments { get; set; }
	public DbSet<ClientOrderDeliverImage> ClientOrderDeliverImages { get; set; }
	public DbSet<ContentSetting> ContentSettings { get; set; }
	public DbSet<HomePageIcon> HomePageIcons { get; set; }

	public DbSet<InfluencerVideo> InfluencerVideos { get; set; }
	public DbSet<OrderSetting> OrderSettings { get; set; }
	public DbSet<UserNotification> UserNotifications { get; set; }
	public DbSet<Otp> Otps { get; set; }
	public DbSet<Package> Packages { get; set; }
	public DbSet<LocationImage> LocationImages { get; set; }
    public DbSet<PromotionalLink> PromotionalLinks { get; set; }
    public DbSet<ActionTracking> ActionTrackings { get; set; }
    public DbSet<ActionTrackingHistory> ActionTrackingHistories { get; set; }
    public DbSet<Gift> Gifts { get; set; }
    public DbSet<LinkGeneration> LinkGenerations { get; set; }
    public DbSet<LinkGenerationValue> LinkGenerationValues { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<SiteConfiguration> SiteConfigurations { get; set; }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
	{
		foreach (var entry in ChangeTracker.Entries<Entity>())
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.CreatedBy = _currentUserService.Username ?? entry.Entity.CreatedBy;
					entry.Entity.Created = _dateTime.UtcNow;
					entry.Entity.Active = 1;
					break;
				case EntityState.Modified:
					entry.Entity.UpdatedBy = _currentUserService.Username ?? entry.Entity.CreatedBy;
					entry.Entity.Updated = _dateTime.UtcNow;
					break;
				case EntityState.Detached:
					break;
				case EntityState.Unchanged:
					break;
				case EntityState.Deleted:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

		return base.SaveChangesAsync(cancellationToken);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}

}

public static class CustomExtensions
{
    public static IQueryable<TEntity> ConvertToCurrency<TEntity>(this DbSet<TEntity> dbSet, Func<TEntity, bool> predicate) where TEntity : class
    {
        return dbSet.Where(predicate).AsQueryable();
    }
}
