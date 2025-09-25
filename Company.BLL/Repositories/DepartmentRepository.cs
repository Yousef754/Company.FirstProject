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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DbContextSql _context;

        public DepartmentRepository(DbContextSql dbContextSql)
        {
           _context = dbContextSql;
        }

        public IEnumerable<Department> GetAll()
        {
          return  _context.Departments.ToList();

            
        }

        public Department Get(int id)
        {
            return _context.Departments.Find();

        }
        public int add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }
        public int update(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }

        public int delete(Department department)
        {
            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }

        
    }
}
