namespace SerbiaRates.Services.Helpers;

public static class Date
{
    public static DateOnly Today()
    {
        return DateOnly.FromDateTime(DateTime.Today);
    }
}
