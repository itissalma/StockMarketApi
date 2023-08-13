using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Backend.Models;

public partial class Stock
{
    public string? StockName { get; set; }

    public int StockId { get; set; }

    public float? Price { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
