using System;
using NetBaires.Data;
using NetBaires.Models.Meetup;

namespace NetBaires.Models
{
    public class EventViewModel
    {
        public EventViewModel(EventDetail eventDetail)
        {
            Id = eventDetail.id;
            Name = eventDetail.name;
            ImageUrl = eventDetail?.featured_photo?.highres_link;
            Description = eventDetail.description;
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Date = start.AddMilliseconds(eventDetail.time).ToLocalTime();
        }

        public EventViewModel(EventDb @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            ImageUrl = @event?.ImageUrl;
            Description = @event.Description;
        }

        public EventViewModel()
        {
            
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}