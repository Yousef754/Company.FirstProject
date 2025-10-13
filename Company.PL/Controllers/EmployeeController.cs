using Company.BLL.Interfaces;
using Company.DAL.Model;
using Company.PL.Dto_Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mono.TextTemplating;
using System.Net;
using System.Runtime.InteropServices;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            var employees=_employeeRepository.GetAll().ToList();



            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employeee = new Employee()
                {
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address
                    ,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted
                    ,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt

                };

                var count = _employeeRepository.Add(employeee);
                if (count > 0)

                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

           

        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeRepository.Get(id.Value);
                return View(employee);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {

            if (ModelState.IsValid)
            {

                


                var employee = _employeeRepository.Get(id.Value);

                var dto = new CreateEmployeeDto()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Email = employee.Email,
                    Address = employee.Address,
                    Phone = employee.Phone,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    IsDeleted = employee.IsDeleted,
                    HiringDate = employee.HiringDate,
                    CreateAt = employee.CreateAt,
                };



                return View(dto);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute]int? id,UpdateEmployeeDto model)
        {

            if (ModelState.IsValid)
            {

                if (id == null) return BadRequest();

                var employeee = new Employee()
                {
                    Id = id.Value,
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    Address = model.Address
                    ,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted
                    ,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt
                };
                var count=_employeeRepository.Update(employeee);
                if (count > 0) { return RedirectToAction(nameof(Index)); }

                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int? id)
        {

            if (ModelState.IsValid)
            {
                var employee = _employeeRepository.Get(id.Value);
                return View(employee);
            }

           return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id,Employee model)
        {

            if (ModelState.IsValid)
            {
                var count = _employeeRepository.Delete(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);

        }

    }
}
