using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        //Field (aka local variable) for making queries to the database
        private readonly IDbConnection _connection; //readonly means - the only place we can give it value is through a constructor or immediately in-line; 
                                                    //use underscore because we have it camelCased in parameter list
                                                    //_connection is a field -- fields are local variables that live inside a class
                                                    //Constructor - we are going to use this constructor to initialize(give value to) a private variable on this class
                                                    //  - like a local variable for the class itself
        //Above means - any time I create an instance of this DapperProductRepository class,
            //its goal is to receive a connection inside of its contructor parameter (below)
        
        //Constructor - special member method that allows us to create an instance of this class (method name is same as class name)
        public DapperProductRepository(IDbConnection connection)
        {
            //that connection will be stored inside our private connection field (_connection)
            //basically protecting this field _connection from the outside world (encapsulation)
            _connection = connection;
        }

        //methods that i define here will belong to the instance of the class (but if they were static they would belong to class)
        public void CreateProduct(string name, double price, int categoryID) //go to database, create a product, and leave it there
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@productName, @price, @categoryID);",
               new { productName = name, price = price, categoryID = categoryID }); //wherever you see @name, it's going to equal the value stored in name parameter
        }

        public IEnumerable<Product> GetAllProducts() //this is where Dapper comes in; Dapper extends IDbConnection
                                                        //_connection is an IDbConnection
                                                        //will use dot notation 
        {
            return _connection.Query<Product>("SELECT * FROM products;"); //anything in SQL-think about it as going to MySQL workbench
            //Query is a Dapper method - use it to Select
            //Execute is another Dapper method - use it to insert into, update, or delete
        }

       ////bonus
        //Update data - finds product and gives it a new name
        public void UpdateProductName (int productID, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }

        ////bonus
        //Delete data
        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                    new { productID = productID });
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
        }
    }
}
