using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetBaires.Data;

namespace NetBaires.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty] public string Name { get; set; } = "";
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Subject { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public void OnGet()
        {
        }
        public async Task OnPostSubmmit()
        {
          

        }
        
    }
}
