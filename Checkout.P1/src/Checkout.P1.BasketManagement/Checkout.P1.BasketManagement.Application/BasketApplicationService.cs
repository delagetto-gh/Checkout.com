using Checkout.P1.BasketManagement.Infrastructure;
using Checkout.P1.ProductManagement;
using Checkout.P1.BasketManagement.Application.Core;

namespace Checkout.P1.BasketManagement.Application
{
    public class BasketApplicationService : IBasketApplicationService, IBootStrap
    {
        private AppQueryHandler appQueryGateway;
        private AppCommandHandler appCommandGatway;

        void IBootStrap.Start()
        {
            var dataStore = new CustomerBasketDataStore();
            var productRepo = new ProductApplicationService();
            var basketRepo = new CustomerBasketRepository(dataStore);

            this.appQueryGateway = new AppQueryHandler(basketRepo, productRepo);
            this.appCommandGatway = new AppCommandHandler(basketRepo);
        }
        

        public TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var queryHandler = this.appQueryGateway as IQueryHandler<TQuery, TResult>;
            if (queryHandler != null)
            {
                var res = queryHandler.Handle(query);
                return res;
            }
            return default(TResult);
        }

        public void Submit<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            var cmdHandler = this.appCommandGatway as ICommandHandler<TCommand>;
            if (cmdHandler != null)
                cmdHandler.Handle(cmd);
        }
    }
}