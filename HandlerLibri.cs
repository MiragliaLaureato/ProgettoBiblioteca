using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;

namespace Logica
{
    public class HandlerLibri
    {

        public readonly Libri Libri;

        public int GetByTitoloLibro(string titoloLibro)
        {
            var libri = Libri.Get();
            return libri.Where(x => x.Titolo == titoloLibro).Select(y=>y.IdOggetto).FirstOrDefault();
        }

    }
}
