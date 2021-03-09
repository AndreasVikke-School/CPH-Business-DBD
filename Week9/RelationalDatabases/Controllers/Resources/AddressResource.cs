using System.ComponentModel.DataAnnotations;

namespace RelationalDatabases.Controllers.Resources
{
    public class AddressResource
    {
        [Required]
        public string street { get; set; }
        [Required]
        public string housenumber { get; set; }
        [Required]
        public string zip { get; set; }
        [Required]
        public string city { get; set; }
    }
}