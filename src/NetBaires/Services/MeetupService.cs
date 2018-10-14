using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NetBaires.Models.Meetup;
using Polly;
using Polly.Registry;

namespace NetBaires.Services
{
    public class MeetupService : IMeetupService
    {
        private const string GroupName = "net-baires";

        private readonly IOptions<MeetupEndPoint> _meetupOptions;
        private readonly HttpClient _client;
        private readonly IAsyncPolicy<HttpResponseMessage> _cachePolicy;

        public MeetupService(IOptions<MeetupEndPoint> meetupOptions,
            IPolicyRegistry<string> policyRegistry,
            IHttpClientFactory httpClientFactory)
        {
            _meetupOptions = meetupOptions;
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri(meetupOptions.Value.Url);
            _cachePolicy = policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>("myCachePolicy");

        }

        public async Task<EventDetail> GetEventDetail(string id)
        {
            var request = _client.GetAsync($"{GroupName}/events/{id}?key={_meetupOptions.Value.Key}&photo-host=public&fields=featured_photo,photo_album");
            var result = await _cachePolicy.ExecuteAsync(() => request);

            return await result.Content.ReadAsAsync<EventDetail>();
        }
        public async Task<List<EventDetail>> GetEvents(int count, List<string> only = null)
        {
            var urlRequest =
                $"{GroupName}/events?key={_meetupOptions.Value.Key}&photo-host=public&page={count}&fields=featured_photo&desc=true&status=past,upcoming";
            if (only != null)
                urlRequest += $"&only={string.Join(",", only)}";

            var request = _client.GetAsync(urlRequest);
            var result = await _cachePolicy.ExecuteAsync(() => request);

            return await result.Content.ReadAsAsync<List<EventDetail>>();
        }
    
        public async Task<List<PhotoDetail>> GetPhotos(List<string> eventsIds = null, int count = 20)
        {
            var urlRequest =
                $"/2/photos?key={_meetupOptions.Value.Key}&group_urlname={GroupName}&page={count}";
            if (eventsIds != null)
                urlRequest += $"&event_id={string.Join(",", eventsIds)}";
            var eventsResults = await _client.GetAsync(urlRequest);
            return (await eventsResults.Content.ReadAsAsync<PhotoDetailContainer>()).Results;
        }
        public async Task<MemberDetail> GetMemberDetail(string memberId)
        {
            var eventsResults = await _client.GetAsync(
                $"2/member/{memberId}?key={_meetupOptions.Value.Key}&group_urlname=ny-tech&sign=true");
            return await eventsResults.Content.ReadAsAsync<MemberDetail>();
        }

        public async Task<List<MembersResult>> GetMembersDetail(List<string> membersId)
        {
            var eventsResults = await _client.GetAsync(
                $"/2/members?key={_meetupOptions.Value.Key}&member_id={string.Join(",", membersId)}&page=9999");
            return (await eventsResults.Content.ReadAsAsync<MembersRoot>()).Results.ToList();
        }
        public async Task<List<MemberDetail>> GetLeads()
        {
            var eventsResults = await _client.GetAsync(
                $"{GroupName}/members?key={_meetupOptions.Value.Key}&&role=leads&page=9999");
            return await eventsResults.Content.ReadAsAsync<List<MemberDetail>>();
        }



        public async Task<List<Attendance>> GetAttendance(string eventId, int page)
        {
            var eventsResults = await _client.GetAsync(
                $"{GroupName}/events/{eventId}/attendance?key={_meetupOptions.Value.Key}&sign=true&photo-host=public&page={page}");
            return await eventsResults.Content.ReadAsAsync<List<Attendance>>();
        }
        public async Task<GroupDetail> GroupDetail()
        {
            var eventsResults = await _client.GetAsync(
                $"/2/groups?key={_meetupOptions.Value.Key}&photo-host=public&group_urlname={GroupName}&fields=past_event_count,sponsors,organizer,members,member_cap,membership_dues,group_photo,photos&page=20");
            return await eventsResults.Content.ReadAsAsync<GroupDetail>();
        }

    }
}