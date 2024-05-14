namespace SolarWatch_MVC.Services.Api;

public class GeocodingApi : IGeoCodingApi
{
    
    private readonly ILogger<GeocodingApi> _logger;

    public GeocodingApi(ILogger<GeocodingApi> logger)
    {
        _logger = logger;
    }

    public async Task<string> GetCity(string city)
    {
        var apiKey = "eb492d310dac0d33aeacc638aec3f04d";
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}";
    
        using var client = new HttpClient();
    
        _logger.LogInformation("Calling Geo Coding API with url: {url}", url);
        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}