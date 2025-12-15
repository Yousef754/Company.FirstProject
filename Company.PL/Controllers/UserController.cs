using Company.DAL.Model;
using Company.PL.Dto_Employee;
using Company.PL.Dtos;
using Company.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager) 
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
                users =  _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result

                });

            }
            else
            {
                users =  _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result

                }).Where(U => U.FirstName.ToLower().Contains(SearchInput.ToLower()));
            }



            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null) return BadRequest("invalid id");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
            {
                return NotFound(new { StatusCode = 404, message = $"user with id :{id} is not found" });
            }

            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,

                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).Result

            };



            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {


            if (ModelState.IsValid)
            {
                



                return View(id);

            }
            return View(id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string id, UserToReturnDto model)
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("invalid Operation !");
                var user = await _userManager.FindByIdAsync(id);

                if (user is null) return BadRequest("invalid Operation 1");
                
                user.UserName=model.UserName;
                user.FirstName=model.FirstName;
                user.LastName=model.LastName;
                user.Email=model.Email;

                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {

            return View(id);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserToReturnDto model)
        {

            if (id != model.Id) return BadRequest("invalid Operation !");
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return BadRequest("invalid Operation 1");

            

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);

        }

    }
}

    

