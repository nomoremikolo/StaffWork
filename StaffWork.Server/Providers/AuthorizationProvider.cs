using AutoMapper;
using BusinessLogic;
using BusinessLogic.Enums;
using BusinessLogic.Models;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils.Intarfaces;

namespace StaffWork.Server.Provider
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        private IUserDataProvider userDBProvider;
        private IJwtUtils jwtUtils;
        private IMapper mapper;
        private IHashHelper hashHelper;
        public AuthorizationProvider(IUserDataProvider userProvider, IJwtUtils jwtUtils, IMapper mapper, IHashHelper hashHelper)
        {
            this.userDBProvider = userProvider;
            this.jwtUtils = jwtUtils;
            this.mapper = mapper;
            this.hashHelper = hashHelper;
        }

        public AuthenticationResponseModel? Login(UserLoginModel user)
        {
            var response = new AuthenticationResponseModel();
            var userInDB = userDBProvider.GetUserByUsername(user.Username);
            if (userInDB == null)
            {
                response.StatusCode = 401;
                response.Errors.Add("Incorrect login or password");
                return response;
            }

            if (userInDB.IsActivated == false)
            {
                response.StatusCode = 401;
                response.Errors.Add("Your account is deactivated!");
                return response;
            }

            if (!hashHelper.VerifyHash(user.Password, userInDB.PasswordHash, userInDB.PasswordSalt))
            {
                response.StatusCode = 401;
                response.Errors.Add("Incorrect login or password");
                return response;
            }

            response.StatusCode = 200;

            var jwtToken = jwtUtils.GenerateJwtAccessToken(userInDB);
            var refreshToken = jwtUtils.GenerateJWTRefreshToken();
            userInDB.RefreshToken = refreshToken;

            userInDB = userDBProvider.UpdateUser(userInDB);

            response.User = userInDB;
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken;

            return response;
        }
        public AuthenticationResponseModel? LoginByRefreshToken(string token)
        {
            var response = new AuthenticationResponseModel();
            var validatedToken = jwtUtils.ValidateJWTRefreshToken(token);
            if (validatedToken == null)
            {
                response.StatusCode = 401;
                response.Errors.Add("Refresh token is not valid");
                return response;
            }

            var existingUser = userDBProvider.GetUserByRefreshToken(token);
            if (existingUser == null)
            {
                response.StatusCode = 401;
                response.Errors.Add("Refresh token is not valid or user doesn't exist");
                return response;
            }

            if (existingUser.IsActivated == false)
            {
                response.StatusCode = 401;
                response.Errors.Add("Your account is deactivated!");
                return response;
            }

            response.StatusCode = 200;

            var newRefreshToken = jwtUtils.GenerateJWTRefreshToken();
            existingUser.RefreshToken = newRefreshToken;

            userDBProvider.UpdateUser(existingUser);

            var jwtToken = jwtUtils.GenerateJwtAccessToken(existingUser);
            response.User = existingUser;
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken;

            return response;
        }

        public AuthorizationResponse AuthorizeUser(HttpContext context, Func<UserModel, UserModel?> policy)
        {
            var response = new AuthorizationResponse();
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtAccessToken(token);
            if (userId == null)
            {
                response.StatusCode = 401;
                response.Errors.Add("Access token is not valid");
                return response;
            }

            var user = userDBProvider.GetUserById((int)userId);
            if (user == null)
            {
                response.StatusCode = 401;
                response.Errors.Add("User is not found");
                return response;
            }


            if (user.IsActivated == false)
            {
                response.StatusCode = 401;
                response.Errors.Add("Your account is deactivated!");
                return response;
            }

            if (policy(user) == null)
            {
                response.StatusCode = 401;
                response.Errors.Add("Is not alowed for this user");
                return response;
            }

            response.StatusCode = 200;
            response.User = user;

            return response;
        }

        public UserModel? SignOut(UserModel user)
        {
            user.RefreshToken = null;
            return userDBProvider.UpdateUser(user);
        }

        public List<PermissionModel> GetAllPermissions()
        {
            var permissions = new List<PermissionModel>();
            foreach (var permission in UserPolicyPermissions.Permissions)
            {
                permissions.Add(new PermissionModel((int)permission.Key, permission.Value));
            }
            return permissions;
        }
    
    }
}
