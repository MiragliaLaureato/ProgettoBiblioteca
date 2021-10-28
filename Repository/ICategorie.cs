using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;

namespace RepositoryBiblioteca.Repository
{
    public interface ICategorie
    {
        //insieme di firme di metodi pubblici

        Categoria GetCategoria(int idCategoria);

        IEnumerable<Categoria> GetCategorie();

        int CreateCategoria(Categoria categoria);

        bool DeleteCategoria(int id);

        bool UpdateCategoria(Categoria categoria);
    }
}
