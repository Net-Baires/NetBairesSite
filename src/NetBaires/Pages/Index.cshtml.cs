using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetBaires.Data;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.Pages
{


    public class IndexModel : PageModel
    {
        private readonly IMeetupService _meetupService;
        private readonly ApplicationDbContext _context;
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
        public List<PhotoViewModel> Photos { get; set; } = new List<PhotoViewModel>();
        public EventViewModel Event{ get; set; }
        public List<string> SpeakersToShow { get; set; } = new List<string>();
        public List<string> LeadsToShow { get; set; } = new List<string>();


        public IndexModel(IMeetupService meetupService, ApplicationDbContext context)
        {
            _meetupService = meetupService;
            _context = context;
        }
        public async Task OnGet()
        {
            LeadsToShow = (await _meetupService.GetLeads())?.ToList().Select(x => x.id.ToString()).ToList();

            var nextEvent = await _meetupService.GetEvents(1);
            if (nextEvent.Any())
                Event = new EventViewModel(nextEvent.FirstOrDefault());
            var SpesakersToShow = _context.Speakers.Include(x=> x.Events).Where(x => x.Events.Any())
                .OrderByDescending(x => x.Events.Count)
                ?.ToList();
            SpeakersToShow = _context.Speakers.Where(x => x.Events.Any())
                .OrderByDescending(x => x.Events.Count)
                .Take(6)
                ?.ToList()
                .Select(x => x.Id).ToList();

            var events = await _meetupService.GetEvents(500);
            var eventsToAdd = events?.Select(x =>
                    new EventViewModel(x))
                .ToList()
                .OrderByDescending(x => x.Date);
            Events.AddRange(eventsToAdd);
            var lastEvents = (await _meetupService.GetEvents(5));
            Photos = (await _meetupService.GetPhotos(lastEvents.Select(x => x.id).ToList(),9)).Select(x => new PhotoViewModel(x)).ToList();
        }


        public void OnPostContactUs(ContactUsViewModel contactUs)
        {

        }
    }
}
