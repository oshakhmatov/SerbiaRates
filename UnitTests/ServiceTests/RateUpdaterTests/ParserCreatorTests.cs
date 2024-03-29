﻿using SerbiaRates;
using SerbiaRates.Services.RatesUpdater.ParserCreator;
using SerbiaRates.Services.RatesUpdater.RateParsers;

namespace UnitTests.ServiceTests.RateUpdaterTests;

public class ParserCreatorTests
{
    [Theory]
    [InlineData(Const.GagaId, typeof(GagaParser))]
    [InlineData(Const.PostanskaId, typeof(PostanskaParser))]
    [InlineData(Const.EldoradoId, typeof(EldoradoParser))]
    [InlineData(Const.TackaId, typeof(TackaParser))]
    public void ShouldReturnCorrectParser(int companyId, Type expectedType)
    {
        // Arrange
        var cut = new ParserCreator();

        // Act
        var actual = cut.CreateParser(companyId);

        // Assert
        Assert.Equal(actual.GetType(), expectedType);
    }
}
