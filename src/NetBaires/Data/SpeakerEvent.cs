using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetBaires.Models.Meetup;

namespace NetBaires.Data
{
    public class SpeakerEvent
    {
        public EventDb Event { get; set; }
        public string EventId { get; set; }
        public Speaker Speaker { get; set; }
        public string SpeakerId { get; set; }
    }
    public class SponsorEvent
    {
        public EventDb Event { get; set; }
        public string EventId { get; set; }
        public SponsorDb Sponsor { get; set; }
        public string SponsorId { get; set; }

    }

    public class SponsorDb
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SponsorEvent> Events { get; set; }
    }
}