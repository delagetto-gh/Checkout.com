using Checkout.P1.BasketManagement.Application.Core;

namespace Checkout.P1.BasketManagement.Application
{
    public interface IBasketApplicationService
    {
        void Submit<TCommand>(TCommand cmd) where TCommand : ICommand;

        TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}