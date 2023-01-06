using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Cart
{
    public class CartMutations : ObjectGraphType
    {
        public CartMutations()
        {
            Field<NonNullGraphType<CRUDWareResponseType>, CRUDWareResponse>()
                .Name("AddToCart")
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
        }
    }
}
