namespace ShippingAPI.DTOS.RegisterAndLogin
{
    public class SafeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;
    }
}
