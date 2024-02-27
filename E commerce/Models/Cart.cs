using System;
using System.Collections.Generic;

namespace shopping_cart_backend.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? ProductId { get; set; }
}
