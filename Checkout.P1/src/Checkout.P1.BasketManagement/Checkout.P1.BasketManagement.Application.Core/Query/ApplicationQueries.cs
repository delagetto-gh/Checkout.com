namespace Checkout.P1.BasketManagement.Application.Core
{
    public interface IQuery<TQueryResult>
    {
    }

    #region Application Queries 
    public class GetBasketQuery : IQuery<BasketDto>
    {
        public int CustomerId { get; set; }
    }

    #endregion
}
