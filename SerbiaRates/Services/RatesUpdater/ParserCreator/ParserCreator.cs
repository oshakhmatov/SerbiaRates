﻿using SerbiaRates.Services.RatesUpdater.RateParsers;

namespace SerbiaRates.Services.RatesUpdater.ParserCreator;

public sealed class ParserCreator : IParserCreator
{
    public IRatesParser CreateParser(int providerId) => providerId switch
    {
        Const.GagaId => new GagaParser(),
        Const.PostanskaId => new PostanskaParser(),
        Const.EldoradoId => new EldoradoParser(),
        Const.TackaId => new TackaParser(),
        _ => throw new NotImplementedException($"No parser for provider with ID {providerId} is registred")
    };
}
