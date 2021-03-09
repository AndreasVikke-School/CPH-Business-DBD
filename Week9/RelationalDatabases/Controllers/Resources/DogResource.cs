using System.ComponentModel.DataAnnotations;

namespace RelationalDatabases.Controllers.Resources
{
    public class DogResource : PetResource
    {
        [Required]
        public string barkPitch { get; set; }
    }
}