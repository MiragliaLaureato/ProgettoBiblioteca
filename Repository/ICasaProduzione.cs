using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface ICasaProduzione
    {
        CasaProduzione GetCasaProduzione(int IdCasaProduzione);

        IEnumerable<CasaProduzione> GetCasaProduzione();

        int CreateCasaProduzione(CasaProduzione casaProduzione);

        bool KillcasaProduzione(int IdCasaProduzione);

        bool UpdateCasaProduzione(CasaProduzione casaProduzione);
        IEnumerable<CasaProduzione> GetCasaProduzione(bool deleted);
    }
}
