﻿namespace CartService;

public class ProductDto
{
     public Guid ProductId {get;set;}
    public string ProductName {get;set;}="";
    public string Description {get;set;}="";
    public decimal Price {get;set;}
    public int Quantity {get;set;}
    
    

}
