namespace NetBaires.Models.Meetup
{
    public class Result
    {
        public int utc_offset { get; set; }
        public string country { get; set; }
        public string visibility { get; set; }
        public string city { get; set; }
        public string timezone { get; set; }
        public long created { get; set; }
        public Topic[] topics { get; set; }
        public string link { get; set; }
        public float rating { get; set; }
        public string description { get; set; }
        public float lon { get; set; }
        public Group_Photo group_photo { get; set; }
        public Photo[] photos { get; set; }
        public string join_mode { get; set; }
        public Sponsor[] sponsors { get; set; }
        public Organizer organizer { get; set; }
        public int members { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string urlname { get; set; }
        public Category category { get; set; }
        public float lat { get; set; }
        public string who { get; set; }
    }


    public class Linkedin
    {
        public string identifier { get; set; }
    }
}