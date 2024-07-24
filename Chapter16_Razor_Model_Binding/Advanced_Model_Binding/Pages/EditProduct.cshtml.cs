using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Advanced_Model_Binding.Pages
{
    [IgnoreAntiforgeryToken]
    public class EditProductModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        public required string result { get; set; }
        public readonly ProductService _ProductService; 
        public EditProductModel(ProductService productService)
        {
            _ProductService = productService;
        }
        public void OnGet(int id)
        {
            //this should result in an Exception, because get does not support propertybinding by body by default
            result = $"Id: {id}; Input.Name: {Input.Name}, Input.Price: {Input.Price}";
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                result = $"invalid modelstate Id: {id}; Input.Name: {Input.Name}, Input.Price: {Input.Price}";
                return Page();
            }
            result = $"VALID modelstate Id: {id}; Input.Name: {Input.Name}, Input.Price: {Input.Price}";
            return Page();
        }
        public class InputModel
        {
            [Required]
            public string Name { get; set; }
            [Range(0, int.MaxValue)]
            public decimal Price { get; set; }
            //public InputModel() { }
        }

    }
}
