﻿using BusinessLogic.Models;
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
        public WareModelWithBrandAndCategory GetWareById(int wareId);
        public List<WareModel> GetAllWares(QuerySettings settings);
        public List<AuthorizedUserWareModel> GetAllWaresWithFavorite(QuerySettings settings, int userId);
    }
}
