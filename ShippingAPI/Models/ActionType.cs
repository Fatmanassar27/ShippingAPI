using System.ComponentModel.DataAnnotations;

namespace ShippingAPI.Models
{
    public class ActionType
    {
        [Key]
        public int Id { get; set; }      
        public string Name { get; set; } = ""; 
        
    }
}
