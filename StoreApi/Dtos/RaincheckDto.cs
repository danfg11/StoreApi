namespace StoreApi.Dtos
{
    public class RaincheckDto
    {
        public int RaincheckId { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public int StoreId { get; set; }
        public ProductDto Product { get; set; }
        public StoreDto Store { get; set; }
    }

}
