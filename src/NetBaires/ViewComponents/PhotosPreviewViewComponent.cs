using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetBaires.Models;
using NetBaires.Services;

namespace NetBaires.ViewComponents
{
    public class PhotosPreviewViewComponent : ViewComponent
    {
        private readonly IMeetupService _meetupService;

        public PhotosPreviewViewComponent(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<PhotoViewModel> photos)
        {
        
            return View(photos);
        }
    }
}