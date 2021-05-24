namespace WebAPI.Models
{
    public class WatchlistModel
    {
        public int ProfileId { get; set; }
        public string TypeId { get; set; }
        public enum Type { Movie, Series }
        public string TimeStamp { get; set; }
        public string Season { get; set; }
        public string Episode { get; set; }
    }
}