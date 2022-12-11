using Microsoft.AspNetCore.Mvc;
using SerbiaRates.Handlers.GetRates;

namespace SerbiaRates.Controllers;

public class RatesController : ApiController
{
    [HttpGet]
    public async Task<RatesViewModel?> Get([FromServices] GetRatesHandler handler)
    {
        return await handler.Handle();
    }
}