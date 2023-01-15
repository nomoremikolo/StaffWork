using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Authorization.Types;
using StaffWork.Server.GraphQL.Ware.Output.Favorite;
using StaffWork.Server.GraphQL.Ware.Output.Ware;

namespace StaffWork.Server.Providers.Interfaces
{
    public interface IWareProvider
    {
        CRUDWareResponse CreateWare(NewWareModel ware);
        List<WareModel> GetAllWares(QuerySettings settings);
        WareModel GetWareById(int id);
        GetAuthorizedUserWaresResponse GetAllWaresWithFavorite(QuerySettings settings);
        CRUDWareResponse DeleteWare(int id);

        CRUDWareResponse AddWareToFavorite(int wareId);
        GetFavoriteWaresResponse GetUserFavoriteWares();
        CRUDWareResponse RemoveWareFromFavorite(int wareId);
        


        
    }
}
