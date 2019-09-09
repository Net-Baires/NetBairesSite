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
        public List<SponsorViewModel> Sponsors { get; set; } = new List<SponsorViewModel>();
        public List<PhotoViewModel> Photos { get; set; } = new List<PhotoViewModel>();
        public EventViewModel Event { get; set; }
        public GroupViewModel Group { get; set; }

        public List<string> SpeakersToShow { get; set; } = new List<string>();
        public List<MemberViewModel> LeadsToShow { get; set; } = new List<MemberViewModel>();


        public IndexModel(IMeetupService meetupService, ApplicationDbContext context)
        {
            _meetupService = meetupService;
            _context = context;
        }
        public async Task OnGet()
        {
            var idsLeadsToShow = (await _meetupService.GetLeads())?.ToList().Select(x => x.id.ToString()).ToList();
            if (idsLeadsToShow.Any())
            {
                var memebers = await _meetupService.GetMembersDetail(idsLeadsToShow);
                foreach (var speaker in memebers)
                    LeadsToShow.Add(new MemberViewModel(speaker));
            }
            var nextEvent = await _meetupService.GetEvents(5, "upcoming");
            if (nextEvent.Any())
                Event = new EventViewModel(nextEvent.LastOrDefault());
            var SpesakersToShow = _context.Speakers.Include(x => x.Events).Where(x => x.Events.Any())
                                          .OrderByDescending(x => x.Events.Count)
                                          ?.ToList();
           SpeakersToShow = _context.Speakers.Where(x => x.Events.Any())
                                                .OrderByDescending(x => x.Events.Count)
                                                .Take(6)
                                                ?.ToList()
                                                .Select(x => x.Id).ToList();
            var events = await _meetupService.GetEvents(5);
            var eventsToAdd = events?.Select(x =>
                    new EventViewModel(x))
                .ToList()
                .OrderByDescending(x => x.Date);
            Events.AddRange(eventsToAdd);
            Photos = (await _meetupService.GetPhotos(events.Select(x => x.id).ToList(), 9)).Select(x => new PhotoViewModel(x)).ToList();
            var groupDetail = await _meetupService.GroupDetail();
            if (groupDetail.results != null)
            {
                Group = new GroupViewModel(groupDetail.results.FirstOrDefault());
                Sponsors = _context.Sponsors.ToList()?.Select(x => new SponsorViewModel(x)).ToList();
            }
        }



        public void OnPostContactUs(ContactUsViewModel contactUs)
        {

        }
    }
}
