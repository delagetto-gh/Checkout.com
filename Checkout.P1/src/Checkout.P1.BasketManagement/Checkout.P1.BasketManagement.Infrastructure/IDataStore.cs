using System.Collections.Generic;
using Checkout.P1.BasketManagement.Domain;

namespace Checkout.P1.BasketManagement.Infrastructure
{
    public interface IDataStore
    {
        List<CustomerBasket> Baskets { get; }
    }
}