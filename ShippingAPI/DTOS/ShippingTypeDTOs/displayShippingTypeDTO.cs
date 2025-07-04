using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.DTOS.ShippingTypeDTOs
{
    public class displayShippingTypeDTO
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public decimal AdditionalCost { get; set; }

        public int EstimatedDays { get; set; }
        public bool isActive { get; set; }


    }
}
