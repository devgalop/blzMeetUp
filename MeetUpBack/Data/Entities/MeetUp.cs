using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUpBack.Data.Entities;

public class MeetUp
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public bool Status { get; set; } = true;
    public List<Event> Events { get; set; } = new List<Event>();
    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public UserMeetUpOwner Owner { get; set; } = null!;
    public List<UserMeetUpAssistant> Attendees { get; set; } = null!;
}