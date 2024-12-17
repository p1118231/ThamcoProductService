using System;

namespace ThamcoProducts.Services.ProductRespository;

public class Product{

    public int Id{get; set;}

    public String Name {get; set;} = string.Empty;

    public decimal Price{get;set;}
}