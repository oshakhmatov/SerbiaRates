using Microsoft.AspNetCore.Mvc;

namespace SerbiaRates.Controllers;

[ApiController]
[Route("[controller]")]
[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
public abstract class ApiController : ControllerBase
{
}
