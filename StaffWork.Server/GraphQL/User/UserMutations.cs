﻿using AutoMapper;
using BusinessLogic.Models;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.User.Input;
using StaffWork.Server.GraphQL.User.Inputs;
using StaffWork.Server.GraphQL.User.Types;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils;

namespace StaffWork.Server.GraphQL.User
{
    public class UserMutations : ObjectGraphType
    {
        public UserMutations(IMapper mapper, ICookiesHelper cookiesHelper, IUserProvider userProvider, IAuthorizationProvider authorizationProvider, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<CRUDUserResponseType>, CRUDUserResponse>()
                .Name("SignIn")
                .Argument<CreateUserInputType>("user", "User info")
                .Resolve(context =>
                {
                    var response = new CRUDUserResponse();

                    var userInput = context.GetArgument<CreateUserInput>("user");
                    var createUserModel = mapper.Map<NewUserModel>(userInput);

                    try
                    {
                        response = userProvider.CreateUser(createUserModel);
                        if(response.StatusCode == 409)
                        {
                            return response;
                        }
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Database error");
                        return response;
                    }

                    try
                    {
                        UserLoginModel userLoginModel = new UserLoginModel();
                        userLoginModel.Username = userInput.Username;
                        userLoginModel.Password = userInput.Password;
                        AuthenticationResponseModel authRes = authorizationProvider.Login(userLoginModel);
                        if (authRes.StatusCode == 200)
                        {
                            response.RefreshToken = authRes.RefreshToken;
                            response.JwtToken = authRes.RefreshToken;
                            cookiesHelper.SetTokenCookie(response.RefreshToken, httpContextAccessor.HttpContext);
                        }
                        else
                        {
                            response.Errors.AddRange(authRes.Errors);
                            throw new Exception("Somethings wrong..");
                        }
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Account was successfully created! But we can't auth you right now.. Try to login!");
                        return response;
                    }
                    return response;
                }
                );
            Field<NonNullGraphType<CRUDUserResponseType>, CRUDUserResponse>()
                .Name("UpdateUser")
                .Argument<UpdateUserByAdminInputType>("user", "User")
                .Resolve(context =>
                {
                    var response = new CRUDUserResponse();

                    var userInput = context.GetArgument<UpdateUserByAdminInput>("user");

                    try
                    {
                        response = userProvider.UpdateUser(new UserModel
                        {
                            Id = userInput.Id,
                            Adress = userInput.Adress,
                            Age = userInput.Age,
                            Email = userInput.Email,
                            IsActivated = userInput.IsActivated,
                            Name = userInput.Name,
                            Permissions = userInput.Permissions,
                            Role = userInput.Role,
                            Surname = userInput.Surname,
                            Username = userInput.Username,
                        });
                        if (response.StatusCode == 409)
                        {
                            return response;
                        }
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

            Field<NonNullGraphType<CRUDUserResponseType>, CRUDUserResponse>()
                .Name("UpdateSelfInfo")
                .Argument<UpdateBySelfInputType>("user", "User")
                .Resolve(context =>
                {
                    var response = new CRUDUserResponse();

                    var userInput = context.GetArgument<UpdateBySelfInput>("user");
                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.Authorized);
                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }
                    try
                    {
                        response = userProvider.UpdateUser(new UserModel
                        {
                            Id = authorizationResponse.User.Id,
                            Adress = userInput.Adress,
                            Age = userInput.Age,
                            Email = userInput.Email,
                            Name = userInput.Name,
                            Surname = userInput.Surname,
                            Username = userInput.Username,
                        });
                        if (response.StatusCode == 409)
                        {
                            return response;
                        }
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
