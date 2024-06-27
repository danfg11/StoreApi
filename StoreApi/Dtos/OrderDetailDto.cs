namespace StoreApi.Dtos
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductDto Product { get; set; }
    }

}
