using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetExercise.Controllers.Resources
{
    public class PetResource
    {
        [Required]
        public string name { get; set; }
        [Required]
        public int age { get; set; }
    }
}