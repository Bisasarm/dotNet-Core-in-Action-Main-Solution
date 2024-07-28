using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forms_and_Tag_Helpers.Pages
{
    public class CurrencyConverterModel : PageModel
    {
        [BindProperty]
        public InputModel? Input { get; set; }
        public List<SelectListItem> CurrencyOut { get; set; }
        public List<SelectListItem> CurrencyIn { get; set; }
        public CurrencyConverterModel()
        {
            SelectListGroup OptGroupEuropean = new SelectListGroup { Name = "European" };
            SelectListGroup OptGroupAsian = new SelectListGroup { Name = "Asian" };
            SelectListGroup OptGroupAmerican = new SelectListGroup { Name = "American" };             
            CurrencyIn = new List<SelectListItem>
            {
                    new SelectListItem{Value = "euro", Text = "€", Group=OptGroupEuropean},
                    new SelectListItem{Value = "dollar", Text = "$", Group=OptGroupAmerican},
                    new SelectListItem{Value = "yen", Text = "Y", Group=OptGroupAsian}
            };
            CurrencyOut = new List<SelectListItem>
            {
                    new SelectListItem{Value = "euro", Text = "€", Group=OptGroupEuropean},
                    new SelectListItem{Value = "dollar", Text = "$", Group=OptGroupAmerican},
                    new SelectListItem{Value = "yen", Text = "Y", Group=OptGroupAsian}
            };
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            //do something
        }
        public class InputModel
        {
            public string SelectedValue1 { get; set; }
            public string SelectedValue2 { get; set; }
            //public IEnumerable<string> MultipleSelectedValue { get; set; }
        }
    }
}
