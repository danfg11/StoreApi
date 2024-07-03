using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Models;

public partial class Store
{
    [Key]
    public int StoreId { get; set; }

    public Guid StoreGuid { get; set; }

    public string? Name { get; set; }
}
