using Company.BLL.Interfaces;
using Company.DAL.Data.Context;
using Company.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepsitory : GenericRepository<Employee>,IEmployeeRepository
    {

        public EmployeeRepsitory(DbContextSql Context) :base(Context) 
        {

        }
        
        
    }
}
