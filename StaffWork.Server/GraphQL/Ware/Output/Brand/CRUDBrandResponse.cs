using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Brand
{
    public class CRUDBrandResponse
    {
        public BrandModel? Brand { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public CRUDBrandResponse()
        {
            Errors = new List<string>();
        }
    }
}
