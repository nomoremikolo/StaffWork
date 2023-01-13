using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Authorization.Types;
using StaffWork.Server.GraphQL.Ware.Output;

namespace StaffWork.Server.Providers.Interfaces
{
    public interface IWareProvider
    {
        List<WareModel> GetAllWares(QuerySettings settings);
        GetAuthorizedUserWaresResponse GetAllWaresWithFavorite(QuerySettings settings);
        WareModel GetWareById(int id);
        CRUDWareResponse CreateWare(NewWareModel ware);
        CRUDWareResponse AddWareToFavorite(int wareId);
        CRUDWareResponse RemoveWareFromFavorite(int wareId);
        GetFavoriteWaresResponse GetUserFavoriteWares();
    }
}
