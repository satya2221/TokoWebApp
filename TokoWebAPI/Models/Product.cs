using System;
using System.Collections.Generic;
using TokoWebAPI.DTO;

namespace TokoWebAPI.Models;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Stock { get; set; }

    public int Price { get; set; }

    public Guid CatId { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public Product(ProductDto productDto)
    {
        ProductName = productDto.ProductName;
        Stock = productDto.Stock;
        Price = productDto.Price;
        CatId = productDto.CatId;
    }
    public Product()
    {

    }
}
