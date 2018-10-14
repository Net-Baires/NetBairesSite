using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetBaires.Data;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.Pages.Events
{
    [Authorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public EventViewModel Event { get; set; } = new EventViewModel();
        [BindProperty]
        public List<MemberViewModel> Speakers { get; set; } = new List<MemberViewModel>();
        [BindProperty]
        public List<SponsorViewModel> Sponsors { get; set; } = new List<SponsorViewModel>();

        private readonly IMeetupService _meetupService;
        private readonly ApplicationDbContext _context;

        public EditModel(IMeetupService meetupService, ApplicationDbContext context)
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
            var eventDetail = await _meetupService.GetEventDetail(id);
            var eventDb = _context.Events.Include(x => x.Speakers)
                                         .Include(x => x.Sponsors)
                                         .ThenInclude(s => s.Sponsor)
                                         .FirstOrDefault(x => x.Id == id);
            Event = new EventViewModel(eventDetail);

            Speakers = (await _meetupService.GetAttendance(id, 20))?.Select(x => new MemberViewModel(x)).ToList();
            Sponsors = (await _meetupService.GroupDetail()).results[0].sponsors?.Select(x => new SponsorViewModel(x))
                .ToList();
            if (eventDb != null)
            {
                Speakers.Where(x => eventDb.Speakers.Any(s => s.SpeakerId == x.Id))?.ToList()
                    .ForEach(s => s.IsSpeaker = true);
           
                Speakers.Where(x => eventDb.Speakers.Any(s => s.SpeakerId == x.Id))?.ToList()
                    .ForEach(s => s.IsSpeaker = true);
                Sponsors.Where(x => eventDb.Sponsors.Any(s => s.SponsorId == x.Name))?.ToList()
                    .ForEach(s => s.Selected = true);
            }
        }



        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var eventToModifier = _context.Events.Include(x => x.Speakers)
                                                 .Include(x => x.Sponsors)
                                                 .ThenInclude(s => s.Sponsor)
                                                 .FirstOrDefault(x => x.Id == Event.Id);
            if (eventToModifier == null)
            {
                eventToModifier = new EventDb(Event);
                _context.Events.Add(eventToModifier);
            }

            SetSpeakerContext(eventToModifier);
            SetSponsorContext(eventToModifier);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }

        private void SetSpeakerContext(EventDb eventToModifier)
        {
            var speakersSelected = Speakers.Where(x => x.IsSpeaker).ToList();
            var speakersToAddToDb = speakersSelected.Where(x => !_context.Speakers.Any(s => x.Id == s.Id)).ToList();
            speakersToAddToDb?.ForEach(x =>
            {
                _context.Speakers.Add(new Speaker
                {
                    Id = x.Id
                });
            });

            var speakerToAddFromEvent = speakersSelected.Where(x => !eventToModifier.Speakers.Any(s => s.SpeakerId == x.Id))
                .ToList();

            speakerToAddFromEvent?.ForEach(x =>
            {
                eventToModifier.Speakers.Add(new SpeakerEvent
                {
                    SpeakerId = x.Id,
                    Event = eventToModifier
                });
            });

            eventToModifier.Speakers.Where(x => !speakersSelected.Any(s => s.Id == x.SpeakerId))
                   .ToList()
                   ?.ForEach(x => eventToModifier.Speakers.Remove(x));
        }

        private void SetSponsorContext(EventDb eventToModifier)
        {
            var sponsorsSelected = Sponsors.Where(x => x.Selected).ToList();
            var sponsorsToAddToDb = sponsorsSelected.Where(x => !_context.Sponsors.Any(s => x.Name == s.Name)).ToList();
            sponsorsToAddToDb?.ForEach(x =>
            {
                _context.Sponsors.Add(new SponsorDb
                {
                    Name =  x.Name,
                    Id =  x.Name
                });
            });

            var sponsorsToAddFromEvent = sponsorsSelected.Where(x => !eventToModifier.Sponsors.Any(s => s.Sponsor.Name == x.Name))
                .ToList();

            sponsorsToAddFromEvent?.ForEach(x =>
            {
                eventToModifier.Sponsors.Add(new SponsorEvent
                {
                    SponsorId = x.Name,
                    Event = eventToModifier
                });
            });

            eventToModifier.Sponsors.Where(x => !sponsorsSelected.Any(s => s.Name == x.SponsorId))
                .ToList()
                ?.ForEach(x => eventToModifier.Sponsors.Remove(x));
        }
    }
}