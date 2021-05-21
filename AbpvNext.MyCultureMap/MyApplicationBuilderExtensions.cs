using System;
using Microsoft.AspNetCore.Builder;

namespace AbpvNext.MyCultureMap
{
    public static class MyApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMyRequestLocalization(this IApplicationBuilder app,
            Action<RequestLocalizationOptions> optionsAction = null)
        {
            return app.UseAbpRequestLocalization(options =>
            {
                options.RequestCultureProviders.Insert(0, new MyRequestCultureProvider());
                optionsAction?.Invoke(options);
            });
        }
    }
}