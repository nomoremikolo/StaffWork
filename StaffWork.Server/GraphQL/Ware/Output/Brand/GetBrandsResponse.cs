using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Brands
{
    public class GetBrandsResponse
    {
        public List<BrandModel> Brands { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public GetBrandsResponse()
        {
            Errors = new List<string>();
        }
    }
}
