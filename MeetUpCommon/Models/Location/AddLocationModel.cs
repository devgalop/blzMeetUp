namespace MeetUpCommon.Models.Location;

public class AddLocationModel
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int CityId { get; set; }
}