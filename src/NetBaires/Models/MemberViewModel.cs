using NetBaires.Models.Meetup;

namespace NetBaires.Models
{
    public class MemberViewModel
    {
        public MemberViewModel(MemberDetail memberDetail)
        {
            Id = memberDetail.id.ToString();
            Name = memberDetail.name;
            Bio = memberDetail.bio;
            Facebook = memberDetail.other_services?.facebook?.identifier;
            Linkedin = memberDetail.other_services?.linkedin?.identifier;
            Twitter = memberDetail.other_services?.twitter?.identifier;
            ImageUrl = memberDetail?.photo?.highres_link;
        }
        public MemberViewModel(MembersResult member)
        {
            Id = member.id.ToString();
            Name = member.name;
            Bio = member.bio;
            Facebook = member.other_services?.facebook?.identifier;
            Linkedin = member.other_services?.linkedin?.identifier;
            Twitter = member.other_services?.twitter?.identifier;
            ImageUrl = member?.photo?.highres_link;
        }

        public MemberViewModel()
        {
            
        }

        public MemberViewModel(Attendance attendance)
        {
            Id = attendance.Member.id.ToString();
            Name = attendance.Member.name;
            ImageUrl = attendance.Member?.photo?.highres_link;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSpeaker { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
    }
}