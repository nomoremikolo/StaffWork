using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IWareDataProvider
    {
        public WareModel CreateWare(NewWareModel newWare);
        public WareModel UpdateWare(WareModel updatedWare);
        public WareModel DeleteWare(int wareId);
        public WareModel GetWareById(int wareId);
        public List<WareModel> GetAllWares(QuerySettings settings);
    }
}
