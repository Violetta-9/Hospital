namespace Profile.Application.Contracts.Incoming;

public class UpdateAccountInfoDTO
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
}