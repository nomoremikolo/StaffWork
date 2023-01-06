using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public interface FavoriteModel
    {
        public int Id { get; set; }
        public int WareId { get; set; }
        public int UserId { get; set; }
    }
}
