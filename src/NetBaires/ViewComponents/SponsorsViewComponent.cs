using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.ViewComponents
{
    public class SponsorsViewComponent : ViewComponent
    {
        private readonly IMeetupService _meetupService;

        public SponsorsViewComponent(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<SponsorViewModel> sponsors)
        {
           
            return View(sponsors);
        }
    }
}