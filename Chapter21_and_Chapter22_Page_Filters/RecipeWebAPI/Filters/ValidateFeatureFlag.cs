using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace RecipeWebAPI.Filters
{
    public class ValidateFeatureFlag : Attribute, IResourceFilter
    {
        public bool Enabled { get; set; }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //nothing
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //only need a badrequestresult here due to no need to use the modelstate
            //this shortcircuits the filter pipeline
            if (!Enabled) context.Result = new BadRequestResult();
        }
    }
}
