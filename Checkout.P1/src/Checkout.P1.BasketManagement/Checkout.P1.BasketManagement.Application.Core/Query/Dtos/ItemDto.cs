namespace Checkout.P1.BasketManagement.Application.Core
{
    public class ItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public string Price { get; set; }
    }
}