using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RapidPay.Api.Controllers.Base;

[ApiController]
[Authorize]
[Route("[controller]/[action]")]
[Produces("application/json")]
public abstract class RapidPayBaseController(IMapper mapper) : ControllerBase
{
	protected IMapper Mapper = mapper;
}
