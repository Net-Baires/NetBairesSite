using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetBaires.Data;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.ViewComponents
{
    public class OrganizersViewComponent : ViewComponent
    {
        private readonly IMeetupService _meetupService;
        private readonly ApplicationDbContext _context;

        public OrganizersViewComponent(IMeetupService meetupService, ApplicationDbContext context)
        {
            _meetupService = meetupService;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var speakers = _context.Speakers.ToList();
            var speakersToShow = new List<MemberViewModel>();
            var memebers = await _meetupService.GetLeads();
            foreach (var speaker in memebers)
                speakersToShow.Add(new MemberViewModel(speaker));
            return View(speakersToShow);
        }
    }
}