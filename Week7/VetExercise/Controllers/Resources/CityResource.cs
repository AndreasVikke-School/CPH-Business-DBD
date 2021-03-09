using System.ComponentModel.DataAnnotations;

namespace VetExercise.Controllers.Resources
{
    public class CityResource
    {
        [Required]
        public int zip { get; set; }
        [Required]
        public string city { get; set; }
    }
}