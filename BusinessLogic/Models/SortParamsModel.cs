using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class SortParamsModel
    {
        public string Value { get; set; } = "Id";
        public bool IsReverse { get; set; } = false;
    }
}
