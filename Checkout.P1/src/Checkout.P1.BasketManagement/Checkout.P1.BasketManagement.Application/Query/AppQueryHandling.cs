using Checkout.P1.BasketManagement.Application.Core;
using Checkout.P1.BasketManagement.Domain;
using Checkout.P1.ProductManagement;
using System.Linq;

namespace Checkout.P1.BasketManagement.Application
{
    public class AppQueryHandler : IQueryHandler<GetBasketQuery, BasketDto>
    {
        private readonly ICustomerBasketRepository cbRepo;
        private readonly IProductApplicationService productRepo;

        public AppQueryHandler(ICustomerBasketRepository cbRepo, IProductApplicationService prodRepo)
        {
            this.cbRepo = cbRepo;
            this.productRepo = prodRepo;
        }

        public BasketDto Handle(GetBasketQuery qry)
        {
            var basket = this.cbRepo.Get(qry.CustomerId);

            var basketDto = new BasketDto
            {
                CustomerId = basket.CustomerId,
                Items = basket.Items.Select(item =>
                {
                    var product = this.productRepo.Get(item.ProductId);

                    var itemDto = new ItemDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = item.Quantity.Amount,
                        Price = $"{product.Price.Currency} {product.Price.Value}"
                    };
                    return itemDto;
                }).ToList()
            };
            return basketDto;
        }
    }
}
