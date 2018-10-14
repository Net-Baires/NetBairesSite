namespace NetBaires.Data
{
    public class SpeakerEvent
    {
        public EventDb Event { get; set; }
        public string EventId { get; set; }
        public Speaker Speaker { get; set; }
        public string SpeakerId { get; set; }
    }
}