using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class QuerySettings
    {
        public SortParamsModel? SortParam { get; set; }
        public int? CategoryId { get; set; }
        public int? CountOfRecords { get; set; } = 10;
        public FilterEnum? Filter { get; set; }
    }
}
