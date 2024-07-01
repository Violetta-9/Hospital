namespace Profile.Application.Contracts.Incoming;

public class UpdateAccountInfoDTO
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
}