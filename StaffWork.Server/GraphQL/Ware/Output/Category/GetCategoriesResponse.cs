using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Category
{
    public class GetCategoriesResponse
    {
        public List<CategoryModel> Categories { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public GetCategoriesResponse()
        {
            Errors = new List<string>();
        }
    }
}
