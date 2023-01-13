using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class NewBasketWareModel
    {
        public int WareId { get; set; }
        public int UserId { get; set; } = 0;
        public int BasketId { get; set; }
        public int Count { get; set; }
    }
}
