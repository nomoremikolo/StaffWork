using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class WareModelWithBrandAndCategory : WareModel
    {
        public int BrandId { set; get; }
        public string BrandName { set; get; }
        public string Phone { set; get; }
        public string CountryManufactured { set; get; }
        public int CategoryId { set; get; }
        public string CategoryName { set; get; }
    }
}
