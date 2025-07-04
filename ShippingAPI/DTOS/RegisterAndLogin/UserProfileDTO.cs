namespace ShippingAPI.DTOS.RegisterAndLogin
{
    public class UserProfileDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string FullName { get; set; }
        public string? Address { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
    }
}
