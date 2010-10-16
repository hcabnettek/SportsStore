using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class SqlProductsRepository : IProductsRepository
    {
        private Table<Product> productsTable;
        public SqlProductsRepository (string connectionString)
        {
            productsTable = (new DataContext(connectionString)).GetTable<Product>();
        }

        public IQueryable<Product> Products
        {
            get { return productsTable; }
        }

        public void SaveProduct(Product product)
        {
            if(product.ProductId == 0)
            {
                product.CreatedDate = DateTime.Now;
                productsTable.InsertOnSubmit(product);
            }
            else if(productsTable.GetOriginalEntityState(product) == null)
            {
                productsTable.Attach(product);
                productsTable.Context.Refresh(RefreshMode.KeepCurrentValues, product);
            }

            productsTable.Context.SubmitChanges();
        }

        public void DeleteProduct(Product product)
        {
            productsTable.DeleteOnSubmit(product);
            productsTable.Context.SubmitChanges();
        }
    }
}