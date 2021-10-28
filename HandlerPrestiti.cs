using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;

namespace Logica
{
    public class HandlerPrestiti<T> where T : Oggetto
    {
        private readonly IOggetto<T> Repo;

        public IPrestito<T> Prestiti { get; }

        public HandlerPrestiti(IPrestito<T> prestiti, IOggetto<T> repo)
        {
            Prestiti = prestiti;
            Repo = repo;
        }



        public int CreatePrestito(Prestito<T> prestito)
        {
            if (Prestiti.GetPrestito().Where(x => x.ImpRestituzione == null && x.Oggetto.IdOggetto == prestito.Oggetto.IdOggetto).Any()) return -1;

            return Prestiti.InsertPrestito(prestito);
        }

        public Prestito<T> GetPrestitoBy(int idPrestito) => Prestiti.GetPrestito(idPrestito);

        public Prestito<T> SearchActive(int idOggetto)
        {
            return Prestiti.GetPrestito().Where(x => x.ImpRestituzione == null && x.Oggetto.IdOggetto == idOggetto).FirstOrDefault();
        }

        public bool SearchAvailable(int idOggetto)
        {
            return !Prestiti.GetPrestito().Where(x => x.ImpRestituzione == null && x.Oggetto.IdOggetto == idOggetto).Any();
        }

        public IEnumerable<T> GetDisponibili()
        {
            var listaOggetti = Repo.Get();
            var trovati = Prestiti.GetPrestito().Where(x=> x.ImpRestituzione == null).Select(x=>x.Oggetto.IdOggetto);
            return listaOggetti.Where(y=>!trovati.Contains(y.IdOggetto));
        }


        
            /*var provajoin = (from og in listaOggetti
                             join tr in trovati on og.IdOggetto equals tr
                             select og);*/
    }
}
