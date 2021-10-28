using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public class DVDs : IOggetto<DVD>
    {
        public int Create(DVD ogg)
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
                command.Parameters.AddWithValue("@Cancellato", ogg.Cancellato);
                command.Parameters.AddWithValue("@IdRegista", ogg.Regista.Id);
                command.Parameters.AddWithValue("@IdTipologiaOggetto", 2);


                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "DVD", "CreateDVD");
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
                LogError.Write(Ex.Message, "DVD", "DeleteDVD");
                return false;
            }
        }

        public DVD Get(int id)
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
                                AND [IdTipologiaOggetto] = 2";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdOggetto", id);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun DVD trovat* per l'Id richiesto {id}");
                var categoria = new Categorie();
                var dvd = new DVD();
                var casaProd = new CaseProduzioni();
                var regista = new Registi();
                datareader.Read();
                {
                    dvd.IdOggetto = Convert.ToInt32(datareader["IdOggetto"]);
                    dvd.Titolo = datareader["Titolo"].ToString();
                    dvd.Categoria = categoria.GetCategoria(Convert.ToInt32(datareader["IdCategoria"]));
                    dvd.CasaProduzione = casaProd.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"]));
                    dvd.Regista = regista.GetRegista(Convert.ToInt32(datareader["IdRegista"]));
                    dvd.Cancellato = false;
                    dvd.DataUscita = Convert.ToDateTime(datareader["DataUscita"]);

                    return dvd;
                }
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "DVD", "GetDVD");
                return null;
            }
        }

        public IEnumerable<DVD> Get()
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
                                WHERE [IdTipologiaOggetto] = 2";
            using var command = new SqlCommand(sql, connection);

            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    var categoria = new Categorie();
                    var casaProd = new CaseProduzioni();
                    var regista = new Registi();
                    yield return new DVD
                    {


                        IdOggetto = Convert.ToInt32(datareader["IdOggetto"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Categoria = categoria.GetCategoria(Convert.ToInt32(datareader["IdCategoria"])),
                        CasaProduzione = casaProd.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"])),
                        Regista = regista.GetRegista(Convert.ToInt32(datareader["IdRegista"])),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"])



                    };
                }
            }
        }

        public bool Update(DVD ogg)
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
                              ,[IdRegista] = @IdRegista
                              
                         WHERE [IdOggetto] = @IdOggetto";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Titolo", ogg.Titolo);
                command.Parameters.AddWithValue("@IdCategoria", ogg.Categoria.IdCategoria);
                command.Parameters.AddWithValue("@IdCasaProduzione", ogg.CasaProduzione.IdCasaProduzione);
                command.Parameters.AddWithValue("@DataUscita", ogg.DataUscita);
                command.Parameters.AddWithValue("@IdRegista", ogg.Regista.Id);
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "DVD", "UpdateDVD");
                return false;
            }
        }
    }
}
