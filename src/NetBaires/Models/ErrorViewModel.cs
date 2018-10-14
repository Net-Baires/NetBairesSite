using System.Collections.Generic;
using NetBaires.Models.Meetup;

namespace NetBaires.Models
{
    public class SponsorViewModel
    {
        public SponsorViewModel()
        {
            
        }
        public SponsorViewModel(Sponsor sponsor)
        {
            Name = sponsor.name;
            ImageUrl = sponsor.image_url;
            Url = sponsor.url;
            Info = sponsor.info;
        }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Info { get; set; }
        public bool Selected { get; set; }

    }
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string HighresLink { get; set; }
        public string PhotoLink { get; set; }
        public string ThumbLink { get; set; }
        public PhotoViewModel(PhotoDetail photo)
        {
            Id = photo.id;
            HighresLink = photo.highres_link;
            PhotoLink = photo.photo_link;
            ThumbLink = photo.thumb_link;
        }
      
    }
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class EventList
    {
        public List<EventDetail> Property1 { get; set; }
    }


}