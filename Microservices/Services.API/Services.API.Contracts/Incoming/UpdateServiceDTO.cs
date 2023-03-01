namespace Services.API.Contracts.Incoming;

public class UpdateServiceDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
   public long ServiceCategoryId { get; set; }
}