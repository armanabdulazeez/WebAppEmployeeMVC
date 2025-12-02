namespace WebAppEmpMVC.HttpClients
{
    public class ApiConstants
    {
        public const string Login = "/api/Users/login";
        public const string Register = "/api/Users/login";

        public const string GetAllEmployees = "/api/Employees/GetAllEmployees";
        public static string GetAllPaged(int pageNumber, int pageSize)
        {
            return $"{GetAllEmployees}?pageNumber={pageNumber}&pageSize={pageSize}";
        }

        public const string GetEmployeeDetails = "/api/Employees/GetEmployeeById";
        public const string CreateEmployee = "/api/Employees/CreateEmployee";
    }
}
