using FormModule.Data;
using FormModule.Models.Account;
using FormModule.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FormModule.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext context;

        public AccountController(ApplicationContext context) 
        { 
           
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Mobile=model.Mobile,
                    Password = model.Password,
                    IsActive = model.IsActive
                };
                context.Users.Add(data);
                context.SaveChanges();
                TempData["SuccessMessage"] = "You are eligibile for login";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errorMessage"] = "Empty from can't be submitted!";
                return View(model);
            }
           
        }

        public IActionResult Login()
        { 
            return View(); 
        }
        [HttpPost]
        public IActionResult Login(LoginSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = context.Users.Where(e => e.UserName == model.UserName).SingleOrDefault();
                if(data !=null)
                {
                    bool IsValid=(data.UserName==model.UserName && data.Password==model.Password);
                    if(IsValid)
                    {
                        var identity = new ClaimsIdentity(new[]
                        {
                           new Claim(ClaimTypes.Name,model.UserName)
                        },CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal=new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
                        HttpContext.Session.SetString("UserName",model.UserName);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorPassword"] = "Invalid Password !";
                        return View(model);
                    }
                }
                else
                {
                    TempData["errorUsername"] = "user not found";
                    return View(model);
                }
            }
            else
            {
                
            }
            return View();
        }
        public IActionResult Logout() 
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login","Account");
        }


    }
}
