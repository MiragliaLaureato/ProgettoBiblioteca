using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public class PrestitoLibro : Operazioni<Libro>
    {
        public new Prestito<Libro> GetPrestito(int idPrestito)
        {
            try
            {
                using var connection = new SqlConnection(Connection.GetConnectionString());
                connection.Open();

                var sql = @"SELECT [IdPrestito]
                            ,[DataInizio]
                            ,[DataFine]
                            ,[IdUtente]
                            ,[IdImpiegatoPrestito]
                            ,[IdImpiegatoRestituzione]
                            ,[IdOggetto]
                             FROM [dbo].[Operazioni]
                   WHERE [IdPrestito] = @IdPrestito";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdPrestito", idPrestito);

                using var datareader = command.ExecuteReader();
                if (!datareader.HasRows)
                    throw new Exception($"Nessuna categoria trovata per l'Id richiesto {idPrestito}");
                var impiegati = new Impiegati();
                var utente = new Utenti();
                var lib = new Libri();

                datareader.Read();
                {
                    return new Prestito<Libro>
                    {
                        IdPrestito = Convert.ToInt32(datareader["IdPrestito"]),
                        DataInizio = Convert.ToDateTime(datareader["DataInizio"]),
                        DataFine = Convert.ToDateTime(datareader["DataFine"]),
                        Utente = utente.GetUtente(Convert.ToInt32(datareader["IdUtente"])),
                        ImpPrestito = impiegati.GetImpiegato((Convert.ToInt32(datareader["ImpPrestito"]))),
                        ImpRestituzione = impiegati.GetImpiegato((Convert.ToInt32(datareader["ImpRestituzione"]))),
                        Oggetto = lib.Get(Convert.ToInt32(datareader["IdOggetto"]))

                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Prestito", "GetPrestito");
                return null;
            }
        }

        public new IEnumerable<Prestito<Libro>> GetPrestito()
        {
            using var connection = new SqlConnection(Connection.GetConnectionString());
            connection.Open();
            var sql = @"SELECT [IdPrestito]
                                  ,[DataInizio]
                                  ,[DataFine]
                                  ,[IdUtente]
                                  ,[IdImpiegatoPrestito]
                                  ,[IdImpiegatoRestituzione]
                                  ,[IdOggetto]
                              FROM [dbo].[Operazioni]";
            using var command = new SqlCommand(sql, connection);

            using var datareader = command.ExecuteReader();
            if (datareader.HasRows)
            {
                while (datareader.Read())
                {
                    
                    var categoria = new Categorie();
                    var casaProd = new CaseProduzioni();
                    var utente = new Utenti();
                    var imp1 = new Impiegati();
                    var imp2 = new Impiegati();
                    var lib = new Libri();


                    datareader.Read();
                    {
                        yield return new Prestito<Libro>
                        {
                            IdPrestito = Convert.ToInt32(datareader["IdPrestito"]),
                            DataInizio = Convert.ToDateTime(datareader["DataInizio"]),
                            DataFine = Convert.ToDateTime(datareader["DataFine"]),
                            Utente = utente.GetUtente(Convert.ToInt32(datareader["IdUtente"])),
                            ImpPrestito = imp1.GetImpiegato(Convert.ToInt32(datareader["IdImpPrestito"])),
                            ImpRestituzione = imp2.GetImpiegato(Convert.ToInt32(datareader["IdImpRestituzione"])),
                            Oggetto = lib.Get(Convert.ToInt32(datareader["IdOggetto"]))

                        };
                    }
                }

            }
        }

    }
}
