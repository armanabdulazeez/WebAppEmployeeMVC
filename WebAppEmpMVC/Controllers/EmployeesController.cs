using Microsoft.AspNetCore.Mvc;
using WebAppEmpMVC.HttpClients;
using WebAppEmpMVC.Models.Dto;
using WebAppEmpMVC.Models.ViewModels;

namespace WebAppEmpMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IGenericHttpClient _httpClient;

        public EmployeesController(IGenericHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<ActionResult> Index(int pageNumber=1,int pageSize=10)
        //{
        //    var employees = await _httpClient.GetAsAsync<PaginatedResponseDto<EmployeeViewModel>>(ApiConstants.GetAllPaged(pageNumber,pageSize));

        //    return View(employees);
        //}

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> EmployeeDetails(int id)
        {
            var employee = await _httpClient.GetAsAsync<EmployeeDto>($"{ApiConstants.GetEmployeeDetails}/{id}");

            if (employee == null)
                return NotFound();

            var viewModel = new EmployeeViewModel
            {
                EmployeeId=employee.EmployeeId,
                FullName=employee.FullName,
                Department = employee.Department,
                Designation = employee.Designation,
                Salary = employee.Salary,
                JoinDate = employee.JoinDate,
                Username = employee.Username,
                Role = employee.Role,
                Addresses=employee.Addresses.Select(a=>new AddressViewModel
                {
                    Id=a.Id,
                    HouseNo=a.HouseNo,
                    HouseName=a.HouseName,
                    Place=a.Place,
                    City=a.City,
                    State=a.State,
                }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> DetailsPartial(int id)
        {
            var data = await _httpClient.GetAsAsync<EmployeeViewModel>(
                ApiConstants.GetEmployeeDetails + "/" + id
            );

            if (data == null)
                return NotFound();

            return PartialView("_EmployeeDetailsPartial", data);
        }


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();  
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateEmpViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var response = await _httpClient.PostAsAsync(ApiConstants.CreateEmployee, model);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    ModelState.AddModelError("", "Failed to create employee.");
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserEntryViewModel model)
        {
            var response = await _httpClient.PostAsAsync<UserResponseViewModel>(
                ApiConstants.RegisterUser, model
            );

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeEntryViewModel model)
        {
            var response = await _httpClient.PostAsAsync<EmployeeResponseViewModel>(
                ApiConstants.CreateEmployee, model
            );

            return Json(response);
        }

    }
}
