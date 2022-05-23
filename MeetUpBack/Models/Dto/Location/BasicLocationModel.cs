namespace MeetUpBack.Models.Dto;

public class BasicLocationModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public BasicCityModel City { get; set; } = null!;
}