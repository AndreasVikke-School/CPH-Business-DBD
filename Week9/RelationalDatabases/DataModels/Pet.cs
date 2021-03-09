using System.Collections.Generic;

namespace RelationalDatabases.DataModels
{
    public class Pet : BaseModel
    {
        public string name { get; set; }
        public int age { get; set; }

        #region Relations
        public ICollection<Caretaker> caretakers { get; set; }
        public Vet vet { get; set; }
        #endregion
    }
}