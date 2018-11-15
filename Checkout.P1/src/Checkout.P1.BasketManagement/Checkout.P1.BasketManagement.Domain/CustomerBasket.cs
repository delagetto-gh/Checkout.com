using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Checkout.P1.BasketManagement.Domain
{
    public class CustomerBasket
    {
        private List<Item> items;

        public CustomerBasket(int customerId)
        {
            this.CustomerId = customerId;
            this.items = new List<Item>();
        }

        public int CustomerId { get; private set; }

        public IEnumerable<Item> Items
        {
            get
            {
                return this.items.AsReadOnly();
            }
            private set
            {
                this.items = new List<Item>(value);
            }
        }

        public void AddItem(int productId, int quantity)
        {
            var itemToBeAdded = new Item(productId, new Quantity(quantity));

            this.items.Add(itemToBeAdded);
        }

        public void ClearBasket()
        {
            this.items.Clear();
        }

        public void RemoveItem(int productId)
        {
            this.items.RemoveAll(item => item.ProductId == productId);
        }

        public void ChangeQuantity(int productId, int newQuantity)
        {
            //get index of current Item
            var curItemIdx = this.items.FindIndex(item => item.ProductId == productId);

            if (curItemIdx < 0)
                throw new Exception($"Cannot update quantity. Product does not exist in basket.");

            //change Quanity on item.
            this.items[curItemIdx].ChangeQuantity(newQuantity);
        }
    }
}
