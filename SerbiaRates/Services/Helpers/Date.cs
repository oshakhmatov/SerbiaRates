namespace SerbiaRates.Services.Helpers;

public static class Date
{
    public static DateOnly Today(int add = 0)
    {
        return DateOnly.FromDateTime(DateTime.Today.AddDays(add));
    }
}
