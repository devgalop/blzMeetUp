using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUpBack.Data.Entities;

public class Event
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public string Details { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
}