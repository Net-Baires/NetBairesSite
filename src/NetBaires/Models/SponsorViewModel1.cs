using NetBaires.Data;
using NetBaires.Models.Meetup;

namespace NetBaires.Models
{

    public class SponsorViewModel
    {
        private SponsorDb x;

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
        public SponsorViewModel(Sponsor sponsor, SponsorDb sponsorDb) : this(sponsor)
        {
            LogoUrlHigh = sponsorDb.LogoUrlHigh;
        }

        public SponsorViewModel(SponsorDb x)
        {
            Name = x.Name;
            ImageUrl = x.LogoUrl;
            LogoUrlHigh = x.LogoUrlHigh;
        }

        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string LogoUrlHigh { get; set; }

        public string Info { get; set; }
        public bool Selected { get; set; }

    }
}