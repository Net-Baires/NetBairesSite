using System.Collections.Generic;

namespace NetBaires.Data
{
    public class Speaker
    {
        public string Id { get; set; }
        public List<SpeakerEvent> Events { get; set; }
    }
}