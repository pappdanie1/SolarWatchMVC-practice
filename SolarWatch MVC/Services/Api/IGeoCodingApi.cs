namespace SolarWatch_MVC.Services.Api;

public interface IGeoCodingApi
{
    Task<string> GetCity(string city);
}