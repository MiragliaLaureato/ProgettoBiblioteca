using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBiblioteca.Repository
{
    public interface IOggetto<T> where T: Oggetto
    {

        T Get(int id);

        int Create(T ogg);

        bool Delete(int id);

        IEnumerable<T> Get();

        bool Update(T ogg);

    }
}
