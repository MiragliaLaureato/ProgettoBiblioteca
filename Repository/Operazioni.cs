using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Repository
{
    public class Operazioni<T> : IPrestito<T> where T : Oggetto
    {
        public int InsertPrestito(Prestito<T> prestito)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"INSERT INTO [dbo].[Prestito]
                                    ([IdPrestito]
                                    ,[DataInizio]
                                    ,[DataFine],[Utente],[ImpPrestito],[ImpRestituzione],[Oggetto])
                            VALUES (@IdPrestito),
                                    (@DataInizio),
                                    (@DataFine),(@Utente),(@ImpPrestito), (@ImpRestituzione),
                                     (@Oggetto);  SELECT SCOPE_IDENTITY();";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdPrestito", prestito.IdPrestito);
                command.Parameters.AddWithValue("@DataInizio", prestito.DataInizio);
                command.Parameters.AddWithValue("@DataFine", prestito.DataFine);
                command.Parameters.AddWithValue("@Utente", prestito.Utente);
                command.Parameters.AddWithValue("@ImpPrestito", prestito.ImpPrestito);
                command.Parameters.AddWithValue("@ImpRestituzione", prestito.ImpRestituzione);
                command.Parameters.AddWithValue("@Oggetto", prestito.Oggetto);
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Prestito", "CreatePrestito");
                return -1;
            }
        }

        public bool DeletePrestito(int id)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"DELETE FROM [dbo].[Operazioni]
                            WHERE [IdPrestito] =@IdPrestito";
                using var command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();

                return true;

            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Prestito", "DeletePrestito");
                return false;
            }
        }

        public  Prestito<T> GetPrestito(int idPrestito)
        {
            throw new NotImplementedException();
        }

        public  IEnumerable<T> GetPrestito()
        {
            throw new NotImplementedException();
        }

        public bool UpdatePrestito(Prestito<T> prestito)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();
                var sql = @"UPDATE [dbo].[Operazioni]
                           SETSET [DataInizio] = @DataInizio
                                  ,[DataFine] = @DataFine
                                  ,[IdUtente] = @IdUtente
                                  ,[IdImpiegatoPrestito] = @IdImpiegatoPrestito
                                  ,[IdImpiegatoRestituzione] = @IdImpiegatoRestituzione
                            where [IdPrestito] = @IdPrestito";
                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@DataInizio", prestito.DataInizio);
                command.Parameters.AddWithValue("@DataFine", prestito.DataFine);
                command.Parameters.AddWithValue("@IdUtente", prestito.Utente.Id);
                command.Parameters.AddWithValue("@IdImpiegatoPrestito", prestito.ImpPrestito.Id);
                command.Parameters.AddWithValue("@IdImpiegatoRestituzione", prestito.ImpRestituzione.Id);
                command.Parameters.AddWithValue("@Oggetto", prestito.Oggetto);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                LogError.Write(Ex.Message, "Prestito", "UpdatePrestito");
                return false;
            }
        }

        IEnumerable<Prestito<T>> IPrestito<T>.GetPrestito()
        {
            throw new NotImplementedException();
        }
    }
}
