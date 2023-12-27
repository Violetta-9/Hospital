namespace Documents.API.Application.Contracts.Incoming;

public class DeleteOrGetFileDTO
{
    public long DocumentId { get; set; }
    public bool IsPhoto { get; set; }
}