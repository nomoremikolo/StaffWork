using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class WareModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Sizes { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public bool IsDiscount { get; set; } = false;
        public int CountInStorage { get; set; }
        public string? Thumbnail { get; set; }
        public string? Images { get; set; }

    }
}
