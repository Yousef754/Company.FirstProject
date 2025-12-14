using Company.BLL.Interfaces;
using Company.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextSql _context;

        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }


        public UnitOfWork(DbContextSql context)
        {
            _context = context;

            DepartmentRepository=new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepsitory(_context);
        }

        public async Task<int> completeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        

        public async ValueTask DisposeAsync()
        {
           await  _context.DisposeAsync();

        }
    }
}
