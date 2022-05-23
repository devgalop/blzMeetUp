using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUpBack.Data.Entities;

public class Event
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StartHour { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public int MeetUpId { get; set; }
    public MeetUp MeetUp { get; set; } = null!;
}