namespace StaffWork.Server.GraphQL.Ware.Inputs
{
    public class CreateWareInput 
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Sizes { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public bool IsDiscount { get; set; } = false;
        public int CountInStorage { get; set; }
    }
}
