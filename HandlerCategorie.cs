using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class HandlerCategorie
    {
        private readonly ICategorie Categorie;

        public HandlerCategorie(ICategorie categorie)
        {
            Categorie = categorie;
        }

        public Categoria CreaCategoria(string nome)
        {
            var listaCategorie = Categorie.GetCategorie();

            var exist = listaCategorie.Where(x => x.Nome == nome).SingleOrDefault();
            if(exist!=null)
            {
                return exist;
            }

            var categoria = new Categoria
            {
                Nome = nome
            };

            categoria.IdCategoria = Categorie.CreateCategoria(categoria);
            return categoria;
        }

        public bool ExistCategoria(int idCategoria) => (Categorie.GetCategorie().Where(x=>x.IdCategoria == idCategoria))!=null;

        public bool UpdateCategorie(Categoria categoria) => Categorie.UpdateCategoria(categoria);

        public bool DeleteCategoria(int id) => Categorie.DeleteCategoria(id);

       
    }
}
