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
            Venue = new VenueViewModel(eventDetail?.venue);
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
        public VenueViewModel Venue { get; set; }

    }

    public class VenueViewModel
    {
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public string Country { get; set; }

        public VenueViewModel(Venue venue)
        {
            if (venue != null)
            {
                Address = venue.address_1;
                CompanyName = venue.name;
                City = venue.city;
                Lat = venue.lat;
                Long = venue.lon;
                Country = venue.localized_country_name;
            }

        }
    }
}