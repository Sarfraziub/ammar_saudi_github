using Application.Features.Common.Interfaces;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Application.System.Commands;

public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;
	private readonly RoleManager<ApplicationRole> _roleManager;
	private readonly IUserManager _userManager;
	private readonly IWebHostEnvironment _webHostEnvironment;

	public SeedSampleDataCommandHandler(IUserManager userManager
		, RoleManager<ApplicationRole> roleManager
		, IApplicationDbContext context
		, IWebHostEnvironment webHostEnvironment
		, IMapper mapper
		, IMediator mediator
	)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_context = context;
		_webHostEnvironment = webHostEnvironment;
		_mapper = mapper;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
	{
		var seeder =
			new SampleDataSeeder(_roleManager, _userManager, _context, _webHostEnvironment, _mapper, _mediator);

		await seeder.SeedAllAsync(cancellationToken);

		return Unit.Value;
	}
}

