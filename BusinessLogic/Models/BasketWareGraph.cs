using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class BasketWareGraph : WareModel
    {
        public int BasketId { get; set; }
        public int Count { get; set; }
    }
}
