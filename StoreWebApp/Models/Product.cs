using System;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public Guid ProductGuid { get; set; }

        public string? SkuNumber { get; set; }
        public int CategoryId { get; set; }
        public int? RecommendationId { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string? ProductArtUrl { get; set; }
        public string? Description { get; set; }
        public DateTime? Created { get; set; }
        public string? ProductDetails { get; set; }
        public int? Inventory { get; set; }
        public int? LeadTime { get; set; }
    }
}
