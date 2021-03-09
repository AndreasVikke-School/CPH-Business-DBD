using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetExercise.Controllers.Resources
{
    public class CaretakerResource
    {
        [Required]
        public string name { get; set; }
        [Required]
        public AddressResource address { get; set; }
    }
}