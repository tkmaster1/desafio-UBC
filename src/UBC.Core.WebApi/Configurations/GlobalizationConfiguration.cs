﻿using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace UBC.Core.WebApi.Configurations
{
    public static class GlobalizationConfiguration
    {
        public static IApplicationBuilder UseGlobalizationConfig(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var defaultCulture = new CultureInfo("pt-BR");

            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            var suportCulture = new[] { defaultCulture };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = suportCulture,
                SupportedUICultures = suportCulture
            };

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}