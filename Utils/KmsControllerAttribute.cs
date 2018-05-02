using System;
using Microsoft.AspNetCore.Mvc.Routing;

namespace kms.Utils
{
    public class KmsControllerAttribute : Attribute, IRouteTemplateProvider
    {
        public string Template => "api/[controller]";

        public int? Order { get; set; }

        public string Name { get; set; }
    }
}
