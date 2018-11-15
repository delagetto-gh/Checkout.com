using Checkout.P1.BasketManagement.Application;
using Checkout.P1.BasketManagement.Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.P1.BasketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly IBasketApplicationService basketService;

        public BasketController(IBasketApplicationService basketService)
        {
            this.basketService = basketService;
        }

        [Route("{customerId}")]
        [HttpPost]
        public IActionResult NewBasket(int customerId)
        {
            var cmd = new NewBasketCommand { CustomerId = customerId };

            this.basketService.Submit(cmd);

            return CreatedAtAction(nameof(GetBasketById), new { customerId }, null);
        }

        [Route("{customerId}")]
        [HttpGet(Name = nameof(GetBasketById))]
        public IActionResult GetBasketById(int customerId)
        {
            var qry = new GetBasketQuery { CustomerId = customerId };

            var basket = this.basketService.Query<GetBasketQuery, BasketDto>(qry);
            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        [Route("{customerId}")]
        [HttpPut]
        public IActionResult Clear(int customerId)
        {
            var cmd = new ClearBasketCommand { CustomerId = customerId };

            this.basketService.Submit(cmd);

            return NoContent();
        }

        [Route("{customerId}/item")]
        [HttpPost]
        public IActionResult AddItemToBasket(int customerId, int productId, int quantity)
        {
            var cmd = new AddItemToBasketCommand { CustomerId = customerId, ProductId = productId, Quantity = quantity };

            this.basketService.Submit(cmd);

            return CreatedAtAction(nameof(GetBasketById), new { customerId }, null);
        }

        [Route("{customerId}/item")]
        [HttpDelete]
        public IActionResult RemoveItemFromBasket(int customerId, int productId)
        {
            var cmd = new RemoveItemFromBasketCommand { CustomerId = customerId, ProductId = productId };

            this.basketService.Submit(cmd);

            return NoContent();
        }

        [Route("{customerId}/item")]
        [HttpPut]
        public IActionResult ChangeBasketItemQuantity(int customerId, int productId, int newQuantity)
        {
            var cmd = new ChangeBasketItemQuantityCommand { CustomerId = customerId, ProductId = productId, NewQuantity = newQuantity };

            this.basketService.Submit(cmd);

            return NoContent();
        }
    }
}
