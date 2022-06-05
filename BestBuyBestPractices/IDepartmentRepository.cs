using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestPractices
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments(); //stubbed out method that must be implemented for anything that confirms to this Interface
        //this method returns a collection that confirms to IEnumerable<T> --can be list or array

        void InsertDepartment(string newDepartmentName); //anything that conforms to IDepartmentRepository must have this method implemented
    }
}
