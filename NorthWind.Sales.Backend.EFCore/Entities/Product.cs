﻿namespace NorthWind.Sales.Backend.EFCore.Entities;

internal class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public short UnitsInStock { get; set; }
    public decimal UnitPrice { get; set; }
}