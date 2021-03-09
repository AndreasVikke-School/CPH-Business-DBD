using System.Collections.Generic;

namespace RelationalDatabases.DataModels
{
    public class Address : BaseModel
    {
        public string street { get; set; }
        public string housenumber { get; set; }

        #region Relations
        public City city { get; set; }
        public ICollection<Vet> vets { get; set; }
        public ICollection<Caretaker> caretakers { get; set; }
        #endregion
    }
}