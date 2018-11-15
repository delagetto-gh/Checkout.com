using System;
using Moq;
using Xunit;
using System.Linq;

namespace Checkout.P1.BasketManagement.Domain.Tests
{
    public class CustomerBasketTests
    {
        [Fact]
        public void ShouldHaveNoItemsGivenNewCustomerBasket()
        {
            //arrange
            var customerId = 123456789;

            //act
            var sut = new CustomerBasket(customerId);

            //assert
            Assert.Empty(sut.Items);
        }

        [Fact]
        public void ShouldAddItemToBasketGivenItemDetails()
        {
            //arrange

            var customerId = 123456789;
            var itemDetail = new
            {
                ProductId = 1234,
                Quantity = 1
            };

            //act
            var sut = new CustomerBasket(customerId);
            sut.AddItem(itemDetail.ProductId, itemDetail.Quantity);

            //assert
            Assert.NotEmpty(sut.Items);
        }

        [Fact]
        public void ShouldHaveCorrectItemDetailsInBasketGivenAddedItem()
        {
            //arrange

            var customerId = 123456789;
            var itemDetail = new
            {
                ProductId = 1234,
                Quantity = 1
            };

            //act
            var sut = new CustomerBasket(customerId);
            sut.AddItem(itemDetail.ProductId, itemDetail.Quantity);
            var result = sut.Items.Single();

            //assert
            Assert.Equal(itemDetail.ProductId, result.ProductId);
            Assert.Equal(itemDetail.Quantity, result.Quantity.Amount);
        }

        [Fact]
        public void ShouldIncreaseQuantityOfItemInBasketGivenAddOfExistingItemProduct()
        {
            //arrange

            //act

            //assert
        }

        [Fact]
        public void ShouldRemoveItemFromBasketGivenExistingItemDetails()
        {
            //arrange

            var customerId = 123456789;
            var itemDetail = new
            {
                ProductId = 1234,
                Quantity = 1
            };

            var sut = new CustomerBasket(customerId);
            sut.AddItem(itemDetail.ProductId, itemDetail.Quantity);

            //act
            sut.RemoveItem(itemDetail.ProductId);

            //assert
            Assert.DoesNotContain(sut.Items, item => item.ProductId == itemDetail.ProductId);
        }

        [Fact]
        public void ShouldClearBasket()
        {
            //arrange

            var customerId = 123456789;
            var itemDetail = new
            {
                ProductId = 1234,
                Quantity = 1
            };

            var sut = new CustomerBasket(customerId);
            sut.AddItem(itemDetail.ProductId, itemDetail.Quantity);

            //act
            sut.ClearBasket();

            //assert
            Assert.Empty(sut.Items);
        }

        [Fact]
        public void ShouldClearBasketEvenWithNoItemsInBasket()
        {
            //arrange

            var customerId = 123456789;
            var sut = new CustomerBasket(customerId);

            //act
            sut.ClearBasket();

            //assert
            Assert.Empty(sut.Items);
        }

        [Fact]
        public void ShouldChangeQuantityOfItemInBasketGivenExistingItemDetail()
        {
            //arrange

            var customerId = 123456789;
            var itemDetail = new
            {
                ProductId = 1234,
                Quantity = 1
            };
            var newQuantity = 25;

            var sut = new CustomerBasket(customerId);
            sut.AddItem(itemDetail.ProductId, itemDetail.Quantity);

            //act
            sut.ChangeQuantity(itemDetail.ProductId, newQuantity);

            //assert
            var result = sut.Items.Single();
            Assert.Equal(itemDetail.ProductId, result.ProductId);
            Assert.Equal(newQuantity, result.Quantity.Amount);
        }
    }
}
