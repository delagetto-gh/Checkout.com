using Checkout.P1.BasketManagement.Application.Core;

namespace Checkout.P1.BasketManagement.Application
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand cmd);
    }
}