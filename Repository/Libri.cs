using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Repository
{
    public class Libri : IOggetto<Libro>
    {
        public int Create(Libro ogg)
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
                command.Parameters.AddWithValue("@IdAutore", ogg.Autore.Id);
                command.Parameters.AddWithValue("@Cancellato", ogg.Cancellato);
                command.Parameters.AddWithValue("@IdRegista", null);
                command.Parameters.AddWithValue("@IdTipologiaOggetto", 1);


                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Libro", "CreateLibro");
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
                LogError.Write(Ex.Message, "Libro", "DeleteLibro");
                return false;
            }
        }

        public Libro Get(int id)
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
                                AND [IdTipologiaOggetto] = 1";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdOggetto", id);
                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessun libro trovat* per l'Id richiesto {id}");
                var categoria = new Categorie();
                var libro = new Libro();
                var casaProd = new CaseProduzioni();
                var autore = new Autori();
                datareader.Read();
                {
                    libro.IdOggetto = Convert.ToInt32(datareader["IdOggetto"]);
                    libro.Titolo = datareader["Titolo"].ToString();
                    libro.Categoria = categoria.GetCategoria(Convert.ToInt32(datareader["IdCategoria"]));
                    libro.CasaProduzione = casaProd.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"]));
                    libro.Autore = autore.GetAutore(Convert.ToInt32(datareader["IdAutore"]));
                    libro.Cancellato = false;
                    libro.DataUscita = Convert.ToDateTime(datareader["DataUscita"]);

                    return libro;
                }
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Libro", "GetLibro");
                return null;
            }
        }

        public IEnumerable<Libro> Get()
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
                                WHERE [IdTipologiaOggetto] = 1";
                using var command = new SqlCommand(sql, connection);
                
                using var datareader = command.ExecuteReader();
                if (datareader.HasRows)
                {
                    while(datareader.Read())
                    {
                        var categoria = new Categorie();                   
                        var casaProd = new CaseProduzioni();
                        var autore = new Autori();
                    yield return new Libro
                    {


                        IdOggetto = Convert.ToInt32(datareader["IdOggetto"]),
                        Titolo = datareader["Titolo"].ToString(),
                        Categoria = categoria.GetCategoria(Convert.ToInt32(datareader["IdCategoria"])),
                        CasaProduzione = casaProd.GetCasaProduzione(Convert.ToInt32(datareader["IdCasaProduzione"])),
                        Autore = autore.GetAutore(Convert.ToInt32(datareader["IdAutore"])),
                        Cancellato = Convert.ToBoolean(datareader["Cancellato"]),
                        DataUscita = Convert.ToDateTime(datareader["DataUscita"])



                        };
                    }
                }
                    
                
            
        }

        public bool Update(Libro ogg)
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
                command.Parameters.AddWithValue("@IdAutore", ogg.Autore.Id);
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Libro", "Updatelibro");
                return false;
            }
        }
    }
}
