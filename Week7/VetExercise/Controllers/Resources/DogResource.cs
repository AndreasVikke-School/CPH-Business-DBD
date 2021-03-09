using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetExercise.Controllers.Resources
{
    public class DogResource : PetResource
    {
        [Required]
        public string barkPitch { get; set; }
        [Required]
        public int vetId { get; set; }
    }
}