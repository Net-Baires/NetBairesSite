using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetBaires.Data;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.ViewComponents
{
    public class SpeakersViewComponent : ViewComponent
    {
        private readonly IMeetupService _meetupService;
        private readonly ApplicationDbContext _context;

        public SpeakersViewComponent(IMeetupService meetupService, ApplicationDbContext context)
        {
            _meetupService = meetupService;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var speakersToShow = new List<MemberViewModel>();

            var speakers = _context.Speakers.ToList();
            if (speakers.Any())
            {
                var memebers = await _meetupService.GetMembersDetail(speakers?.Select(x => x.Id).ToList());
                foreach (var speaker in memebers)
                    speakersToShow.Add(new MemberViewModel(speaker));
            }
            return View(speakersToShow);
        }
    }
}