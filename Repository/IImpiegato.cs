using RepositoryBiblioteca.Model;
using System.Collections.Generic;

namespace RepositoryBiblioteca.Repository
{
    public interface IImpiegato
    {

        Impiegato GetImpiegato(int idImpiegato);

        IEnumerable<Impiegato> GetImpiegato();

        int CreateImpiegato(Impiegato impiegato);

        bool KillImpiegato(int idImp);

        bool UpdateImpiegato(Impiegato impiegato);
    }
}
