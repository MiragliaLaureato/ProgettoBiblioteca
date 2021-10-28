using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Repository
{
    public class PrestitoRivista : Operazioni<Rivista>
    {
        public new Prestito<Rivista> GetPrestito(int idPrestito)
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
                var riv = new Riviste();

                datareader.Read();
                {
                    return new Prestito<Rivista>
                    {
                        IdPrestito = Convert.ToInt32(datareader["IdPrestito"]),
                        DataInizio = Convert.ToDateTime(datareader["DataInizio"]),
                        DataFine = Convert.ToDateTime(datareader["DataFine"]),
                        Utente = utente.GetUtente(Convert.ToInt32(datareader["IdUtente"])),
                        ImpPrestito = impiegati.GetImpiegato((Convert.ToInt32(datareader["ImpPrestito"]))),
                        ImpRestituzione = impiegati.GetImpiegato((Convert.ToInt32(datareader["ImpRestituzione"]))),
                        Oggetto = riv.Get(Convert.ToInt32(datareader["IdOggetto"]))

                    };
                }
            }
            catch (Exception error)
            {
                LogError.Write(error.Message, "Prestito", "GetPrestito");
                return null;
            }
        }

        public new IEnumerable<Prestito<Rivista>> GetPrestito()
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
                    var rv = new Riviste();


                    datareader.Read();
                    {
                        yield return new Prestito<Rivista>
                        {
                            IdPrestito = Convert.ToInt32(datareader["IdPrestito"]),
                            DataInizio = Convert.ToDateTime(datareader["DataInizio"]),
                            DataFine = Convert.ToDateTime(datareader["DataFine"]),
                            Utente = utente.GetUtente(Convert.ToInt32(datareader["IdUtente"])),
                            ImpPrestito = imp1.GetImpiegato(Convert.ToInt32(datareader["IdImpPrestito"])),
                            ImpRestituzione = imp2.GetImpiegato(Convert.ToInt32(datareader["IdImpRestituzione"])),
                            Oggetto = rv.Get(Convert.ToInt32(datareader["IdOggetto"]))

                        };
                    }
                }

            }
        }
    }
}
