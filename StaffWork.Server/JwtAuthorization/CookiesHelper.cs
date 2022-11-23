using StaffWork.Server.JwtAuthorization.Interfaces;

namespace StaffWork.Server.JwtAuthorization
{
    public class CookiesHelper : ICookiesHelper
    {
        public void SetTokenCookie(string token, HttpContext context)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMonths(1),
                SameSite = SameSiteMode.None,
                Secure = true,
            };
            context.Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        public string? GetTokenCookie(HttpContext context)
        {
            return context.Request.Cookies["refreshToken"];
        }
    }
}
