﻿using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace lab.azure_active_directory_auth.Helpers
{
    public class FeaturesViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(FeaturesViewLocationExpander);
        }

        public IEnumerable<string> ExpandViewLocations(
                  ViewLocationExpanderContext context,
                  IEnumerable<string> viewLocations)
        {
            List<string> viewLocationFormats = new List<string>
            {
                "~/Views/Shared/{0}.cshtml",
            };
            viewLocations.ToList().ForEach(X => {
                viewLocationFormats.Add(X);
            });
            return viewLocationFormats;
        }
    }
}
