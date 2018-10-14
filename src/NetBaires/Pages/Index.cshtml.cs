using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.Pages
{


    public class IndexModel : PageModel
    {
        private readonly IMeetupService _meetupService;
        public List<EventViewModel> Events { get; set; } = new List<EventViewModel>();
        public List<PhotoViewModel> Photos { get; set; } = new List<PhotoViewModel>();

        public IndexModel(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }
        public async Task OnGet()
        {


    

            var events = await _meetupService.GetEvents(500);
            var eventsToAdd = events?.Select(x =>
                    new EventViewModel(x))
                .ToList()
                .OrderByDescending(x => x.Date);
            Events.AddRange(eventsToAdd);
            var lastEvents = (await _meetupService.GetEvents(5));
            Photos = (await _meetupService.GetPhotos(lastEvents.Select(x => x.id).ToList())).Select(x => new PhotoViewModel(x)).ToList();
        }


        public async Task OnPostContactUs(ContactUsViewModel contactUs)
        {

        }
    }
}
