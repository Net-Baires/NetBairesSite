using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetBaires.Data;
using NetBaires.Services;

namespace NetBaires.Pages.Speakers
{
    public class ListModel : PageModel
    {
        public List<string> SpeakersToShow { get; set; } = new List<string>();
        private readonly IMeetupService _meetupService;
        private readonly ApplicationDbContext _context;

        public ListModel(IMeetupService meetupService, ApplicationDbContext context)
        {
            _meetupService = meetupService;
            _context = context;
        }

        public void OnGet()
        {
            SpeakersToShow = _context.Speakers.Where(x => x.Events.Any())?.ToList().Select(x => x.Id).ToList();

        }

    }
}