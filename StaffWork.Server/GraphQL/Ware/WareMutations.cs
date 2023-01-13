using AutoMapper;
using BusinessLogic.Models;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.Ware.Input;
using StaffWork.Server.GraphQL.Ware.Inputs;
using StaffWork.Server.GraphQL.Ware.Output;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.GraphQL.Ware
{
    public class WareMutations : ObjectGraphType
    {
        public WareMutations(IMapper mapper, IWareProvider wareProvider, IBasketProvider basketProvider, IAuthorizationProvider authorizationProvider, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<CRUDWareResponseType>, CRUDWareResponse>()
                .Name("CreateWare")
                .Argument<CreateWareInputType>("ware", "Ware info")
                .Resolve(context =>
                {
                    var response = new CRUDWareResponse();

                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }

                    var wareInput = context.GetArgument<CreateWareInput>("ware");
                    var createUserModel = mapper.Map<NewWareModel>(wareInput);

                    try
                    {
                        response = wareProvider.CreateWare(createUserModel);
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

            Field<NonNullGraphType<CRUDWareResponseType>, CRUDWareResponse>()
              .Name("AddToFavorite")
              .Argument<IntGraphType>("wareId", "Ware id")
              .Resolve(context =>
              {
                  var response = new CRUDWareResponse();

                  var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                  if (authorizationResponse.StatusCode != 200)
                  {
                      response.StatusCode = authorizationResponse.StatusCode;
                      response.Errors = authorizationResponse.Errors;
                      return response;
                  }

                  var id = context.GetArgument<int>("wareId");

                  try
                  {
                      response = wareProvider.AddWareToFavorite(id);
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

            Field<NonNullGraphType<CRUDBasketResponseType>, CRUDBasketResponse>()
              .Name("AddWareToBasket")
              .Argument<CreateBasketWareInputType>("Ware", "Ware")
              .Resolve(context =>
              {
                  var response = new CRUDBasketResponse();

                  var newBasket = context.GetArgument<CreateBasketWareInput>("Ware");

                  response = basketProvider.AddToBasket(new NewBasketWareModel
                  {
                      WareId = newBasket.WareId,
                      Count = newBasket.Count,
                  });
                  return response;
              }
              );
            Field<NonNullGraphType<CRUDBasketResponseType>, CRUDBasketResponse>()
              .Name("ChangeCount")
              .Argument<CreateBasketWareInputType>("Ware", "Ware")
              .Resolve(context =>
              {
                  var response = new CRUDBasketResponse();

                  var newBasket = context.GetArgument<CreateBasketWareInput>("Ware");

                  response = basketProvider.ChangeBasketWareCount(new NewBasketWareModel
                  {
                      WareId = newBasket.WareId,
                      Count = newBasket.Count,
                  });
                  return response;
              }
              );

            Field<NonNullGraphType<GetBasketWaresResponseType>, GetBasketWaresResponse>()
              .Name("ConfirmOrder")
              .Resolve(context =>
              {
                  var response = new GetBasketWaresResponse();

                  response = basketProvider.ConfirmOrder();
                  return response;
              }
              );

            Field<NonNullGraphType<CRUDBasketResponseType>, CRUDBasketResponse>()
              .Name("RemoveWareFromBasket")
              .Argument<IntGraphType>("WareId", "Ware id")
              .Resolve(context =>
              {
                  var response = new CRUDBasketResponse();

                  var wareId = context.GetArgument<int>("WareId");

                  response = basketProvider.RemoveFromBasket(wareId);
                  return response;
              }
              );

            Field<NonNullGraphType<CRUDWareResponseType>, CRUDWareResponse>()
              .Name("RemoveFromFavorite")
              .Argument<IntGraphType>("wareId", "Ware id")
              .Resolve(context =>
              {
                  var response = new CRUDWareResponse();

                  var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                  if (authorizationResponse.StatusCode != 200)
                  {
                      response.StatusCode = authorizationResponse.StatusCode;
                      response.Errors = authorizationResponse.Errors;
                      return response;
                  }

                  var id = context.GetArgument<int>("wareId");

                  try
                  {
                      response = wareProvider.RemoveWareFromFavorite(id);
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
