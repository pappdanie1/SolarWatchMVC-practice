using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SolarWatch_MVC.Models;
using SolarWatch_MVC.Services.Api;

namespace SolarWatch_MVC.Controllers;

public class SolarWatchController : Controller
{
    private readonly ILogger<SolarWatchController> _logger;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly IGeoCodingApi _codingApi;
    private readonly ISunApi _sunApi;

    public SolarWatchController(IJsonProcessor jsonProcessor, IGeoCodingApi codingApi, ILogger<SolarWatchController> logger, ISunApi sunApi)
    {
        _jsonProcessor = jsonProcessor;
        _codingApi = codingApi;
        _logger = logger;
        _sunApi = sunApi;
    }

    public async Task<IActionResult> GetCity(string city)
    {
        try
        {
            if (string.IsNullOrEmpty(city) || city.Length < 3)
            {
                return BadRequest("City name must be at least 3 characters long.");
            }
            
            var cityData = await _codingApi.GetCity(city);
            var processed = _jsonProcessor.ProcessCity(cityData);

            if (processed == null)
            {
                return NotFound("City not found");
            }

            return View("GetCity", processed); 
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting data");
            return NotFound("Error getting data");
        }
    }
    
    public async Task<IActionResult> GetSunsetSunrise(string city)
    {
        try
        {
            if (string.IsNullOrEmpty(city) || city.Length < 3)
            {
                return BadRequest("City name must be at least 3 characters long.");
            }
            
            var cityData = await _codingApi.GetCity(city);
            var processedCity = _jsonProcessor.ProcessCity(cityData);

            if (processedCity == null)
            {
                return NotFound("City not found");
            }

            var lat = processedCity.Latitude;
            var lon = processedCity.Longitude;

            var sunData = await _sunApi.GetSunriseSunset(lat, lon);
            var processedSunData = _jsonProcessor.ProcessSunriseSunset(sunData, processedCity);

            return View("GetSunsetSunrise", processedSunData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting data");
            return NotFound("Error getting data");
        }
    }

}