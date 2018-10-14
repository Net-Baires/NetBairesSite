using System.Collections.Generic;
using NetBaires.Models;
using NetBaires.Models.Meetup;

namespace NetBaires.Data
{
    public class EventDb
    {
        public EventDb()
        {
            
        }
        public EventDb(EventDetail eventDetail, List<SpeakerEvent> speakers)
        {
            Id = eventDetail.id;
            Name = eventDetail.name;
            ImageUrl = eventDetail?.featured_photo?.highres_link;
            Description = eventDetail.description;
            Speakers = speakers;
        }


        public EventDb(EventViewModel @event, List<SpeakerEvent> speakers)
        {
            Id = @event.Id;
            Name = @event.Name;
            ImageUrl = @event?.ImageUrl;
            Description = @event.Description;
            Speakers = speakers;

        }

        public EventDb(EventViewModel @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            ImageUrl = @event?.ImageUrl;
            Description = @event.Description;

        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<SpeakerEvent> Speakers { get; set; } = new List<SpeakerEvent>();
        public List<SponsorEvent> Sponsors { get; set; } = new List<SponsorEvent>();
    }
}