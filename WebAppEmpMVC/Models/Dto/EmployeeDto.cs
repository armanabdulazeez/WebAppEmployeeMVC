namespace WebAppEmpMVC.Models.Dto
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<AddressDto> Addresses { get; set; }
    }
}
