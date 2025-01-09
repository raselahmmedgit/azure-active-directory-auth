using Microsoft.AspNetCore.Http;
using System;

namespace lab.azure_active_directory_auth.Helpers
{
    public class SessionCookiesHelper
    {
        public static void SetCookiees(HttpResponse response, string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            response.Cookies.Append(key, value, option);
        }
        public static string ReadCookies(HttpRequest request, string key)
        {
            return request.Cookies[key];
        }
    }
}
