using AviaMare.Data.Repositories;
using AviaMare.Services;
using Enums.Users;
using System.Globalization;

namespace AviaMare.CustomMiddleWares
{
    public class LocalizationMiddleWare
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleWare(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var authService = context.RequestServices.GetRequiredService<AuthService>();
            var userRepositoryReal = context.RequestServices.GetRequiredService<IUserRepositryReal>();

            if (authService.IsAuthenticated())
            {
                var user = userRepositoryReal.Get(authService.GetUserId()!.Value)!;
                SwitchLanguage(user.Language);
                await _next.Invoke(context);
                return;
            }

            var langFromCookies = context.Request.Cookies["lang"];
            if (langFromCookies != null)
            {
                var lang = Enum.Parse<Language>(langFromCookies);
                SwitchLanguage(lang);
                await _next.Invoke(context);
                return;
            }

            if (context.Request.Headers.ContainsKey("accept-language"))
            {
                var langFromHeader = context.Request.Headers["accept-language"].FirstOrDefault();
                if (langFromHeader is not null)
                {
                    var localeStrCode = langFromHeader.Substring(0, 5);
                    var culture = new CultureInfo(localeStrCode);
                    SwitchLanguage(culture);
                    await _next.Invoke(context);
                    return;
                }

            }

            await _next.Invoke(context);
        }
        private void SwitchLanguage(Language language)
        {
            CultureInfo culture;

            switch (language)
            {
                case Language.Ru:
                    culture = new CultureInfo("ru-RU");
                    break;
                case Language.En:
                    culture = new CultureInfo("en-US");
                    break;
                default:
                    throw new Exception("Unknown languge");
            }

            SwitchLanguage(culture);
        }
        private void SwitchLanguage(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

    }
}

