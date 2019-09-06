using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NetBaires.Data;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.ViewComponents
{
    public class MembersCarouselViewComponent : ViewComponent
    {
        private readonly IMeetupService _meetupService;
        private readonly ApplicationDbContext _context;

        public MembersCarouselViewComponent(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        public IViewComponentResult Invoke(List<MemberViewModel> speakers) =>
            View(speakers);
    }
}