using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using NetBaires.Models;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace NetBaires.Pages.Twitter
{
    [Authorize]

    public class DetailModel : PageModel
    {
        private readonly IOptions<TwitterApi> _twitterApOption;

        [BindProperty]
        public long TweetId { get; set; }
        [BindProperty]
        public List<TweetViewModel> Tweets { get; set; } = new List<TweetViewModel>();
        [BindProperty]
        public TweetViewModel Win { get; set; }

        public DetailModel(IOptions<TwitterApi> twitterApOption)
        {
            _twitterApOption = twitterApOption;
        }
        public void OnGet( )
        {
           

        }
        public void OnPostAddTweetId()
        {
            Auth.SetUserCredentials(_twitterApOption.Value.ConsumerKey,
                _twitterApOption.Value.ConsumerSecret,
                _twitterApOption.Value.UserAccessToken,
                _twitterApOption.Value.UserAccessSecret);
            var currentTweet = Tweet.GetTweet(TweetId);
            var twitters = Tweet.GetRetweets(TweetId);
            //var t222wis = Timeline.GetMentionsTimeline();
            Tweets = twitters.Select(x => new TweetViewModel(x)).ToList();
            Win = new TweetViewModel();
        }
        public void OnPostRaffle()
        {
            Win = Tweets[new Random(Guid.NewGuid().GetHashCode()).Next(0, Tweets.Count)];
        }

        
    }
}