using MeetUpBack.Data.Entities;

namespace MeetUpBack.Models.Dto;

public class BasicUserModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public BasicRoleModel Role { get; set; } = null!;
}