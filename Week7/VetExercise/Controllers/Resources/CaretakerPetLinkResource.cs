using System.ComponentModel.DataAnnotations;

namespace VetExercise.Controllers.Resources
{
    public class CaretakerPetLinkResource
    {
        [Required]
        public int caretakerId { get; set; }
        [Required]
        public int petId { get; set; }
    }
}