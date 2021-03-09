using System.ComponentModel.DataAnnotations;
using RelationalDatabases.DataModels;

namespace RelationalDatabases.Controllers.Resources
{
    public class VetResource
    {
        [Required]
        public string cvr { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string phone { get; set; }

        #region  Relations
        [Required]
        public AddressResource address { get; set; }
        #endregion
    }
}