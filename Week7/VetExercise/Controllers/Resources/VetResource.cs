using System.ComponentModel.DataAnnotations;

namespace VetExercise.Controllers.Resources
{
    public class VetResource
    {
        [Required]
        public string cvr { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public AddressResource address { get; set; }
    }
}