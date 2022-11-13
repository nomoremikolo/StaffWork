using BusinessLogic.Models;

namespace StaffWork.Server.JwtAuthorization.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateJwtAccessToken(UserModel user);
        public int? ValidateJwtAccessToken(string token);
        public string GenerateJWTRefreshToken();
        public string? ValidateJWTRefreshToken(string token);
    }
}
