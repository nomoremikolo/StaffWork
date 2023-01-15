using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Category
{
    public class CRUDCategoryResponse
    {
        public CategoryModel? Category { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public CRUDCategoryResponse()
        {
            Errors = new List<string>();
        }
    }
}
