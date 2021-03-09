using System.ComponentModel.DataAnnotations;

namespace RelationalDatabases.Controllers.Resources
{
    public class CatResource : PetResource
    {
        [Required]
        public int lifeCount { get; set; }
    }
}