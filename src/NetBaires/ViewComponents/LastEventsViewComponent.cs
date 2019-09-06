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

        public async Task<IViewComponentResult> InvokeAsync(List<EventViewModel> events)=>
            View(events);
    }
}