using Moq;
using SerbiaRates;
using SerbiaRates.Data.Repos;
using SerbiaRates.Models;
using SerbiaRates.Services.Helpers;
using SerbiaRates.Services.HttpService;
using SerbiaRates.Services.ParserCreator;
using SerbiaRates.Services.RateBuilder;
using SerbiaRates.Services.RateParsers;
using SerbiaRates.Services.RatesUpdater;

namespace UnitTests.ServiceTests;

public class RatesUpdaterTests
{
    [Fact]
    public async Task RateIsUpToDate_ShouldStopWork()
    {
        var token = CancellationToken.None;
        var companyId = Const.GagaId;
        var createDate = Date.Today();
        var exchangeRate = CreateExchangeRate(companyId, createDate);
        var company = CreateCompany(companyId);

        var repo = new Mock<IRepo>();
        var webProvider = new Mock<IWebProvider>();
        var parserCreator = new Mock<IParserCreator>();
        var rateBuilder = new Mock<IRateBuilder>();

        repo.Setup(r => r.GetCompanies(token))
            .ReturnsAsync(new[] { company });

        repo.Setup(r => r.GetLastExchangeRate(companyId, token))
            .ReturnsAsync(exchangeRate);

        var cut = new RatesUpdater(
            repo.Object,
            webProvider.Object,
            parserCreator.Object,
            rateBuilder.Object);

        await cut.UpdateRates(token);

        repo.Verify(x => x.GetCompanies(token), Times.Once);
        repo.Verify(x => x.GetLastExchangeRate(companyId, token), Times.Once);

        repo.VerifyNoOtherCalls();
        webProvider.VerifyNoOtherCalls();
        parserCreator.VerifyNoOtherCalls();
        rateBuilder.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task RateIsOutOfDate_ShouldUpdateIt()
    {
        var token = CancellationToken.None;
        var companyId = Const.GagaId;
        var companyUrl = "url";
        var webResult = "result";
        var ratesCouple = CreateRateCouple(Date.Today());
        var averageRate = CreateAverageRate();
        var lastRate = CreateExchangeRate(companyId, DateOnly.FromDateTime(DateTime.Today.AddDays(-1)));
        var actualRate = CreateExchangeRate(companyId, Date.Today());
        var company = CreateCompany(companyId, companyUrl);

        var repo = new Mock<IRepo>();
        var webProvider = new Mock<IWebProvider>();
        var parserCreator = new Mock<IParserCreator>();
        var rateParser = new Mock<IRatesParser>();
        var rateBuilder = new Mock<IRateBuilder>();

        repo.Setup(r => r.GetCompanies(token))
            .ReturnsAsync(new[] { company });

        repo.Setup(r => r.GetLastExchangeRate(companyId, token))
            .ReturnsAsync(lastRate);

        webProvider.Setup(x => x.Request(companyUrl, token))
            .ReturnsAsync(webResult);

        parserCreator.Setup(x => x.CreateParser(companyId))
            .Returns(rateParser.Object);

        rateParser.Setup(x => x.Parse(webResult))
            .Returns(ratesCouple);

        rateBuilder.Setup(x => x.BuildAverageRate(ratesCouple))
            .Returns(averageRate);

        rateBuilder.Setup(x => x.BuildExchangeRate(ratesCouple, companyId))
            .Returns(actualRate);

        var cut = new RatesUpdater(
            repo.Object,
            webProvider.Object,
            parserCreator.Object,
            rateBuilder.Object);

        await cut.UpdateRates(token);

        repo.Verify(x => x.GetCompanies(token), Times.Once);
        repo.Verify(x => x.GetLastExchangeRate(companyId, token), Times.Once);
        webProvider.Verify(x => x.Request(companyUrl, token), Times.Once);
        parserCreator.Verify(x => x.CreateParser(companyId), Times.Once);
        rateParser.Verify(x => x.Parse(webResult), Times.Once);
        rateBuilder.Verify(x => x.BuildExchangeRate(ratesCouple, companyId), Times.Once);
        rateBuilder.Verify(x => x.BuildAverageRate(ratesCouple), Times.Once);
        repo.Verify(x => x.UpdateRates(actualRate, averageRate, token), Times.Once);

        repo.VerifyNoOtherCalls();
        webProvider.VerifyNoOtherCalls();
        parserCreator.VerifyNoOtherCalls();
        rateParser.VerifyNoOtherCalls();
        rateBuilder.VerifyNoOtherCalls();
    }


    private static Company CreateCompany(int companyId, string url = "url")
    {
        return new Company()
        {
            Id = companyId,
            Name = "Test",
            Url = url
        };
    }

    private static AverageRate CreateAverageRate()
    {
        return new AverageRate()
        {
            Date = Date.Today(),
            Dollar = default,
            Euro = default
        };
    }

    private static ExchangeRate CreateExchangeRate(int companyId, DateOnly date)
    {
        return new ExchangeRate()
        {
            CompanyId = companyId,
            Date = date,
            EuroBuy = default,
            EuroSell = default,
            DollarBuy = default,
            DollarSell = default,
            CreateDate = date
        };
    }

    private static RatesCoupleDto CreateRateCouple(DateOnly date)
    {
        return new RatesCoupleDto()
        {
            Date = date,
            Euro = new RateDto()
            {
                Average = 30,
                Buy = default,
                Sell = default
            },
            Dollar = new RateDto()
            {
                Average = 30,
                Buy = default,
                Sell = default
            }
        };
    }
}
