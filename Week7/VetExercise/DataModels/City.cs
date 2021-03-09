using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VetExercise.DataModels
{
    public class City : BaseModel
    {
        public int zip { get; set; }
        public string city { get; set; }

        #region Relations
        public ICollection<Address> addresses { get; set; }
        #endregion
    }
}