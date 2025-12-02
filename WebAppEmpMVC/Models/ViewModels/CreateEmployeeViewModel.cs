namespace WebAppEmpMVC.Models.ViewModels
{
    public class CreateEmployeeViewModel
    {
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
