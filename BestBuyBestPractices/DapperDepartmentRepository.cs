using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BestBuyBestPractices
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {   
        //Field (aka local variable) for making queries to the database
        private readonly IDbConnection _connection; //readonly means - the only place we can give it value is through a constructor or immediately in-line; 
                                       //use underscore because we have it camelCased in parameter list
                                       //_connection is a field -- fields are local variables that live inside a class

        //Constructor - we are going to use this constructor to initialize(give value to) a private variable on this class - like a local variable for the class itself 
        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            var depos = _connection.Query<Department>("SELECT * FROM departments;");
                                    //Query is a Dapper method - use it to Select
                                     //Execute is another Dapper method - use it to insert into, update, or delete
            return depos;
        }

        public void InsertDepartment(string newDepartmentName) 
            //dataType must match dataType from SQL; varchar - string; decimal - decimal, int or tinyint - int;
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
            new { departmentName = newDepartmentName });
            //insert into tableName (columnName) values (@variable)
            //in sql, when you prefix s/t with @ symbol, means it's a variable
            //so we're saying use the VALUE of @departmentName

            //this is an anonymous type -- similar to lambda; means that whatever type we're trying to create, it will create that for us
            //the compiler knows it's a string
            //the value comes from the parameter

        }

    }
}
