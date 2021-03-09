using System.Collections.Generic;

namespace RelationalDatabases.DataModels
{
    public class City : BaseModel
    {
        public string zip { get; set; }
        public string city { get; set; }

        #region Relations
        public ICollection<Address> addresses { get; set; }
        #endregion
    }
}