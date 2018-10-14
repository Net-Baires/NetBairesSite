namespace NetBaires.Models.Meetup
{
    public class Member
    {
        public int id { get; set; }
        public string name { get; set; }
        public Photo photo { get; set; }
        public string role { get; set; }
        public Event_Context event_context { get; set; }
    }
}