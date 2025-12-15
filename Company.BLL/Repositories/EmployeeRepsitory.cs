using Company.BLL.Interfaces;
using Company.DAL.Data.Context;
using Company.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepsitory : GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly DbContextSql _context;

        public EmployeeRepsitory(DbContextSql Context) :base(Context) 
        {
            _context = Context;
        }

        public async Task<List<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees.Include(E=>E.Department).Where(E=>E.Name.ToLower().Contains(name.ToLower())).ToListAsync();

        }
    }
}
