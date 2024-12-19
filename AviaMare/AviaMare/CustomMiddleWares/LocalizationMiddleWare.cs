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
                CultureInfo culture;
                switch (user.Language)
                {
                    case Language.Ru:
                        culture = new CultureInfo("ru-RU");
                        break;
                    case Language.En:
                        culture = new CultureInfo("en-US");
                        break;
                    default:
                        throw new Exception("Unknow language");
                }

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            await _next.Invoke(context);
        }
    }
}
