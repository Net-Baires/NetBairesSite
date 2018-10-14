using Microsoft.AspNetCore.Mvc;
using NetBaires.Models;

namespace NetBaires.ViewComponents
{
    public class ContactUsViewComponent : ViewComponent
    {
        [BindProperty]
        public ContactUsViewModel ContactUs { get; set; } = new ContactUsViewModel();
        public ContactUsViewComponent( )
        {
         
        }

        public IViewComponentResult Invoke()
        {
            return View(ContactUs);
        }
    }
}