namespace StoreApi.Dtos
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public ProductDto Product { get; set; }
    }

}
