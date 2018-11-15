using System.Net.Http;
using Checkout.P1.BasketManagement.Application.Core;

namespace Checkout.P2.BasketManagementClient
{
    public class BasketManagementClient : BasketHttpClient
    {
        public BasketManagementClient(string uri) : base(uri)
        {
        }

        public BasketDto GetNewBasket(NewBasketCommand msg)
        {
            BasketDto basketDto = null;

            var uri = this.GenerateRequestUri($"{msg.CustomerId}");

            var response = this.Post(uri, content: null);
            if (response.IsSuccessStatusCode)
            {
                basketDto = this.GetBasket(new GetBasketQuery { CustomerId = msg.CustomerId });
            }
            return basketDto;
        }

        public BasketDto GetBasket(GetBasketQuery msg)
        {
            BasketDto basketDto = null;

            var uri = this.GenerateRequestUri($"{msg.CustomerId}");

            var response = this.Get(uri, content: null);

            if (response.IsSuccessStatusCode)
                basketDto = response.Content.ReadAsAsync<BasketDto>().Result;

            return basketDto;
        }

        public BasketDto ClearBasket(ClearBasketCommand msg)
        {
            BasketDto basketDto = null;

            var uri = this.GenerateRequestUri($"{msg.CustomerId}");

            var response = this.Put(uri, content: null);
            if (response.IsSuccessStatusCode)
            {
                basketDto = this.GetBasket(new GetBasketQuery { CustomerId = msg.CustomerId });
            }
            return basketDto;
        }

        public BasketDto AddItemToBasket(AddItemToBasketCommand msg)
        {
            BasketDto basketDto = null;

            var uri = this.GenerateRequestUri($"{msg.CustomerId}", "item");

            var response = this.Post(uri, new { productId = msg.ProductId, quantity = msg.Quantity });
            if (response.IsSuccessStatusCode)
            {
                basketDto = this.GetBasket(new GetBasketQuery { CustomerId = msg.CustomerId });
            }
            return basketDto;
        }

        public BasketDto RemoveItemFromBasket(RemoveItemFromBasketCommand msg)
        {
            BasketDto basketDto = null;

            var uri = this.GenerateRequestUri($"{msg.CustomerId}", "item");

            var response = this.Delete(uri, new { productId = msg.ProductId });
            if (response.IsSuccessStatusCode)
            {
                basketDto = this.GetBasket(new GetBasketQuery { CustomerId = msg.CustomerId });
            }
            return basketDto;
        }

        public BasketDto ChangeItemQuantity(ChangeBasketItemQuantityCommand msg)
        {
            BasketDto basketDto = null;

            var uri = this.GenerateRequestUri($"{msg.CustomerId}", "item");

            var response = this.Put(uri, new { productId = msg.ProductId, newQuantity = msg.NewQuantity });
            if (response.IsSuccessStatusCode)
            {
                basketDto = this.GetBasket(new GetBasketQuery { CustomerId = msg.CustomerId });
            }
            return basketDto;
        }
    }
}
