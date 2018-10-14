using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetBaires.Data;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.Pages.Speakers
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public MemberViewModel Speaker { get; set; } = new MemberViewModel();
        [BindProperty]
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();

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
            var eventDetail = await _meetupService.GetMemberDetail(id);
            var speakerDb = _context.Speakers.Include(x => x.Events)
                                             .ThenInclude(x=> x.Event)
                                             .FirstOrDefault(x => x.Id == id);
            Speaker = new MemberViewModel(eventDetail);
            Events = new List<EventViewModel>();
            foreach (var eventDbSpeaker in speakerDb.Events)
                Events.Add(new EventViewModel(eventDbSpeaker.Event));

        }
    }
}