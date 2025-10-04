using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Model;
using Company.PL.Dtos;
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
            var department = _departmentRepository.GetAll();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department() { Code = model.code, Name = model.name, CreateAt = model.CreateAt };
                var count = _departmentRepository.add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public IActionResult Details(int? id,string ViewName ="Details")
        {
            if (id is null) return BadRequest("invalid id");
            var department = _departmentRepository.Get(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not fount" });
            return View(ViewName,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
        //    if (id is null) return BadRequest("invalid id");
        //    var department = _departmentRepository.Get(id.Value);
        //    if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not fount" });

            return Details(id,"Edit");
        }

        //[HttpPost]
        //public IActionResult Edit([FromRoute]int id,Department department)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var count = _departmentRepository.update(department);
        //        if (count > 0) { return RedirectToAction(nameof(Index)); }
        //    }

        //    return View(department);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department() { Code = model.code, Name = model.name, CreateAt = model.CreateAt };

                var count = _departmentRepository.update(department);
                if (count > 0) { return RedirectToAction(nameof(Index)); }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Delete([FromRoute] int? id)
        {
            
                //if(id is null) { return BadRequest(); };
                //var department=_departmentRepository.Get(id.Value);
                //if (department is null) return NotFound();
                
            return Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id,Department department)
        {
            if (ModelState.IsValid) {
                var count = _departmentRepository.delete(department);
                if (count > 0) {return RedirectToAction(nameof(Index));}
            }



            return View(department);
        }

    }
}
