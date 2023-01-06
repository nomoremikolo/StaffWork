using AutoMapper;
using BusinessLogic;
using BusinessLogic.Models;
using StaffWork.Server.GraphQL.User.Types;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils.Intarfaces;
using System.ComponentModel.DataAnnotations;

namespace StaffWork.Server.Providers
{
    public class UserProvider : IUserProvider
    {
        private IUserDataProvider userDBProvider;
        private IJwtUtils jwtUtils;
        private IMapper mapper;
        private IHashHelper hashHelper;
        public UserProvider(IUserDataProvider userDBProvider, IJwtUtils jwtUtils, IMapper mapper, IHashHelper hashHelper)
        {
            this.userDBProvider = userDBProvider;
            this.jwtUtils = jwtUtils;
            this.mapper = mapper;
            this.hashHelper = hashHelper;
        }
        public List<UserModel> GetUsers()
        {
            return userDBProvider.GetUsers();
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
    }
}
