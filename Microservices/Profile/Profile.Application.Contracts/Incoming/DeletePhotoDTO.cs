using Profile.Application.Contracts.Enum;

namespace Profile.Application.Contracts.Incoming;

public class DeletePhotoDTO
{
    public string AccountId { get; set; }
    public SubjectUpdate SubjectUpdate { get; set; }
}