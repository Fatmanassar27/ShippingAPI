namespace ShippingAPI.DTOS.Employee
{
    public class EmployeeWithPermissionsDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public List<string> Permissions { get; set; } = new();

    }
}
