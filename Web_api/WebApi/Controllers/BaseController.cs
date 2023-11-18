using Application.Features.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Route("api/[controller]/[action]")]

public class BaseController : ControllerBase
{
	private ICurrentUserService _currentUserService;


	private IMapper _mapper;
	private IMediator _mediator;
	protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

	protected ICurrentUserService CurrentUserService =>
		_currentUserService ??= HttpContext.RequestServices.GetService<ICurrentUserService>();

	protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
	//protected static string CompanyOwnerRole = Enum.GetName(typeof(ApplicationRoles), ApplicationRoles.CompanyOwner);
}


