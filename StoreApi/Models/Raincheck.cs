using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Models
{
    public partial class Raincheck
    {
        [Key]
        public int RaincheckId { get; set; }

        public Guid RaincheckGuid { get; set; }

        public string? Name { get; set; }
        public int ProductId { get; set; }
        public int? Count { get; set; }
        public double? SalePrice { get; set; }
        public int StoreId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } // Propiedad de navegación para Product

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; } // Propiedad de navegación para Store
    }
}
