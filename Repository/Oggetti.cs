using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Repository
{
    public class Oggetti
    {
        public int IdOggetto { get; set; }
        public string Titolo { get; set; }
        public DateTime DataUscita { get; set; }
        public int IdCategoria { get; set; }
        public int IdCasaProduzione { get; set; }
        public int IdAutore { get; set; }
        public int IdRegista { get; set; }
        public int IdTipologiaOggetto { get; set; }
        public bool Cancellato { get; set; }
    }
}
