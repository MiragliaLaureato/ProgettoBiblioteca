using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class HandlerAutori 
    {
        private readonly IAutore Autori;

        public HandlerAutori(IAutore autori)
        {
            Autori = autori; 
        }
        

        public Autore CreaAutore(string nome, string cognome, string nazionalità)
        {
            var listaAutori = Autori.GetAutori();

            var exist = listaAutori.Where(x => x.Nome == nome && x.Cognome == cognome && x.Nazionalità == nazionalità).SingleOrDefault();

            if (exist != null)
            {
                return exist;
            }

            var autore = new Autore
            {
                Nome = nome,
                Cognome = cognome,
                Nazionalità = nazionalità,
                Cancellato = false
            };

           
            autore.Id = Autori.CreateAutore(autore);
            return autore;
        }

        public bool ExistAutore(int idAutore) => (Autori.GetAutori().Where(x => x.Id == idAutore)) != null;

        public bool UpdateAutore(Autore autore) => Autori.UpdateAutore(autore);
        
        public bool DeleteAutore(int id) => Autori.DeleteAutore(id);

        public List<Autore> GetAutoriByNome(string nome) =>  Autori.GetAutori().Where(x => x.Nome == nome).ToList();

        public List<Autore> GetAutoriByCognome(string cognome) => Autori.GetAutori().Where(x => x.Cognome == cognome).ToList();
        
        public List<Autore> GetAutoriByNazionalità(string nazionalità) => Autori.GetAutori().Where(x => x.Nazionalità == nazionalità).ToList();
        
    }
}
