using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Model;
using Company.PL.Dto_Employee;
using Company.PL.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mono.TextTemplating;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            IUnitOfWork unitOfWork,
            //(IEmployeeRepository employeeRepository ,
            //IDepartmentRepository departmentRepository,
            IMapper mapper
            
            ) 
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees =  await _unitOfWork.EmployeeRepository.GetAllAsync();

                
            }
            else
            {
                 employees = await  _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }



            return View(employees);
        }

        [HttpGet]
        public  async Task< IActionResult> Create()
        {
            var departments= await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"]=departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {

                if(model.Image is not null)
                {
                    model.ImageName= DocumnetSettings.UploadFile(model.Image, "Images");

                }
                //var employeee = new Employee()
                //{
                //    //Name = model.Name,
                //    //Age = model.Age,
                //    //Email = model.Email,
                //    //Address = model.Address
                //    //,
                //    //Phone = model.Phone,
                //    //Salary = model.Salary,
                //    //IsActive = model.IsActive,
                //    //IsDeleted = model.IsDeleted
                //    //,
                //    //HiringDate = model.HiringDate,
                //    //CreateAt = model.CreateAt,
                //    //DepartmentId = model.DepartmentId


                //};
                var employeee=  _mapper.Map<Employee>(model);
                 await  _unitOfWork.EmployeeRepository.AddAsync(employeee);
                var count = await _unitOfWork.completeAsync();

                if (count > 0)

                {
                    TempData["message"] = "Employee is created";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

           

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (ModelState.IsValid)
            {
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
                return View(employee);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            

            if (ModelState.IsValid)
            {

                

                var departments = _unitOfWork.DepartmentRepository.GetAllAsync();
                ViewData["departments"] = departments;


                var employee =await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

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
                    DepartmentId = employee.DepartmentId,
                };



                return View(dto);

            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute]int id,CreateEmployeeDto model)
        {

            if (ModelState.IsValid)
            {

                if(model.ImageName is not null)
                {
                    DocumnetSettings.DeleteFile(model.ImageName, "Images");
                }
                if(model.Image is not null)
                {
                    model.ImageName = DocumnetSettings.UploadFile(model.Image, "Images");
                }


                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                 _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.completeAsync();

                if (count > 0) { return RedirectToAction(nameof(Index)); }

                
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {

            if (ModelState.IsValid)
            {
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
                return View(employee);
            }

           return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id,Employee model)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Delete(model);
                var count = await _unitOfWork.completeAsync();

                if (count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumnetSettings.DeleteFile(model.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);

        }

    }
}
