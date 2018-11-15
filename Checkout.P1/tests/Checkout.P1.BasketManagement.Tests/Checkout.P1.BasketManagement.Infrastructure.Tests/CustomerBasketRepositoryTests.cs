using System;
using System.Linq;
using Checkout.P1.BasketManagement.Infrastructure;
using Checkout.P1.BasketManagement.Domain;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace Checkout.P1.BasketManagement.Infrastructure.Tests
{
    public class CustomerBasketRepositoryTests
    {
        [Fact]
        public void ShouldReturnNewCustomerBasketWithNoItemsWhenGivenCallToGetNewBasket()
        {
            //arrange
            var customerId = 123456789;
            var dataStore = new Mock<IDataStore>();

            dataStore.Setup(o => o.Baskets)
                     .Returns(new List<CustomerBasket>());

            var sut = new CustomerBasketRepository(dataStore.Object);

            //act
            var result = sut.GetNew(customerId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
            Assert.Empty(result.Items);
        }

        [Fact]
        public void ShouldReturnExistingCustomerBasketWhenGetBasketIsRequested()
        {
            //arrange
            var correctExistingBasket = new Mock<CustomerBasket>(new object[] { 123456 });
            var incorrectExistingBasket1 = new Mock<CustomerBasket>(new object[] { 999 });
            var incorrectExistingBasket2 = new Mock<CustomerBasket>(new object[] { 666 });


            var dataStore = new Mock<IDataStore>();
            dataStore.Setup(o => o.Baskets)
                     .Returns(new List<CustomerBasket> {
                     { correctExistingBasket.Object },
                     { incorrectExistingBasket1.Object },
                     { incorrectExistingBasket2.Object }});

            var sut = new CustomerBasketRepository(dataStore.Object);

            //act
            var result = sut.Get(correctExistingBasket.Object.CustomerId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(correctExistingBasket.Object.CustomerId, result.CustomerId);
        }

        [Fact]
        public void ShouldAddCustomerBasketToDataStoreWhenSaveIsRequested()
        {
            //arrange
            var customerId = 123456789;
            var customerBasket = new CustomerBasket(customerId);

            var dataStore = new Mock<IDataStore>();

            dataStore.Setup(o => o.Baskets)
                     .Returns(new List<CustomerBasket>())
                     .Verifiable();

            var sut = new CustomerBasketRepository(dataStore.Object);

            //act
            sut.Save(customerBasket);

            //assert
            dataStore.Verify();
        }
    }
}
