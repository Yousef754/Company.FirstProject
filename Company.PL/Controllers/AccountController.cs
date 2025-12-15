using Company.DAL.Model;
using Company.PL.Dtos;
using Company.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region SignUp


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp( SignUpDto model)
        {

            if (ModelState.IsValid)
            {

               
                
                var user = await _userManager.FindByNameAsync(model.UserName);

                    if (user is null)
                    {

                             if (user is null)
                             {
                        user = await _userManager.FindByEmailAsync(model.Email);

                        user = new AppUser()
                                  {
                                      UserName = model.UserName,
                                      FirstName = model.FirstName,
                                      LastName = model.LastName,
                                      Email = model.Email,
                                      IsAgree = model.IsAgree,
                             
                                  };
                             }

                    }


                var result= await  _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("signIn");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


                ModelState.AddModelError("", "invalid SignUp !!");

            }




            return View();
        }



        #endregion

        #region SignIn

        #endregion

        [HttpGet]
        public IActionResult SignIn()
        {


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                   var flag= await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (result.Succeeded)
                        {

                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }

                    }

                }
                ModelState.AddModelError("", "Invalid Error !");
            }
            return View();
        }

        #region SignOut

        [HttpGet]

        public new async Task<IActionResult> SignOut()
        {
           await  _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }


        #endregion



        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPassword(ForgetPasswordDto model)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email }, Request.Scheme);


                     
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset password",
                        Body = url
                    };


                    var flag = EmailSettings.SendEmail(email);
                    if (flag)
                    {


                        return RedirectToAction("CheckYourInbox");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid Reset Password Operation !!");

            return View("ForgetPassword",model);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        #endregion

        #region Reset Password
        [HttpGet]

        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"]=email;
            TempData["token"]=token;
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model )
        {

            if (ModelState.IsValid) 
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                if (email is null || token is null) return BadRequest("invalid Operation");
                        var user = await _userManager.FindByNameAsync(email);
                if (user != null) 
                {
                 var result=_userManager.ResetPasswordAsync(user, token,model.NewPassword);

                    if (result.IsCompletedSuccessfully)
                    {
                        return RedirectToAction("signIn");
                    }
                }
            }
            return View();
        }
        #endregion

    }
}
