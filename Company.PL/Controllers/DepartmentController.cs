using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            var department=_departmentRepository.GetAll();
            return View();
        }
    }
}
