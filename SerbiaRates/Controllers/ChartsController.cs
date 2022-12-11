using Microsoft.AspNetCore.Mvc;
using SerbiaRates.Handlers.GetCharts;

namespace SerbiaRates.Controllers;

public class ChartsController : ApiController
{
	[HttpGet]
	public async Task<ChartsViewModel?> Get([FromServices] GetChartsHandler handler)
	{
		return await handler.Handle();
	}
}
