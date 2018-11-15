namespace Checkout.P1.ProductManagement
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Price Price { get; set; }
    }

    public class Price
    {
        public decimal Value { get; set; }

        public string Currency { get; set; }
    }
}