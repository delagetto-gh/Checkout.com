using System;

namespace Checkout.P1.BasketManagement.Domain
{
    public class Quantity
    {
        private readonly int amount;

        public Quantity(int amount)
        {
            if (amount < 0)
                throw new Exception("Quantity cannot be less than 0");

            this.amount = amount;

        }
        
        public int Amount => amount;
    }
}