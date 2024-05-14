using SolarWatch_MVC.Models;

namespace SolarWatch_MVC.Services.Api;

public interface IJsonProcessor
{
    City ProcessCity(string data);
    
    SunsetSunrise ProcessSunriseSunset(string data, City city);
}