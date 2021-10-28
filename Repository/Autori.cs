using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public class Autori : IAutore
    {
        public int CreateAutore(Autore autore)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Autore]
                                    ([Nome]
                                    ,[Cognome]
                                    ,[Nazionalità],[Cancellato])
                            VALUES (@Nome),
                                    (@Cognome),
                                    (@Nazionalità),(@Cancellato);  SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Nome", autore.Nome);
                command.Parameters.AddWithValue("@Cognome", autore.Cognome);
                command.Parameters.AddWithValue("@Nazionalità", autore.Nazionalità);
                command.Parameters.AddWithValue("@Cancellato", autore.Cancellato);

                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Autore", "CreateAutore");
                return -1;
            }
        }

        public bool DeleteAutore(int id)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE FROM [dbo].[Autore]
                                SET [Cancellato] = true
                            WHERE [IdAutore] = @id";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdAutore", id);
                command.ExecuteNonQuery();

                return true;

            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Autore", "DeleteAutore");
                return false;
            }
        }
        
        public Autore GetAutore(int idAutore)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdAutore]
                                      ,[Nome]
                                      ,[Cognome]
                                      ,[Nazionalità]
                                      ,[Cancellato]
                                  FROM [dbo].[Autore]
                   WHERE [IdCategoria] = @IdCategoria";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdAutore", idAutore);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessuna categoria trovata per l'Id richiesto {idAutore}");

                datareader.Read();
                {
                    return new Autore
                    {
                        Id = Convert.ToInt32(datareader["IdAutore"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                        Cancellato =  Convert.ToBoolean(datareader["Cancellato"]),
                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Autore", "GetAutore");
                return null;
            }
        }

        public IEnumerable<Autore> GetAutori()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [Id]
                                ,[Nome]
                                ,[Cognome]
                                ,[Nazionalità]
                                ,[Cancellato]
                            FROM [dbo].[Autori]";
            using var command = new SqlCommand(sql, connection);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new Autore
                    {
                        Id = Convert.ToInt32(datareader["Id"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"])

                    };
                }
            }
        }

        public IEnumerable<Autore> GetAutoriByState(bool deleted)
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [Id]
                                ,[Nome]
                                ,[Cognome]
                                ,[Nazionalità]
                                ,[Cancellato]
                            FROM [dbo].[Autori]WHERE Cancellato = @Cancellato";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Cancellato", deleted);
            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    yield return new Autore
                    {
                        Id = Convert.ToInt32(datareader["Id"]),
                        Nome = datareader["Nome"].ToString(),
                        Cognome = datareader["Cognome"].ToString(),
                        Nazionalità = datareader["Nazionalità"].ToString(),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"])

                    };
                }
            }
        }

        public bool UpdateAutore(Autore autore)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Autore]
                            SET [Nome] = @Nome
                            ,[Cognome] = @Cognome
                            ,[Nazionalità] = @Nazionalità
                            ,[Cancellato] = @Cancellato 
                            WHERE [IdAutore] = @IdAutore";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Cognome", autore.Cognome);
                command.Parameters.AddWithValue("@Nome", autore.Nome);
                command.Parameters.AddWithValue("@IdAutore", autore.Id);
                command.Parameters.AddWithValue("@Nazionalità", autore.Nazionalità);
                command.Parameters.AddWithValue("@Cancellato", autore.Cancellato);
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Autore", "UpdateAutore");
                return false;
            }
        }
    }
}
