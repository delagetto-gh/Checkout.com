using System.Collections.Generic;

namespace Checkout.P1.BasketManagement.Application.Core
{
    public class BasketDto
    {
        public int CustomerId { get; set; }

        public List<ItemDto> Items { get; set; }
    }
}
