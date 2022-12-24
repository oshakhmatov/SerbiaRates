using Microsoft.AspNetCore.Mvc;
using SerbiaRates.Handlers;
using SerbiaRates.ViewModels;

namespace SerbiaRates.Controllers;

public sealed class RatesController : ApiController
{
    [HttpGet]
    public async Task<RatesViewModel?> Get(
        [FromServices] GetRatesHandler handler,
        CancellationToken token)
    {
        return await handler.Handle(token);
    }
}