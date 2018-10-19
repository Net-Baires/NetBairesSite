using System.Collections.Generic;
using System.Linq;
using NetBaires.Models.Meetup;

namespace NetBaires.Models
{

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
}