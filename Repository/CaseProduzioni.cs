using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;


namespace RepositoryBiblioteca.Repository
{
    public class CaseProduzioni : ICasaProduzione
    {
        public int CreateCasaProduzione(CasaProduzione casaProduzione)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[CasaProduzione]
                           ([Nome]
                           ,[Nazionalità])SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdCasaProduzione", casaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@Nome", casaProduzione.Nome);
                command.Parameters.AddWithValue("@Nazionalità", casaProduzione.Nazionalità);

                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "CasaProduzione", "CreateCasaProduzione");
                return -1;
            }
        }

        public CasaProduzione GetCasaProduzione(int IdCasaProduzione)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdCasaProduzione]
                              ,[Nome]
                              ,[Nazionalità]
                          FROM [dbo].[CasaProduzione]";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdCategoria", IdCasaProduzione);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessuna categoria trovata per l'Id richiesto {IdCasaProduzione}");

                datareader.Read();
                {
                    return new CasaProduzione
                    {
                        IdCasaProduzione = IdCasaProduzione,
                        Nome = datareader["Nome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString()
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "CasaProduzione", "GetCasaProduzioni");
                return null;
            }
        }

        public IEnumerable<CasaProduzione> GetCasaProduzione()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdCasaProduzione]
                          ,[Nome]
                          ,[Nazionalità]
                      FROM [dbo].[CasaProduzione]";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new CasaProduzione
                    {
                        IdCasaProduzione = Convert.ToInt32(datareader["IdCasaProduzione"]),
                        Nome = datareader["Nome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString()
                    };
                }
            }
        }

        public IEnumerable<CasaProduzione> GetCasaProduzione(bool deleted)
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdCasaProduzione]
                          ,[Nome]
                          ,[Nazionalità]
                      FROM [dbo].[CasaProduzione]
                        WHERE Cancellato = @Cancellato";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Cancellato", deleted);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new CasaProduzione
                    {
                        IdCasaProduzione = Convert.ToInt32(datareader["IdCasaProduzione"]),
                        Nome = datareader["Nome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString()
                    };
                }
            }
        }

        public bool KillcasaProduzione(int IdCasaProduzione)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCasaProduzione(CasaProduzione casaProduzione)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[CasaProduzione]
                           SET [Nome] =@Nome>
                              ,[Nazionalità] = @Nazionalità,
                         WHERE [IdCasaProduzione] = @IdCasaProduzione"; 

                using var command = new SqlCommand(sql, connection);
                
                command.Parameters.AddWithValue("@Nome", casaProduzione.Nome);
                command.Parameters.AddWithValue("@Nazionalità", casaProduzione.Nazionalità);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "CasaProduzione", "UpdateCasaProduzione");
                return false;
            }
        }
    }
    
}
