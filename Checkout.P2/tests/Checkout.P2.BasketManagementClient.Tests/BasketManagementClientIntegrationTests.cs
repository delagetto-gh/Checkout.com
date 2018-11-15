using System.Collections.Generic;
using Checkout.P1.BasketManagement.Application.Core;
using Xunit;

namespace Checkout.P2.BasketManagementClient.Tests
{
    [TestCaseOrderer("Checkout.P2.BasketManagementClient.Tests.IntegrationTestOrderer", "Checkout.P2.BasketManagementClient.Tests")]
    public class BasketManagementClientIntegrationTests
    {
        private readonly BasketDto expectedBasketDto;

        private readonly string clientBaseUri;

        #region USE CASES
        //         Contains application commands - one for each identified **noun**
        //  -   ```AddItem()```
        //  -   ```RemoveItem() ```
        //  -   ```Clear() ```
        //  -   ```ChangeQuantity() ```
        //  - ++   NewBasket
        //  - ++   GetBasket
        //  - ++   GetProducts
        #endregion

        public BasketManagementClientIntegrationTests()
        {
            // Console.WriteLine($"{typeof(IntegrationTestOrderer).FullName}");
            // Console.WriteLine($"{typeof(IntegrationTestOrderer).Assembly.FullName}");

            this.clientBaseUri = "http://localhost:1337/api/basket";

            this.expectedBasketDto = new BasketDto
            {
                CustomerId = 12345,
                Items = new List<ItemDto>()
                {
                    new ItemDto{ProductId = 1000, ProductName = "Plumbus", Price = "BRL 6.50", Quantity = 9},
                    new ItemDto{ProductId = 2000, ProductName = "Fleeb", Price = "BRL 6.50", Quantity = 9}
                }
            };
        }

        [Fact, TestOrder(1)]
        public void ShouldRetrieveNewCustomerBasketWithIdGivenCustomerNewBasketMessage()
        {
            //arrange
            var msg = new NewBasketCommand { CustomerId = expectedBasketDto.CustomerId };
            var sut = new BasketManagementClient(this.clientBaseUri);

            //act
            var result = sut.GetNewBasket(msg);

            //assert
            Assert.Equal(expectedBasketDto.CustomerId, result.CustomerId);
            Assert.Empty(result.Items);
        }


        [Fact, TestOrder(2)]
        public void ShouldRetrieveExistingCustomerBasketGivenCustomerGetBasketMessage()
        {
            //arrange
            var msg = new GetBasketQuery { CustomerId = expectedBasketDto.CustomerId };
            var sut = new BasketManagementClient(this.clientBaseUri);

            //act
            var result = sut.GetBasket(msg);

            //assert
            Assert.Equal(expectedBasketDto.CustomerId, result.CustomerId);
        }

        [Fact, TestOrder(3)]
        public void ShouldAddAnItemToBasketGivenItemAddMessage()
        {
            //arrange
            var msg = new AddItemToBasketCommand
            {
                CustomerId = expectedBasketDto.CustomerId,
                ProductId = expectedBasketDto.Items[0].ProductId,
                Quantity = expectedBasketDto.Items[0].Quantity
            };
            var sut = new BasketManagementClient(this.clientBaseUri);

            //act
            var result = sut.AddItemToBasket(msg);

            //assert
            Assert.Equal(1, result.Items.Count);
            Assert.Equal(expectedBasketDto.Items[0].ProductId, result.Items[0].ProductId);
            Assert.Equal(expectedBasketDto.Items[0].Quantity, result.Items[0].Quantity);
        }

        [Fact, TestOrder(4)]
        public void ShouldAddAnotherItemToBasketGivenItemAddMessage()
        {
            //arrange
            var msg = new AddItemToBasketCommand
            {
                CustomerId = expectedBasketDto.CustomerId,
                ProductId = expectedBasketDto.Items[1].ProductId,
                Quantity = expectedBasketDto.Items[1].Quantity
            };
            var sut = new BasketManagementClient(this.clientBaseUri);

            //act
            var result = sut.AddItemToBasket(msg);

            //assert
            Assert.Equal(2, result.Items.Count);
            Assert.Equal(expectedBasketDto.Items[1].ProductId, result.Items[1].ProductId);
            Assert.Equal(expectedBasketDto.Items[1].Quantity, result.Items[1].Quantity);
        }

        [Fact, TestOrder(5)]
        public void ShouldChangeQuantityOfItemInBasketGivenChangeItemQuantityMessage()
        {
            //arrange
            var changeQtyMsg = new ChangeBasketItemQuantityCommand
            {
                CustomerId = expectedBasketDto.CustomerId,
                ProductId = expectedBasketDto.Items[0].ProductId,
                NewQuantity = 900
            };
            var sut = new BasketManagementClient(this.clientBaseUri);

            //act
            var result = sut.ChangeItemQuantity(changeQtyMsg);

            //assert
            Assert.Equal(2, result.Items.Count);
            Assert.Equal(expectedBasketDto.Items[0].ProductId, result.Items[0].ProductId);
            Assert.Equal(changeQtyMsg.NewQuantity, result.Items[0].Quantity);
        }

        [Fact, TestOrder(6)]
        public void ShouldRemoveItemToBasketGivenItemRemovalMessage()
        {
            //arrange
            var removeItemMsg = new RemoveItemFromBasketCommand
            {
                CustomerId = expectedBasketDto.CustomerId,
                ProductId = expectedBasketDto.Items[0].ProductId
            };
            var sut = new BasketManagementClient(this.clientBaseUri);

            //act
            var result = sut.RemoveItemFromBasket(removeItemMsg);

            //assert
            Assert.Equal(1, result.Items.Count);
            Assert.Equal(expectedBasketDto.Items[1].ProductId, result.Items[0].ProductId);
            Assert.Equal(expectedBasketDto.Items[1].Quantity, result.Items[0].Quantity);
        }

        [Fact, TestOrder(7)]
        public void ShouldClearCustomerBasketGivenClearBasketMessage()
        {
            //arrange
            var msg = new ClearBasketCommand { CustomerId = expectedBasketDto.CustomerId };
            var sut = new BasketManagementClient(this.clientBaseUri);

            //act
            var result = sut.ClearBasket(msg);

            //assert
            Assert.Equal(expectedBasketDto.CustomerId, result.CustomerId);
            Assert.Empty(result.Items);
        }
    }
}
