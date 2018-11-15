using Checkout.P1.BasketManagement.Application.Core;

namespace Checkout.P1.BasketManagement.Application
{
    public interface IQueryHandler<TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
    {
        TQueryResult Handle(TQuery qry);
    }
}