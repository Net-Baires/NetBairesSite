using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.ViewComponents
{
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