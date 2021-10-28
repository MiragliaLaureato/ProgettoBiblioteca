using System;
namespace RepositoryBiblioteca.Model
{
    public class Transazione
    {
        public int IdPrestito { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public int IdUtente { get; set; }
        public int IdImpPrestito { get; set; }
        public int IdImpRestituzione { get; set; }
        public int IdOggetto { get; set; }
        public int IdTipologiaOggetto { get; set; }
    }
}