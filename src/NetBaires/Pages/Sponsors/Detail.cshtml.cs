using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetBaires.Data;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.Pages.Sponsors
{
    public class DetailModel : PageModel
    {
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
        public List<PhotoViewModel> Photos { get; set; }
        public SponsorViewModel Sponsor { get; set; }


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
            var events = _context.Events.Include(x => x.Sponsors)
                .ThenInclude(s => s.Sponsor)
                .Where(x => x.Sponsors.Any(s => s.SponsorId == id))
                .ToList();
            Sponsor = (await _meetupService.GroupDetail()).results[0].sponsors?.Select(x => new SponsorViewModel(x))
                .ToList()
                .FirstOrDefault(s => s.Name == id);
            Events = events?.Select(x => new EventViewModel(x)).ToList();
            Photos = (await _meetupService.GetPhotos(events.Select(x => x.Id).ToList(), 50)).Select(x => new PhotoViewModel(x)).ToList();
        }

    }
}