using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class OrderGraph
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public bool IsConfirmed { get; set; }

        public List<OrderGraphModel>? OrderWares { get; set; } = new List<OrderGraphModel>();
    }
}
