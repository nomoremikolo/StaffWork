using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class CRUDWareResponse
    {
        public WareModel? Ware { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public CRUDWareResponse()
        {
            Errors = new List<string>();
        }
    }
}
