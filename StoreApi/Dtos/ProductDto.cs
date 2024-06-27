namespace StoreApi.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }

}
