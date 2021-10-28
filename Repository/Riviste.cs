using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Repository
{
    public class Riviste : IOggetto<Rivista>
    {
        public int Create(Rivista ogg)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Oggetti]
                                   ([Titolo]
                                   ,[IdCategoria]
                                   ,[IdCasaProduzione]
                                   ,[DataUscita]
                                   ,[IdAutore]
                                   ,[IdRegista]
                                   ,[Cancellato]
                                   ,[IdTipologiaOggetto])
                            VALUES
                                    ([@Titolo],[IdCategoria],[IdCasaProduzione],[DataUscita],[IdAutore],[IdRegista],[IdTipologiaOggetto]),

                                     SELECT SCOPE_IDENTITY();";

                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Titolo", ogg.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", ogg.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", ogg.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", ogg.DataUscita);
                command.Parameters.AddWithValue("@IdAutore", null);
                command.Parameters.AddWithValue("@Cancellato", false);
                command.Parameters.AddWithValue("@IdRegista", null);
                command.Parameters.AddWithValue("@IdTipologiaOggetto", 3);


                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Rivista", "CreateRivista");
                return -1;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Oggetti]
                    SET Cancellato = 1
                    WHERE [IdOggetto] = @id";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();

                return true;

            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Rivista", "DeleteRivista");
                return false;
            }
        }

        public Rivista Get(int id)
        {
            try
            {

                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"SELECT [IdOggetto]
                                  ,[Titolo]
                                  ,[IdCategoria]
                                  ,[IdCasaProduzione]
                                  ,[DataUscita]
                                  ,[IdAutore]
                                  ,[IdRegista]
                                  ,[IdTipologiaOggetto]
                              FROM [dbo].[Oggetti]
                                WHERE [IdOggetto] = @id
                                AND [IdTipologiaOggetto] = 3";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdOggetto", id);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessuna rivist* trovat* per l'Id richiesto {id}");
                var categoria = new Categorie();
                var rivista = new Rivista();
                var casaProd = new CaseProduzioni();
                
                datareader.Read();
                {
                    rivista.IdOggetto = Convert.ToInt32(datareader["IdOggetto"]);
                    rivista.Titolo = datareader["Titolo"].ToString();
                    rivista.Categoria = categoria.GetCategoria(Convert.ToInt32(datareader["IdCategoria"]));
                    rivista.CasaProduzione = casaProd.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"]));
                    rivista.Cancellato = Convert.ToBoolean(datareader["Cancellato"]);
                    rivista.DataUscita = Convert.ToDateTime(datareader["DataUscita"]);

                    return rivista ;
                }
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Rivista", "GetRivista");
                return null;
            }
        }

        public IEnumerable<Rivista> Get()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdOggetto]
                                  ,[Titolo]
                                  ,[IdCategoria]
                                  ,[IdCasaProduzione]
                                  ,[DataUscita]
                                  ,[IdAutore]
                                  ,[IdRegista]
                                  ,[IdTipologiaOggetto]
                              FROM [dbo].[Oggetti]
                                WHERE [IdTipologiaOggetto] = 3";
            using var command = new SqlCommand(sql, connection);

            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    var categoria = new Categorie();
                    var casaProd = new CaseProduzioni();
                    
                    yield return new Rivista
                    {

                        IdOggetto = Convert.ToInt32(datareader["IdOggetto"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Categoria = categoria.GetCategoria(Convert.ToInt32(datareader["IdCategoria"])),
                        CasaProduzione = casaProd.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"])),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"])

                    };
                }
            }
        }

        public bool Update(Rivista ogg)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Oggetti]
                           SET [Titolo] = @Titolo
                              ,[IdCategoria] =@IdCategoria
                              ,[IdCasaProduzione] = @IdCasaProduzione
                              ,[DataUscita] =  @DataUscita
                              ,[IdAutore] = @IdAutore
                              
                         WHERE [IdOggetto] = @IdOggetto";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Titolo", ogg.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", ogg.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", ogg.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", ogg.DataUscita);
               
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Rivista", "Up dateRivista");
                return false;
            }
        }
    }
}
