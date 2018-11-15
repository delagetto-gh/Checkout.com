using System;
using System.Collections.Generic;
using Checkout.P1.BasketManagement.Domain;

namespace Checkout.P1.BasketManagement.Infrastructure
{
    public class CustomerBasketDataStore : IDataStore
    {
        public CustomerBasketDataStore()
        {
            this.Baskets = new List<CustomerBasket>();
        }
        public List<CustomerBasket> Baskets { get; private set; }
    }
}