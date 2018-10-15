using System.Collections.Generic;
using System.Linq;
using NetBaires.Models.Meetup;

namespace NetBaires.Models
{
    public class TopicViewModel
    {

        public TopicViewModel(Topic x)
        {
            Id = x.id;
            Name = x.name;
            UrlKey = x.urlkey;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlKey { get; set; }
    }

    public class GroupViewModel
    {
        public GroupViewModel(Result groupDetail)
        {

            Members = groupDetail.members;
            Description = groupDetail.description;
            Name = groupDetail.name;
            Who = groupDetail.who;
            Topics = groupDetail.topics?.Select(x => new TopicViewModel(x))?.ToList();
        }

        public List<TopicViewModel> Topics { get; set; }
        public string Who { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Members { get; set; }
    }

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