namespace NetBaires.Models.Meetup
{
    public class MembersResult
    {
        public string country { get; set; }
        public string city { get; set; }
        public Topic[] topics { get; set; }
        public long joined { get; set; }
        public string link { get; set; }
        public Photo photo { get; set; }
        public float lon { get; set; }
        public Other_Services other_services { get; set; }
        public string name { get; set; }
        public long visited { get; set; }
        public Self self { get; set; }
        public int id { get; set; }
        public float lat { get; set; }
        public string status { get; set; }
        public string bio { get; set; }
        public string lang { get; set; }
    }
}