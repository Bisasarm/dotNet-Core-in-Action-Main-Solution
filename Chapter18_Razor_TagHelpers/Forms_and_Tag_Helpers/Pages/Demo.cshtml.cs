using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Forms_and_Tag_Helpers.Pages
{
    public class DemoModel : PageModel
    {
        [BindProperty]
        [StringLength(10, ErrorMessage ="Maximum Length is {1}")]
        [DisplayName("Your name")]
        public string FirstName { get; set; }
        [BindProperty]
        [StringLength(maximumLength: 10, MinimumLength = 1)]
        [DisplayName("Your Last Name")]
        public string LastName { get; set; }
        [EmailAddress]
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [Phone]
        [BindProperty]
        public string PhoneNo { get; set; } = string.Empty;
        public void OnGet()
        {
        }
        public void OnPost()
        {
            //does something
        }
    }
}
