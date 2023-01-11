namespace Services.API.Contracts.Incoming;

public class CreateServiceDTO
{
    public string Title { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public long ServiceCategoryId { get; set; }
}