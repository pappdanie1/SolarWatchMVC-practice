namespace SolarWatch_MVC.Models;

public class SunsetSunrise
{
    public Guid Id { get; set; }
    public string Sunrise { get; set; }
    public string Sunset { get; set; }
    public City City { get; set; }
}