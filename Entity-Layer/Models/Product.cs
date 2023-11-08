﻿using System.ComponentModel.DataAnnotations;

namespace Entity_Layer.Models;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double? Price { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool IsApproved { get; set; }
    public List<ProductCategory> ProductCategory { get; set; }
}
