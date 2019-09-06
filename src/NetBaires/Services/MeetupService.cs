using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NetBaires.Models.Meetup;
using Polly;
using Polly.Registry;

namespace NetBaires.Services
{
    public class MeetupService : IMeetupService
    {
        public static AuthToken Token { get; set; }
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
            if (Token == null)
            {
                Token = new AuthToken();
                Token.Token = _meetupOptions.Value.Token;
                Token.RefreshToken = _meetupOptions.Value.RefreshToken;
            }
        }

        public async Task RefreshTokenAsync()
        {
            var contentToSend = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", _meetupOptions.Value.ClientId),
                new KeyValuePair<string, string>("client_secret", _meetupOptions.Value.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", Token.RefreshToken),
            };
            using (var content = new FormUrlEncodedContent(contentToSend))
            {
                content.Headers.Clear();
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                HttpResponseMessage response = await _client.PostAsync("https://secure.meetup.com/oauth2/access", content);

                var value = await response.Content.ReadAsAsync<RefreshTokenResponse>();
                Token.Token = value.access_token;
                Token.RefreshToken = value.refresh_token;
            }

        }
        public async Task<EventDetail> GetEventDetail(string id)
        {
            var result = await GetCallApi($"{GroupName}/events/{id}?&photo-host=public&fields=featured_photo,photo_album");
            return await result.Content.ReadAsAsync<EventDetail>() ?? new EventDetail(); ;
        }
        public async Task<List<EventDetail>> GetEvents(int count, string status = "past,upcoming", List<string> only = null)
        {    
            var urlRequest =
                $"{GroupName}/events?key={_meetupOptions.Value.Key}&photo-host=public&page={count}&fields=featured_photo&desc=true&status={status}";
            if (only != null)
                urlRequest += $"&only={string.Join(",", only)}";

            HttpResponseMessage result = await GetCallApi(urlRequest);
            return await result.Content.ReadAsAsync<List<EventDetail>>() ?? new List<EventDetail>();
        }

        private async Task<HttpResponseMessage> GetCallApi(string urlRequest)
        {
            Task<HttpResponseMessage> request = GenerateGetCall(urlRequest);

            var result = await _cachePolicy.ExecuteAsync(() => request);
            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await RefreshTokenAsync();
                request = GenerateGetCall(urlRequest);
                return await _cachePolicy.ExecuteAsync(() => request);
            }
            return result;
        }

        private Task<HttpResponseMessage> GenerateGetCall(string urlRequest)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Token);
            var request = _client.GetAsync(urlRequest);
            return request;
        }

        public async Task<List<PhotoDetail>> GetPhotos(List<string> eventsIds = null, int count = 20)
        {
            var urlRequest =
                $"/2/photos?key={_meetupOptions.Value.Key}&group_urlname={GroupName}&page={count}";
            if (eventsIds != null)
                urlRequest += $"&event_id={string.Join(",", eventsIds)}";
            var eventsResults = await GetCallApi(urlRequest);
            return (await eventsResults.Content.ReadAsAsync<PhotoDetailContainer>())?.Results ?? new List<PhotoDetail>();
        }
        public async Task<MemberDetail> GetMemberDetail(string memberId)
        {
            var eventsResults = await _client.GetAsync(
                $"2/member/{memberId}?key={_meetupOptions.Value.Key}&group_urlname=ny-tech&sign=true");
            return await eventsResults.Content.ReadAsAsync<MemberDetail>() ?? new MemberDetail(); ;
        }

        public async Task<List<MembersResult>> GetMembersDetail(List<string> membersId)
        {
            var eventsResults = await GetCallApi(
                $"/2/members?key={_meetupOptions.Value.Key}&member_id={string.Join(",", membersId)}&page=9999");
            var result = (await eventsResults.Content.ReadAsAsync<MembersRoot>())?.Results?.ToList();
            return result ?? new List<MembersResult>();
        }
        public async Task<List<MemberDetail>> GetLeads()
        {
            var eventsResults = await GetCallApi(
                $"{GroupName}/members?key={_meetupOptions.Value.Key}&&role=leads&page=9999");
            return await eventsResults.Content.ReadAsAsync<List<MemberDetail>>() ?? new List<MemberDetail>();

        }

        public async Task<List<Attendance>> GetAttendance(string eventId, int page)
        {
            var eventsResults = await GetCallApi(
                $"{GroupName}/events/{eventId}/attendance?key={_meetupOptions.Value.Key}&sign=true&photo-host=public&page={page}");
            return await eventsResults.Content.ReadAsAsync<List<Attendance>>() ?? new List<Attendance>();
        }
        public async Task<GroupDetail> GroupDetail()
        {
            var eventsResults = await GetCallApi(
                $"/2/groups?key={_meetupOptions.Value.Key}&photo-host=public&group_urlname={GroupName}&fields=past_event_count,sponsors,organizer,members,member_cap,membership_dues,group_photo,photos&page=20");
            return await eventsResults.Content.ReadAsAsync<GroupDetail>() ?? new GroupDetail();
        }

    }
}