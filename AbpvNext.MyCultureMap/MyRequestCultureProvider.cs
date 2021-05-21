using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AbpvNext.MyCultureMap
{
    internal class MyRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var option = httpContext.RequestServices.GetRequiredService<IOptions<MyCultureMapOptions>>().Value;

            var mapCultures = new List<StringSegment>();
            var mapUiCultures = new List<StringSegment>();

            var requestLocalizationOptionsProvider = httpContext.RequestServices.GetRequiredService<IAbpRequestLocalizationOptionsProvider>();
            var requestLocalizationOptions = await requestLocalizationOptionsProvider.GetLocalizationOptionsAsync();
            foreach (var provider in requestLocalizationOptions.RequestCultureProviders)
            {
                if (provider == this)
                {
                    continue;
                }

                var providerCultureResult = await provider.DetermineProviderCultureResult(httpContext);
                if (providerCultureResult == null)
                {
                    continue;
                }

                var providerCultures = providerCultureResult.Cultures.Where(x => x.HasValue);
                foreach (var providerCulture in providerCultures)
                {
                    // 替换http请求所携带的多语言
                    var cultureMapInfo = option.CulturesMaps.FirstOrDefault(x =>
                            x.SourceCultures.Contains(providerCulture.Value, StringComparer.OrdinalIgnoreCase));
                    var replaceCultureStringSegment =
                        new StringSegment(cultureMapInfo?.TargetCulture ?? providerCulture.Value);
                    if (!mapCultures.Contains(replaceCultureStringSegment))
                    {
                        mapCultures.Add(replaceCultureStringSegment);
                    }

                    var uiCultureMapInfo = option.UiCulturesMaps.FirstOrDefault(x =>
                        x.SourceCultures.Contains(providerCulture.Value, StringComparer.OrdinalIgnoreCase));
                    var replaceUiCultureStringSegment = new StringSegment(uiCultureMapInfo?.TargetCulture ?? providerCulture.Value);
                    if (!mapUiCultures.Contains(replaceUiCultureStringSegment))
                    {
                        mapUiCultures.Add(replaceUiCultureStringSegment);
                    }
                }
            }

            return new ProviderCultureResult(mapCultures, mapUiCultures);
        }
    }
}