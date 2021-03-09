using System.ComponentModel.DataAnnotations;

namespace VetExercise.Controllers.Resources
{
    public class AddressResource
    {
        [Required]
        public string street { get; set; }
        [Required]
        public CityResource city { get; set; }
    }
}