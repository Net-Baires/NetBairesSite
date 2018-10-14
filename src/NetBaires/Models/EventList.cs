using System.Collections.Generic;
using NetBaires.Models.Meetup;
using Tweetinvi.Models;

namespace NetBaires.Models
{
    public class EventList
    {
        public List<EventDetail> Property1 { get; set; }
    }

    public class TweetViewModel
    {
        public TweetViewModel()
        {
            
        }
        public TweetViewModel(ITweet x)
        {
            CreatedByName = x.CreatedBy.Name;
            CreatedByScreenName = x.CreatedBy.ScreenName;
            CreatedByScreenProfileImageUrlFullSize = x.CreatedBy.ProfileImageUrlFullSize;
            Url = x.Url;
            FullText = x.FullText;
        }

        public string CreatedByName { get; set; }
        public string CreatedByScreenName { get; set; }
        public string CreatedByScreenProfileImageUrlFullSize { get; set; }
        public string Url { get; set; }
        public string FullText { get; set; }

    }
}