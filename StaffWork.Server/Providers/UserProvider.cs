using AutoMapper;
using BusinessLogic;
using BusinessLogic.Models;
using StaffWork.Server.GraphQL.User.Types;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils.Intarfaces;
using System.ComponentModel.DataAnnotations;

namespace StaffWork.Server.Providers
{
    public class UserProvider : IUserProvider
    {
        private IUserDataProvider userDBProvider;
        private IAuthorizationProvider authorizationProvider;
        private IHttpContextAccessor httpContextAccessor;
        private IJwtUtils jwtUtils;
        private IMapper mapper;
        private IHashHelper hashHelper;
        public UserProvider(IUserDataProvider userDBProvider, IHttpContextAccessor httpContextAccessor, IAuthorizationProvider authorizationProvider, IJwtUtils jwtUtils, IMapper mapper, IHashHelper hashHelper)
        {
            this.userDBProvider = userDBProvider;
            this.jwtUtils = jwtUtils;
            this.mapper = mapper;
            this.hashHelper = hashHelper;
            this.authorizationProvider = authorizationProvider;
            this.httpContextAccessor = httpContextAccessor;
        }
        public List<UserModel> GetUsers(string? keyWords)
        {
            return userDBProvider.GetUsers(keyWords);
        }

        public CRUDUserResponse CreateUser(NewUserModel user)
        {
            var response = new CRUDUserResponse();

            ICollection<ValidationResult>? validationResults = null;
            if (!Validate(user, out validationResults))
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    response.Errors.Add(validationResult.ToString());
                }
                response.StatusCode = 401;
                return response;
            }


            var UserWithSameUsername = userDBProvider.GetUserByUsername(user.Username);
            if (UserWithSameUsername != null)
            {
                response.Errors.Add("Username : This username has already been used");
                response.StatusCode = 409;
                return response;
            }


            response.StatusCode = 201;
            var userModel = mapper.Map<UserModel>(user);
            var passwordHash = hashHelper.GenerateSaltedHash(user.Password);
            userModel.PasswordHash = passwordHash.Hash;
            userModel.PasswordSalt = passwordHash.Salt;
            userModel.Username = userModel.Username.ToLower();
            response.User = userDBProvider.CreateUser(userModel);
            return response;
        }
        static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }

        public CRUDUserResponse UpdateUser(UserModel user)
        {
            var response = new CRUDUserResponse();

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.Authorized);
            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }
            try
            {
                response.User = userDBProvider.UpdateUser(user);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
                throw;
            }
        }

        public CRUDUserResponse GetUserById(int userId)
        {
            var response = new CRUDUserResponse();

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.Authorized);
            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }
            try
            {
                response.User = userDBProvider.GetUserById(userId);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
                throw;
            }
        }
    }
}
