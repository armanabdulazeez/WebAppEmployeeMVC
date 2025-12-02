using Microsoft.AspNetCore.Mvc;
using WebAppEmpMVC.Models.Dto;
using WebAppEmpMVC.HttpClients;
using WebAppEmpMVC.Models.ViewModels;

namespace WebAppEmpMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly IGenericHttpClient _httpClient;

        public LoginController(IGenericHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var response = await _httpClient.PostAsAsync<dynamic>(ApiConstants.Login, 
                new LoginRequestDto
                {
                    Username=model.Username,
                    Password=model.Password
                });

            string token = response.token;

            if (response == null || string.IsNullOrEmpty(token))
            {
                ViewBag.Error = "Invalid username or password";
                return View(model);
            }

            HttpContext.Session.SetString("token", token);

            return RedirectToAction("Index", "Employees");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Login");
        }
    }
}
