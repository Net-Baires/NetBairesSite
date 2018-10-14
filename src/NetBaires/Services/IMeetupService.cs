using System.Collections.Generic;
using System.Threading.Tasks;
using NetBaires.Models.Meetup;

namespace NetBaires.Services
{
    public interface IMeetupService
    {
        Task<List<EventDetail>> GetEvents(int count, List<string> only = null);
        Task<EventDetail> GetEventDetail(string id);
        Task<MemberDetail> GetMemberDetail(string memberId);
        Task<List<MembersResult>> GetMembersDetail(List<string> membersId);
        Task<List<Attendance>> GetAttendance(string eventId, int page);
        Task<GroupDetail> GroupDetail();
        Task<List<MemberDetail>> GetLeads();
        Task<List<PhotoDetail>> GetPhotos(List<string> eventsIds = null);
    }
}