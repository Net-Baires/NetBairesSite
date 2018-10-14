using System.Linq;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lastEvents = (await _meetupService.GetEvents(5));
            var photos = (await _meetupService.GetPhotos(lastEvents.Select(x=> x.id).ToList())).Select(x=> new PhotoViewModel(x)).ToList();
            return View(photos);
        }
    }
}