using System.Collections.Generic;

namespace Checkout.P1.ProductManagement
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly Dictionary<int, Product> productDic;

        public ProductApplicationService()
        {
            this.productDic = new Dictionary<int, Product>
            {
                [1000] = new Product { Id = 1000, Name = "Plumbus", Price = new Price { Currency = "BRL", Value = 6.50m } },
                [2000] = new Product() { Id = 2000, Name = "Fleeb", Price = new Price { Currency = "BRL", Value = 6.50m } },
                [3000] = new Product() { Id = 3000, Name = "Brapple", Price = new Price { Currency = "BRL", Value = 6.50m } }
            };
        }

        public Product Get(int productId)
        {
            return this.productDic[productId];
        }
    }
}