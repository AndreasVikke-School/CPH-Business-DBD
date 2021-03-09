using System.Collections.Generic;

namespace VetExercise.DataModels
{
    public class Caretaker : BaseModel
    {
        public string name { get; set; }

        #region Relations
        public Address address { get; set; }
        public ICollection<Pet> pets { get; set; }
        #endregion
    }
}