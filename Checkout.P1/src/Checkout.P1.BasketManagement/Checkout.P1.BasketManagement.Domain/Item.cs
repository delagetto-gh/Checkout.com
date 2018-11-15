using System;
using System.Collections.Generic;

namespace Checkout.P1.BasketManagement.Domain
{
    public class Item
    {
        public Item(int productId, Quantity quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public int ProductId { get; private set; }

        public Quantity Quantity { get; private set; }

        public void ChangeQuantity(int newQuantity)
        {
            this.Quantity = new Quantity(newQuantity);
        }
    }
}
