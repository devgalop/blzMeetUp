namespace MeetUpBack.Models.Dto;

public class UpdateUserModel : AddUserModel
{
    public int Id { get; set; }
    public bool Status { get; set; }
}