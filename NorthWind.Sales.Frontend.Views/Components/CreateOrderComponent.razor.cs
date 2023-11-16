﻿using Microsoft.AspNetCore.Components;
using NorthWind.Sales.Frontend.Views.ViewModels;

namespace NorthWind.Sales.Frontend.Views.Components
{
    public partial class CreateOrderComponent
    {
        [Parameter] 
        public CreateOrderViewModel Order { get; set; }
    }
}
