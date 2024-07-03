using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Models
{
    public partial class Product
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

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } // Propiedad de navegación para Category

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); // Propiedad de navegación para CartItems

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>(); // Propiedad de navegación para OrderDetails

        public virtual ICollection<Raincheck> Rainchecks { get; set; } = new List<Raincheck>(); // Propiedad de navegación para Rainchecks
    }
}
