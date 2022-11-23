using BusinessLogic;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLProvider
{
    public class WareDataProvider : IWareDataProvider
    {
        public WareModel CreateWare(NewWareModel newWare)
        {
            throw new NotImplementedException();
        }

        public WareModel DeleteWare(int wareId)
        {
            throw new NotImplementedException();
        }

        public List<WareModel> GetAllWares()
        {
            throw new NotImplementedException();
        }

        public WareModel GetWareById(int wareId)
        {
            throw new NotImplementedException();
        }

        public WareModel UpdateWare(WareModel updatedWare)
        {
            throw new NotImplementedException();
        }
    }
}
