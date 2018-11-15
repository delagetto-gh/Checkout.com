namespace Checkout.P1.BasketManagement.Application.Core
{
    public interface ICommand
    {

    }

    #region Application Commands 

    public class NewBasketCommand : ICommand
    {
        public int CustomerId { get; set; }
    }

    public class ClearBasketCommand : ICommand
    {
        public int CustomerId { get; set; }
    }

    public class AddItemToBasketCommand : ICommand
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }

    public class RemoveItemFromBasketCommand : ICommand
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }
    }

    public class ChangeBasketItemQuantityCommand : ICommand
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int NewQuantity { get; set; }
    }

    #endregion
}
