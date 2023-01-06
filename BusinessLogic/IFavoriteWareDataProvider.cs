using BusinessLogic.Models;

namespace BusinessLogic
{
    public interface IFavoriteDataProvider
    {
        public FavoriteWareModel AddToFavorite(NewFavoriteModel model);
        public FavoriteWareModel RemoveToFavorite(int userId, int wareId);
        public List<FavoriteWareModel> GetUserFavorite(int userId);
        public FavoriteWareModel GetFavoriteById(int id);
    }
}
