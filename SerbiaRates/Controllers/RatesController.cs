using Microsoft.AspNetCore.Mvc;
using SerbiaRates.Modules.ExchangeRates;

namespace SerbiaRates.Controllers;

[ApiController]
[Route("[controller]")]
public class RatesController : ControllerBase
{
    private readonly IExchangeRateRepo exchangeRateRepo;

    public RatesController(IExchangeRateRepo exchangeRateRepo)
    {
        this.exchangeRateRepo = exchangeRateRepo;
    }

    [HttpGet]
    public async Task<RateListViewModel> List()
    {
        return await exchangeRateRepo.GetRates();
    }
}