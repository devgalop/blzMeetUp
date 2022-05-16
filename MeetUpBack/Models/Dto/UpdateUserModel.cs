namespace MeetUpBack.Models.Dto;

public class UpdateUserModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool Status { get; set; }
    public int RoleId { get; set; }
}