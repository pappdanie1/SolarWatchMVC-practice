namespace SolarWatch_MVC.Services.Api;

public interface ISunApi
{
    Task<string> GetSunriseSunset(double lat, double lon);
}