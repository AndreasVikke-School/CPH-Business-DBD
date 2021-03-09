using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RelationalDatabases.DataModels
{
    public class Vet : BaseModel
    {
        [Required]
        public string cvr { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        
        #region  Relations
        public Address address { get; set; }
        public ICollection<Pet> pets { get; set; }
        #endregion
    }
}