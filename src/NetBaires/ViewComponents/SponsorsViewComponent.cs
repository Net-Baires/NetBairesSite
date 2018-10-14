using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetBaires.Models;
using NetBaires.Pages;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var groupDetail = await _meetupService.GroupDetail();
            var sponsors = (groupDetail.results.First().sponsors)?.Select(x => new SponsorViewModel(x)).ToList();
            return View(sponsors);
        }
    }
}