using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output
{
    public class GetFavoriteWaresResponse
    {
        public List<FavoriteWareModel> Wares { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public GetFavoriteWaresResponse()
        {
            Errors = new List<string>();
        }
    }
}
