using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace RapidPay.Api.Controllers.Base;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public abstract class RapidPayBaseController(IMapper mapper) : ControllerBase
{
	protected IMapper Mapper = mapper;
}
