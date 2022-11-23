using AutoMapper;
using BusinessLogic.Models;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.User.Inputs;
using StaffWork.Server.GraphQL.User.Types;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils;

namespace StaffWork.Server.GraphQL.User
{
    public class UserMutations : ObjectGraphType
    {
        public UserMutations(IMapper mapper, IUserProvider userProvider, IAuthorizationProvider authorizationProvider, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<CRUDUserResponseType>, CRUDUserResponse>()
                .Name("ToRegister")
                .Argument<CreateUserInputType>("userData", "data for creating user")
                .Resolve(context =>
                {
                    var response = new CRUDUserResponse();
                    //var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.UsersCRUD);
                    //if (authorizationResponse.StatusCode != 200)
                    //{
                    //    response.StatusCode = authorizationResponse.StatusCode;
                    //    response.Errors = authorizationResponse.Errors;
                    //    return response;
                    //}

                    var userInput = context.GetArgument<CreateUserInput>("userData");
                    var createUserModel = mapper.Map<NewUserModel>(userInput);
                    createUserModel.Permissions = StringToArrayConverter.ArrayOfNumbersToString(userInput.Permissions);

                    try
                    {
                        response = userProvider.ToRegister(createUserModel);
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Database error");
                        return response;
                    }
                    return response;
                }
                );

        }
    }
}
