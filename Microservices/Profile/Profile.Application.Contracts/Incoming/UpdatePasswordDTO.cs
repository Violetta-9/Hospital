namespace Profile.Application.Contracts.Incoming;

public class UpdatePasswordDTO
{
    public string AccountId { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}