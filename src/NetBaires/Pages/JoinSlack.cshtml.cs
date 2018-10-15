using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetBaires.Data;
using NetBaires.Models.Meetup;
using Polly;

namespace NetBaires.Pages
{
    public class JoinSlackModel : PageModel
    {
        private readonly HttpClient _client;
        [BindProperty]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Ok { get; set; }
        public string Error { get; set; }
        public JoinSlackModel(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://slack.com/api/");
        }
        public void OnGet()
        {
        }
        public async Task OnPostInvite()
        {
            if (ModelState.IsValid)
            {

                var dict = new Dictionary<string, string>
                {
                    {"token", "xoxp-76018477639-75960322915-79210432739-a4814114a4"},
                    {"email", Email}
                };
                var client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/users.admin.invite")
                {
                    Content = new FormUrlEncodedContent(dict)
                };
                req.Headers.Add("Accept", "application/json");
                var result = await client.SendAsync(req);
                var slackResponse = await result.Content.ReadAsAsync<SlackInviteResponse>();
                Ok = slackResponse.ok;
                if (!Ok)
                {
                    switch (slackResponse.error)
                    {
                        case "already_invited":
                            Error = "Este email ya se encuentra invitado a nuestro Slack";
                            break;
                        case "invalid_email":
                            Error = "El email que esta ingresando es incorrecto";
                            break;
                        case "already_in_team":
                            Error = "Este email, ya se encuentra en nuestro Slack";
                            break;
                        default:
                            Error = "Ocurrio un Error, Notifique a nuestro admin!";
                            break;
                    }
                }

            }
        }
    }

    public class SlackInviteResponse
    {
        public bool ok { get; set; }
        public string error { get; set; }
    }

}
