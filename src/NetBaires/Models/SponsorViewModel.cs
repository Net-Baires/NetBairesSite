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
}