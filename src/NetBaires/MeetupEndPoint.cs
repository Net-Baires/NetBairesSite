namespace NetBaires
{
    public class MeetupEndPoint
    {
        public string Url { get; set; }
        public string Key { get; set; }
    }
    public class SlackEndPoint {
        public string Url { get; set; }
        public string Token { get; set; }
    }
    public class TwitterApi
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string UserAccessToken { get; set; }
        public string UserAccessSecret { get; set; }
    }
}