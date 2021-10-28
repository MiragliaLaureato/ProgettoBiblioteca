using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RepositoryBiblioteca
{
    public static class Connection
    {
        private static string ConnectionString = "Server=.; Database=Biblioteca; Trusted_Connection=True;";

        public static string GetConnectionString() => ConnectionString;
    }
}
