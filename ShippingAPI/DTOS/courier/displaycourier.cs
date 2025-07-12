using ShippingAPI.Models;

namespace ShippingAPI.DTOS.courier
{
    public class displaycourier
    {
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal OrderShare { get; set; }


        public List<string> SelectedGovernorates { get; set; }
        public List<string> SelectedBranchs { get; set; }
    }
}
