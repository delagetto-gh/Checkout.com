using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Checkout.P1.BasketManagement.Domain
{
    public interface ICustomerBasketRepository
    {
        CustomerBasket GetNew(int customerId);

        CustomerBasket Get(int customerId);

        void Save(CustomerBasket cb);
    }
}
