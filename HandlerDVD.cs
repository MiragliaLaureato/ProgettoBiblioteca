using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logica
{
    public class HandlerDVD
    {
        public readonly DVDs Dvds;

        public HandlerDVD(DVDs dvds)
        {
            Dvds = dvds; 
        }

        public IEnumerable<DVD> GetFilmFromRegista(string nome, string cognome) => Dvds.Get().Where(x => x.Regista.Nome == nome && x.Regista.Cognome == cognome);
        
        public IEnumerable<DVD> GetDisponibili()
        {
            return Dvds.Get();
            
        }

        public IEnumerable<DVD> GetByCasaProduzione(string nazionalità) => Dvds.Get().Where(x => x.CasaProduzione.Nazionalità == nazionalità);
    }
}
