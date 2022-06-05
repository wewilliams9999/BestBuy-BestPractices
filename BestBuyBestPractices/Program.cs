using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder() //configbuilder is a class that builds a configuration for us
                        .SetBasePath(Directory.GetCurrentDirectory()) //SetBasePath is a method in ConfigBuilder - path to file where we will get the AppSettings
                                                                      //Directory is a class that has method called GetCurrentDirectory
                                                                      //currentDirectory is the path to the executable (the exe file) - the application itself
                        .AddJsonFile("appsettings.json") //this method takes a filepath - it looks inside the appsettings.json file
                        .Build(); //this builds our configuration
                                  //then we can use config variable to get a connection string

            string connString = config.GetConnectionString("DefaultConnection");
            //Console.WriteLine(connString);
            //config has a GetConnectionString method that will get the connection string for the default connection
            //DefaultConnection is listed inside the appsettings.json file
            //connection string "Server = ...." - is the way that a database authenticates a user
            //Server, database, uid (userid), pwd - your password to access the database
            //connectionstrings.com - find which database type you want - MySQL, etc - shows you what to use
            #endregion

            IDbConnection conn = new MySqlConnection(connString);
            //creating an IDbConnection variable called conn
            //and giving it the MySqlConnection implementation with the connection string passed in as a parameter in the constructor
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);


            //Products
            Console.WriteLine("Hello user, here are the current products:");
            var prodRepo = new DapperProductRepository(conn); //object we're creating - instance of DapperProductRepo class
            var products = prodRepo.GetAllProducts(); //belongs to the instance of the DapperProductRepository class

            foreach (var prod in products)
            {
                Console.WriteLine($"Id: {prod.ProductID} Name: {prod.Name}");
            }

            //Departments
            Console.WriteLine("Hello user, here are the current departments:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();

            var depos = repo.GetAllDepartments(); //repo is the instance of our Dapper repository
            Print(depos);

            Console.WriteLine("Do you want to add a department?");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of the new department?");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse); //inserts new record into the table
                Print(repo.GetAllDepartments());
            }

            Console.WriteLine("Have a great day :)");


        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentID} Name: {depo.Name}");
            }
        }


    }
}
