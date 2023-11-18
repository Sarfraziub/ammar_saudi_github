using System.Text.Json;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Seeds;
using Application.Features.Common.Models.Users;
using AutoMapper;
using Domain;
using Domain.DbModel;
using EnumStringValues;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using File = System.IO.File;

namespace Application.System.Commands;

public class SampleDataSeeder
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;
	private readonly RoleManager<ApplicationRole> _roleManager;
	private readonly IUserManager _userManager;
	private readonly IWebHostEnvironment _webHostEnvironment;


	public SampleDataSeeder(
		RoleManager<ApplicationRole> roleManager
		, IUserManager userManager
		, IApplicationDbContext context
		, IWebHostEnvironment webHostEnvironment
		, IMapper mapper
		, IMediator mediator
	)
	{
		_roleManager = roleManager;
		_userManager = userManager;
		_context = context;
		_webHostEnvironment = webHostEnvironment;
		_mapper = mapper;
		_mediator = mediator;
	}

	public async Task SeedAllAsync(CancellationToken cancellationToken)
	{
		await SeedRoles();
		await SeedAdminUsers();
		if (!_context.Countries.Any()) await SeedCountries(cancellationToken);
		if (!_context.HomePageIcons.Any()) await SeedHomePageIcons(cancellationToken);
		if (!_context.OrderSettings.Any()) await SeedOrderSettings(cancellationToken);
	}


	private async Task SeedCountries(CancellationToken cancellationToken)
	{
		var filePath = _webHostEnvironment.WebRootPath
		               + Path.DirectorySeparatorChar
		               + "Seeds"
		               + Path.DirectorySeparatorChar
		               + "countries.json";
		var jsonData = await File.ReadAllTextAsync(filePath, cancellationToken);
		var models = JsonSerializer.Deserialize<List<CountryModel>>(jsonData);
		if (models != null)
		{
			var countries = models.Select(model => _mapper.Map<Country>(model)).ToList();
			_context.Countries.AddRange(countries);
			await _context.SaveChangesAsync(cancellationToken);
		}
	}

	private async Task SeedRoles()
	{
		foreach (var appRole in Enum.GetValues(typeof(ApplicationRoles)).Cast<ApplicationRoles>())
		{
			var stringRole = appRole.GetStringValue();
			var role = await _roleManager.FindByNameAsync(stringRole);
			if (role == null)
				await _roleManager.CreateAsync(new ApplicationRole(stringRole));
		}
	}

	private async Task SeedAdminUsers()
	{
		var adminAhmad = await _userManager.FindByNameAsync("00966540036791");
		if (adminAhmad == null)
			await _userManager.CreateUserAsync(new CreateUserModel("00966540036791", ApplicationRoles.Admin, true));

		var adminSalem = await _userManager.FindByNameAsync("00966559105459");
		if (adminSalem == null)
			await _userManager.CreateUserAsync(new CreateUserModel("00966559105459", ApplicationRoles.Admin, true));

		var adminFake = await _userManager.FindByNameAsync("00966700000000");
		if (adminFake == null)
			await _userManager.CreateUserAsync(new CreateUserModel("00966700000000", ApplicationRoles.Admin, true));


		var testUser = await _userManager.FindByNameAsync("00966700000001");
		if (testUser == null)
			await _userManager.CreateUserAsync(new CreateUserModel("00966700000001", ApplicationRoles.User, true));


		var driverUser = await _userManager.FindByNameAsync("00966700000002");
		if (driverUser == null)
			await _userManager.CreateUserAsync(new CreateUserModel("00966700000002", ApplicationRoles.Driver, true));
	}

	private async Task SeedHomePageIcons(CancellationToken cancellationToken)
	{
		var homePageIcon = new HomePageIcon()
		{
			Title = "Most Needed",
			ArabicTitle = "المساجد الأكثر حاجة",
			// FileId = 1,
			Order = 1,
			Visible = true
		};
		_context.HomePageIcons.Add(homePageIcon);
		await _context.SaveChangesAsync(cancellationToken);

		homePageIcon = new HomePageIcon()
		{
			Title = "Mecca Mosques",
			ArabicTitle = "مساجد مكة",
			// FileId = 1,
			Order = 2,
			Visible = true
		};
		_context.HomePageIcons.Add(homePageIcon);

		await _context.SaveChangesAsync(cancellationToken);

		homePageIcon = new HomePageIcon()
		{
			Title = "Orphanages",
			ArabicTitle = "دور الأيتام",
			// FileId = 1,
			Order = 3,
			Visible = true
		};
		_context.HomePageIcons.Add(homePageIcon);

		await _context.SaveChangesAsync(cancellationToken);
	}

	//OrderSettings
	private async Task SeedOrderSettings(CancellationToken cancellationToken)
	{
		var orderSetting = new OrderSetting()
		{
			OrderSettingType = OrderSettingTypes.Tax,
			Value = (decimal)0.15
		};
		_context.OrderSettings.Add(orderSetting);
		// orderSetting = new OrderSetting()
		// {
		// 	OrderSettingType = OrderSettingTypes.DeliveryFee,
		// 	Value = 8
		// };
		_context.OrderSettings.Add(orderSetting);
		await _context.SaveChangesAsync(cancellationToken);
	}
}
