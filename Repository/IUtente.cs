using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface IUtente
    {
        Utente GetUtente(int IdUtente);

        IEnumerable<Utente> GetUtente();

        int CreateUtente(Utente utente);

        bool KillUtente(int IdUtente);

        bool UpdateUtente(Utente utente);
    }
}
