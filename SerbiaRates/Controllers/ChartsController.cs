using Microsoft.AspNetCore.Mvc;
using SerbiaRates.Handlers;
using SerbiaRates.ViewModels;

namespace SerbiaRates.Controllers;

public sealed class ChartsController : ApiController
{
    [HttpGet]
    public async Task<ChartsViewModel?> Get([FromServices] GetChartsHandler handler)
    {
        return await handler.Handle();
    }
}
