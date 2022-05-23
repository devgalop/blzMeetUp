using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUpBack.Data.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName => Email;
    public string PasswordHash { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public Session Session { get; set; } = null!;
    public List<UserMeetUpOwner> OwnMeetings { get; set; } = null!;
    public List<UserMeetUpAssistant> Attendance { get; set; } = null!;
}