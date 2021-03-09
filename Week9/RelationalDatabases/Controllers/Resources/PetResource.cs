using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RelationalDatabases.Controllers.Resources
{
    public class PetResource
    {
        [Required]
        public string name  { get; set; }
        [Required]
        public int age  { get; set; }

        #region Relations
        [Required]
        public long[] caretakerIds { get; set; }
        [Required]
        public long vetId { get; set; }
        #endregion
    }
}