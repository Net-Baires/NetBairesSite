using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.ViewComponents
{
    public class LastEventsViewComponent : ViewComponent
    {
        private readonly IMeetupService _meetupService;

        public LastEventsViewComponent(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var allEvents = await _meetupService.GetEvents(999, new List<string> {"time"});

            var events = await _meetupService.GetEvents(5);
            var eventsToShow = events?.Select(x => new EventViewModel(x)).ToList();
            return View(eventsToShow);
        }
    }

    public class EventsListViewComponent : ViewComponent
    {
        private readonly IMeetupService _meetupService;

        public EventsListViewComponent(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<EventViewModel> events)
        {
            return View(events);
        }
    }
}