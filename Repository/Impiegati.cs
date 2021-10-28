using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public class Impiegati : IImpiegato
    {
        public int CreateImpiegato(Impiegato impiegato)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Impiegati] ([Nome],[Cognome],[Cancellato])
                            VALUES (@Nome),(@Cognome,0); SELECT SCOPE_IDENTITY()";
;
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", impiegato.Nome);
                command.Parameters.AddWithValue("@Cognome", impiegato.Cognome);


                
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Prestito", "CreatePrestito");
                return -1;
            }
        }

            public Impiegato GetImpiegato(int idImpiegato)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Impiegato> GetImpiegato()
        {
            throw new NotImplementedException();
        }

        public bool KillImpiegato(int idImp)
        {
            throw new NotImplementedException();
        }

        public bool UpdateImpiegato(Impiegato impiegato)
        {
            throw new NotImplementedException();
        }
    }
}
