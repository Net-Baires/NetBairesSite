using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetBaires.Data;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.Pages.Events
{
    public class DetailModel : PageModel
    {
        public EventViewModel Event { get; set; } = new EventViewModel();
        public List<MemberViewModel> Speakers { get; set; } = new List<MemberViewModel>();
        public List<PhotoViewModel> Photos { get; set; }

        private readonly IMeetupService _meetupService;
        private readonly ApplicationDbContext _context;

        public DetailModel(IMeetupService meetupService, ApplicationDbContext context)
        {
            _meetupService = meetupService;
            _context = context;
        }

        public async Task OnGet(string id)
        {
            await GenerateDataToShow(id);
        }

        private async Task GenerateDataToShow(string id)
        {
            Photos = (await _meetupService.GetPhotos(new List<string> { id }, 50)).Select(x => new PhotoViewModel(x)).ToList();

            var eventDetail = await _meetupService.GetEventDetail(id);
            var eventDb = _context.Events.Include(x => x.Speakers).FirstOrDefault(x => x.Id == id);
            Event = new EventViewModel(eventDetail);
            Speakers = new List<MemberViewModel>();
            if (eventDb != null)
                foreach (var eventDbSpeaker in eventDb.Speakers)
                    Speakers.Add(new MemberViewModel(await _meetupService.GetMemberDetail(eventDbSpeaker.SpeakerId)));

        }

    }
}