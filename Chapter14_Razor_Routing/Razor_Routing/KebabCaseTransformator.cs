using System.Text.RegularExpressions;

namespace Razor_Routing
{
    public class KebabCaseTransformator : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value is null)
            {
                return null;
            }
            return Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower() + "/";
        }
    }
}
