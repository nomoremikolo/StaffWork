using AutoMapper;
using BusinessLogic.Models;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.Ware.Input;
using StaffWork.Server.GraphQL.Ware.Inputs;
using StaffWork.Server.GraphQL.Ware.Output.Basket;
using StaffWork.Server.GraphQL.Ware.Output.Brand;
using StaffWork.Server.GraphQL.Ware.Output.Category;
using StaffWork.Server.GraphQL.Ware.Output.Ware;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.GraphQL.Ware
{
    public class WareMutations : ObjectGraphType
    {
        public WareMutations(IMapper mapper,IBrandProvider brandProvider,ICategoryProvider categoryProvider, IWareProvider wareProvider, IBasketProvider basketProvider, IAuthorizationProvider authorizationProvider, IHttpContextAccessor httpContextAccessor)
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

            Field<NonNullGraphType<CRUDCategoryResponseType>, CRUDCategoryResponse>()
                .Name("CreateCategory")
                .Argument<StringGraphType>("CategoryName", "Category name")
                .Resolve(context =>
                {
                    var response = new CRUDCategoryResponse();

                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }

                    var categoryName = context.GetArgument<string>("CategoryName");

                    try
                    {
                        response = categoryProvider.CreateCategory(new NewCategoryModel
                        {
                            Name = categoryName
                        });
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

            Field<NonNullGraphType<CRUDCategoryResponseType>, CRUDCategoryResponse>()
                .Name("DeleteCategory")
                .Argument<IntGraphType>("CategoryId", "Category id")
                .Resolve(context =>
                {
                    var response = new CRUDCategoryResponse();

                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }

                    var CategoryId = context.GetArgument<int>("CategoryId");

                    try
                    {
                        response = categoryProvider.DeleteCategory(CategoryId);
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

            Field<NonNullGraphType<CRUDCategoryResponseType>, CRUDCategoryResponse>()
                .Name("UpdateCategory")
                .Argument<IntGraphType>("CategoryId", "Category id")
                .Argument<StringGraphType>("NewName", "Category name")
                .Resolve(context =>
                {
                    var response = new CRUDCategoryResponse();

                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }

                    var categoryId = context.GetArgument<int>("CategoryId");
                    var categoryName = context.GetArgument<string>("NewName");

                    try
                    {
                        response = categoryProvider.UpdateCategory(new CategoryModel
                        {
                            Name = categoryName,
                            Id = categoryId,
                        });
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


            Field<NonNullGraphType<CRUDBrandResponseType>, CRUDBrandResponse>()
                .Name("CreateBrand")
                .Argument<CreateBrandInputType>("Brand", "Brand")
                .Resolve(context =>
                {
                    var response = new CRUDBrandResponse();

                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }

                    var brandInput = context.GetArgument<CreateBrandInput>("Brand");
                    var brand = mapper.Map<NewBrandModel>(brandInput);
                    try
                    {
                        response = brandProvider.CreateBrand(brand);
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

            Field<NonNullGraphType<CRUDBasketResponseType>, CRUDBasketResponse>()
              .Name("ClearCart")
              .Resolve(context =>
              {
                  var response = new CRUDBasketResponse();

                  response = basketProvider.ClearBasket();
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
            Field<NonNullGraphType<CRUDWareResponseType>, CRUDWareResponse>()
              .Name("DeleteWare")
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
                      response = wareProvider.DeleteWare(id);
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
