using System;
using System.Collections.Generic;

namespace shopping_cart_backend.Models;

public partial class ProductsTbl
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductImage { get; set; } = null!;

    public decimal ActualPrice { get; set; }

    public decimal DiscountPrice { get; set; }
}
