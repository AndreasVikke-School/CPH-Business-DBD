using System.Collections.Generic;

namespace VetExercise.DataModels
{
    public class Vet : BaseModel
    {
        public string cvr { get; set; }
        public string name { get; set; }

        #region Relations
        public Address address { get; set; }
        public ICollection<Pet> pets { get; set; }
        #endregion
    }
}