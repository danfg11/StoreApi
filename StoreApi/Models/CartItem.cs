using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Models;

public partial class CartItem
{
    [Key]
    public int CartItemId { get; set; }

    public Guid CartItemGuid { get; set; }

    public string? CartId { get; set; }
    public int ProductId { get; set; }
    public int? Count { get; set; }
    public DateTime? DateCreated { get; set; }
}

