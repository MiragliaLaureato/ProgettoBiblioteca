using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public class Utenti : IUtente
    {
        public int CreateUtente(Utente utente)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Utenti]
                               ([Nome]
                               ,[Cognome]
                               ,[NumTelefono]
                               ,[Cancellato])
                         VALUES
                               (@Nome),
                                (@Cognome),
                                (@NumTelefono),
                                (@Cancellato); SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", utente.Nome);
                command.Parameters.AddWithValue("@Cognome", utente.Cognome);
                command.Parameters.AddWithValue("@NumTelefono", utente.NumTelefono);
                command.Parameters.AddWithValue("@Cancellato", utente.Cancellato);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Utente", "CreateUtente");
                return -1;
            }
        }

        public Utente GetUtente(int IdUtente)
        {
            try
            {

                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdUtente]
                                      ,[Nome]
                                      ,[Cognome]
                                      ,[NumTelefono]
                                      ,[Cancellato]
                                  FROM [dbo].[Utenti]
                                WHERE [IdUtente] = @IdUtente";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdCategoria", IdUtente);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun utente trovato per l'Id richiesto {IdUtente}");
                datareader.Read();
                {
                    return new Utente
                    {
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        Id = Convert.ToInt32(datareader["IdCategoria"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        NumTelefono = datareader["NumTelefono"].ToString()
                    };
                }
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Utente", "DeleteUtente");
                return null;
            }
        }

        public IEnumerable<Utente> GetUtente()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdUtente]
                              ,[Nome]
                              ,[Cognome]
                              ,[NumTelefono]
                              ,[Cancellato]
                          FROM [dbo].[Utenti]";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new Utente
                    {
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        Id = Convert.ToInt32(datareader["IdCategoria"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        NumTelefono = datareader["NumTelefono"].ToString()
                    };
                }
            }
        }

        public bool KillUtente(int IdUtente)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Utente]
                    SET Cancellato = 1";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();

                return true;

            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Utente", "DeleteUtente");
                return false;
            }
        }

        public bool UpdateUtente(Utente utente)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Utenti]
                           SET [Nome] = @Nome
                              ,[Cognome] = @Cognome
                              ,[NumTelefono] =@NumTelefono
                              ,[Cancellato] = @Cancellato
                         WHERE [Id] = IdUtente";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cancellato", utente.Cancellato);
                command.Parameters.AddWithValue("@Nome", utente.Nome);
                command.Parameters.AddWithValue("@Cognome", utente.Cognome);
                command.Parameters.AddWithValue("@NumTelefono", utente.NumTelefono);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Utente", "DeleteUtente");
                return false;
            }
        }    
    }
}
