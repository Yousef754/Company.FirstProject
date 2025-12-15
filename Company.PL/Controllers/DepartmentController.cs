using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Model;
using Company.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var department = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department() { Code = model.code, Name = model.name, CreateAt = model.CreateAt };
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.completeAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id,string ViewName ="Details")
        {
            if (id is null) return BadRequest("invalid id");
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not fount" });
            return View(ViewName,department);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return BadRequest("invalid id");
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            var dto = new CreateDepartmentDto()
            {
                name = department.Name,
                code = department.Code,
                CreateAt = department.CreateAt
            };



            if (department is null) return NotFound(new { StatusCode = 404, Message = $"Department with {id} is not fount" });

            return View(dto);
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
        public async Task<IActionResult> Update([FromRoute] int? id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department() {Id=id.Value ,Code = model.code, Name = model.name, CreateAt = model.CreateAt };

                _unitOfWork.DepartmentRepository.Update(department);
                var count = await _unitOfWork.completeAsync();


                if (count > 0) { return RedirectToAction(nameof(Index)); }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            
                //if(id is null) { return BadRequest(); };
                //var department=_departmentRepository.Get(id.Value);
                //if (department is null) return NotFound();
                
            return await Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id,Department department)
        {
            if (ModelState.IsValid) {
                 _unitOfWork.DepartmentRepository.Delete(department);
                var count =await _unitOfWork.completeAsync();

                if (count > 0) {return RedirectToAction(nameof(Index));}
            }



            return View(department);
        }

    }
}
