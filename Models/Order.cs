using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public partial class Order
    {
        public int? StockId { get; set; }
        //include stock name
        //public string? StockName { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }

        // Foreign key attribute
        public string? UserName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        // Navigation property for the related User entity
        public virtual User? User { get; set; }

        public virtual Stock? Stock { get; set; }
    }
}
