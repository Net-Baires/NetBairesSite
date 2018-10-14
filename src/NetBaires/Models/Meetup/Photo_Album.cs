namespace NetBaires.Models.Meetup
{
    public class Photo_Album
    {
        public int id { get; set; }
        public string title { get; set; }
        public int photo_count { get; set; }
        public Photo_Sample[] photo_sample { get; set; }
    }
}