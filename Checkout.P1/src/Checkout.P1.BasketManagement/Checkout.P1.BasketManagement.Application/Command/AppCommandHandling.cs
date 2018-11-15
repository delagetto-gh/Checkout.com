using System;
using Checkout.P1.BasketManagement.Application.Core;
using Checkout.P1.BasketManagement.Domain;

namespace Checkout.P1.BasketManagement.Application
{
    public class AppCommandHandler : ICommandHandler<NewBasketCommand>,
                                     ICommandHandler<ClearBasketCommand>,
                                     ICommandHandler<AddItemToBasketCommand>,
                                     ICommandHandler<RemoveItemFromBasketCommand>,
                                     ICommandHandler<ChangeBasketItemQuantityCommand>
    {
        private readonly ICustomerBasketRepository cBRepo;

        public AppCommandHandler(ICustomerBasketRepository cBRepo)
        {
            this.cBRepo = cBRepo;
        }

        public void Handle(NewBasketCommand cmd)
        {
            var basket = this.cBRepo.GetNew(cmd.CustomerId);
            this.cBRepo.Save(basket);
        }

        public void Handle(ClearBasketCommand cmd)
        {
            var basket = this.cBRepo.Get(cmd.CustomerId);
            basket.ClearBasket();
            this.cBRepo.Save(basket);
        }

        public void Handle(AddItemToBasketCommand cmd)
        {
            var basket = this.cBRepo.Get(cmd.CustomerId);
            basket.AddItem(cmd.ProductId, cmd.Quantity);
            this.cBRepo.Save(basket);
        }

        public void Handle(RemoveItemFromBasketCommand cmd)
        {
            var basket = this.cBRepo.Get(cmd.CustomerId);
            basket.RemoveItem(cmd.ProductId);
            this.cBRepo.Save(basket);
        }

        public void Handle(ChangeBasketItemQuantityCommand cmd)
        {
            var basket = this.cBRepo.Get(cmd.CustomerId);
            basket.ChangeQuantity(cmd.ProductId, cmd.NewQuantity);
            this.cBRepo.Save(basket);
        }
    }
}
