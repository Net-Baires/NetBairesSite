using Microsoft.AspNetCore.Mvc;
using NetBaires.Models;

namespace NetBaires.ViewComponents
{
    public class ContactUsViewComponent : ViewComponent
    {
        [BindProperty]
        public ContactUsViewModel ContactUs { get; set; } = new ContactUsViewModel();
        public ContactUsViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View(ContactUs);
        }
    }
    public class BreadcrumbViewComponent : ViewComponent
    {
        public BreadcrumbViewModel Breadcrumb { get; set; }
        public BreadcrumbViewComponent()
        {

        }

        public IViewComponentResult Invoke(BreadcrumbViewModel breadcrumbViewModel)
        {
            return View(breadcrumbViewModel);
        }
    }

    public class BreadcrumbViewModel
    {
        public string Title { get; set; }
        public string ActualPage { get; set; }

        public BreadcrumbViewModel(string title, string actualPage)
        {
            Title = title;
            ActualPage = actualPage;
        }
    }


}