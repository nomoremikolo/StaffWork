namespace StaffWork.Server.JwtAuthorization.Interfaces
{
    public interface ICookiesHelper
    {
        void SetTokenCookie(string token, HttpContext context);
        string? GetTokenCookie(HttpContext context);
    }
}
