using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestPractices
{
    interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts(); //stubbed out method (no scope)

        void CreateProduct(string name, double price, int categoryID); //stubbed out method (no scope)
    }
}
