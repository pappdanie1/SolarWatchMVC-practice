using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch_MVC.Controllers;
using SolarWatch_MVC.Models;
using SolarWatch_MVC.Services.Api;

namespace SolarWatchTester;

public class SunsetSunriseTester
{
    private Mock<ILogger<SolarWatchController>> _loggerMock;
    private SolarWatchController _controller;
    private Mock<IGeoCodingApi> _geoMock;
    private Mock<ISunApi> _sunDataMock;
    private Mock<IJsonProcessor> _jsonProcessorMock;
    
    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<SolarWatchController>>();
        _geoMock = new Mock<IGeoCodingApi>();
        _sunDataMock = new Mock<ISunApi>();
        _jsonProcessorMock = new Mock<IJsonProcessor>();

        _controller = new SolarWatchController(_jsonProcessorMock.Object, _geoMock.Object, _loggerMock.Object,   _sunDataMock.Object);
    }
    
    [Test]
    public async Task Get_InvalidCity_ReturnsBadRequest()
    {
        var city = "NY";

        var result = await _controller.GetCity(city);
        
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
    
    [Test]
    public async Task Get_ExceptionThrown_ReturnsNotFound()
    {
        var city = "New York";
        _geoMock.Setup(cp => cp.GetCity(city)).Throws(new Exception());

        var result = await _controller.GetCity(city);

        Assert.IsInstanceOf<NotFoundObjectResult>(result);
    }
    
    [Test]
    public async Task Get_EmptyCity_ReturnsBadRequest()
    {
        string city = ""; 

        var result = await _controller.GetSunsetSunrise(city);

        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async Task Get_NullCity_ReturnsBadRequest()
    {
        string city = null; 

        var result = await _controller.GetSunsetSunrise(city);

        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }
    

}