using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class OrderGraphModel
    {
        public int WareId { get; set; }
        public string WareName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Sizes { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public bool IsDiscount { get; set; }
        public int CountInStorage { get; set; }
        public int Count { get; set; }
        public string Size { get; set; } 
    }
}
