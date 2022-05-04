namespace MeetUpBack.Models.Dto;

public class AddCityModel
{
    public string Name { get; set; } = string.Empty;
    public int CountryId { get; set; }
}