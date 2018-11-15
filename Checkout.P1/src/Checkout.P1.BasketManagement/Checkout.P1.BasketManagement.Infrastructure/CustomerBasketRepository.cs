using Checkout.P1.BasketManagement.Domain;

namespace Checkout.P1.BasketManagement.Infrastructure
{
    public class CustomerBasketRepository : ICustomerBasketRepository
    {
        private readonly IDataStore dataStore;

        public CustomerBasketRepository(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public CustomerBasket GetNew(int customerId)
        {
            var idx = GetIndex(customerId);

            if (idx >= 0)
                this.dataStore.Baskets.RemoveAt(idx);

            return new CustomerBasket(customerId);
        }

        public CustomerBasket Get(int customerId)
        {
            var idx = GetIndex(customerId);

            if (idx < 0)
                return null;
            else
                return this.dataStore.Baskets[idx];
        }

        public void Save(CustomerBasket cb)
        {
            var idx = GetIndex(cb.CustomerId);

            if (idx < 0)
                this.dataStore.Baskets.Add(cb);
            else
                this.dataStore.Baskets[idx] = cb;
        }

        private int GetIndex(int customerId)
        {
            return this.dataStore.Baskets.FindIndex(b => b.CustomerId == customerId);
        }
    }
}
