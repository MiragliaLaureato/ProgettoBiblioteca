using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface IRegista
    {
        Regista GetRegista(int Id);

        IEnumerable<Regista> GetRegista();

        int CreateRegista(Regista regista);

        bool KillRegista(int Id);

        bool UpdateRegista(Regista regista);
    }
}
