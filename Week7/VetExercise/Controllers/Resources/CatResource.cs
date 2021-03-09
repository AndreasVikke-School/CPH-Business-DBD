using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetExercise.Controllers.Resources
{
    public class CatResource : PetResource
    {
        [Required]
        public int lifeCount { get; set; }
        [Required]
        public int vetId { get; set; }
    }
}