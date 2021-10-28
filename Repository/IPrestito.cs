using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface IPrestito<T> where T: Oggetto
    {
        Prestito<T> GetPrestito(int idPrestito);

        IEnumerable<Prestito<T>> GetPrestito();

        int InsertPrestito(Prestito<T> p);

        bool DeletePrestito(int id);

        bool UpdatePrestito(Prestito<T> prestito);
    }
}
