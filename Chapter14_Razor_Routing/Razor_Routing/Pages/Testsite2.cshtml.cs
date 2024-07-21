using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor_Routing.Pages
{
    public class Testsite2Model : PageModel
    {
        public string LinkToTestsite { get; set; } = string.Empty;
        public readonly LinkGenerator _link;
        public Testsite2Model(LinkGenerator link)
        {
            _link = link;
        }
        public ActionResult OnGet()
        {
            LinkToTestsite = Url.Page("testsite", new { someparameter = "1", someparameter2 = "2"});
            var url1 = _link.GetPathByPage(HttpContext, "testsite", values: new {someparameter = 1, someparameter2 = 2});
            //if no httpcontext is provided it is necessary to create an absolute path by starting with "/"
            //httpcontext contains a lot of information. interesting is the information regarding the incoming request, which i.e. includes the host and scheme
            var url2 = _link.GetPathByPage("/testsite", values: new { someparameter = 1, someparameter2 = 2 });
            var url3 = _link.GetUriByPage(
                page: "/testsite",
                handler: null,
                values: new { someparameter = 1, someparameter2 = 2 },
                scheme: "https",
                host: new HostString("example.com")
                );
            return Page();
        }
    }
}
