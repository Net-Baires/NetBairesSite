namespace NetBaires.Data
{
    public class SponsorEvent
    {
        public EventDb Event { get; set; }
        public string EventId { get; set; }
        public SponsorDb Sponsor { get; set; }
        public string SponsorId { get; set; }

    }
}