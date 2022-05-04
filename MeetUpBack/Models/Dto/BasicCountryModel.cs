namespace MeetUpBack.Models.Dto;

public class BasicCountryModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<BasicCityModel> Cities { get; set; } = new List<BasicCityModel>();
}