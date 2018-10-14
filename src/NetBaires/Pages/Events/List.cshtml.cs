using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.Pages.Events
{

    public class ListModel : PageModel
    {
        private readonly IMeetupService _meetupService;

        public ListModel(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }
        public async Task OnGet()
        {
            var events = await _meetupService.GetEvents(500);
            var eventsToAdd = events?.Select(x =>
                new EventViewModel(x))
                .ToList()
                .OrderByDescending(x=> x.Date);
            Events.AddRange(eventsToAdd);

        }
        [BindProperty]
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Movie.Add(Movie);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}