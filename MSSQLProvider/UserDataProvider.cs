using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLProvider
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly string connectionString;
        public UserDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
