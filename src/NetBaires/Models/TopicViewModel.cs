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
}