using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public class Registi : IRegista
    {
        public int CreateRegista(Regista regista)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Regista]
                                   ([Nome]
                                   ,[Cognome]
                                   ,[Nazionalità]
                                   ,[Cancellato])
                             VALUES
                                   (@Nome),
                                    (@Cognome),
                                    (@Nazionalità),
                                    (@Cancellato); SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", regista.Nome);
                command.Parameters.AddWithValue("@Cognome", regista.Cognome);
                command.Parameters.AddWithValue("@Nazionalità", regista.Nazionalità);
                command.Parameters.AddWithValue("@Cancellato", regista.Cancellato);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Regista", "CreateRegista");
                return -1;
            }
        }

        public Regista GetRegista(int Id)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdRegista]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[Nazionalità]
                                  ,[Cancellato]
                              FROM [dbo].[Regista]
                              WHERE [IdRegista] = @Id";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdRegista", Id);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun regista trovato per l'Id richiesto {Id}");

                datareader.Read();
                {
                    return new Regista
                    {
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Regista", "GetRegista");
                return null;
            }
        }

        public IEnumerable<Regista> GetRegista()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdRegista]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[Nazionalità]
                                  ,[Cancellato]
                              FROM [dbo].[Regista]";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new Regista
                    {
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString()
                    };
                }
            }
        }

        public bool KillRegista(int Id)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Regista]
                 SET Cancellato = 1";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Regista", "DeleteRegista");
                return false;
            }
        }

        public bool UpdateRegista(Regista regista)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Regista]
                           SET [Nome] = @Nome
                              ,[Cognome] = @Cognome
                              ,[Nazionalità] = @Nazionalità
                              ,[Cancellato] = @Cancellato
                              WHERE [Id] = @IdRegista";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cancellato", regista.Cancellato);
                command.Parameters.AddWithValue("@Nome", regista.Nome);
                command.Parameters.AddWithValue("@Nazionalità", regista.Nazionalità);
                command.Parameters.AddWithValue("@Cognome", regista.Cognome);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Regista", "DeleteRegista");
                return false;
            }
        }    
    }
}
