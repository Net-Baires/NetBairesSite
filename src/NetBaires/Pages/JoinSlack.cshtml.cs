using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace NetBaires.Pages
{
    public class JoinSlackModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IOptions<SlackEndPoint> _slackEndPoint;

        [BindProperty]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool Ok { get; set; }
        public string Error { get; set; }
        public JoinSlackModel(IHttpClientFactory httpClientFactory,
            IOptions<SlackEndPoint> slackEndPoint)
        {
            _client = httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://slack.com/api/");
            _slackEndPoint = slackEndPoint;
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
                    {"token", _slackEndPoint.Value.Token},
                    {"email", Email}
                };
                var client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, _slackEndPoint.Value.Url)
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
                else
                {

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
