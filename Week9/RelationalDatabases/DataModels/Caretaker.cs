using System.Collections.Generic;

namespace RelationalDatabases.DataModels
{
    public class Caretaker : BaseModel
    {
        public string name { get; set; }
        public string phone { get; set; }

        #region  Relations
        public Address address { get; set; }
        public ICollection<Pet> pets { get; set; }
        #endregion
    }
}