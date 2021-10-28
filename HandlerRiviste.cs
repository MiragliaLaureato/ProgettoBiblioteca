using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class HandlerRiviste
    {

        public IOggetto<Rivista> Oggetti;

        public HandlerRiviste(IOggetto<Rivista> oggetti)
        {
            Oggetti = oggetti;
        }


        public Rivista CreaRivista(Rivista rivista)
        {
            var exist = ExistRivista(rivista.IdOggetto);
            if (exist != false) return rivista;

            var rv = new Rivista
            {
                Titolo = rivista.Titolo,
                DataUscita = rivista.DataUscita,
                Categoria = rivista.Categoria,
                CasaProduzione = rivista.CasaProduzione,
                Cancellato = false
            };

            rv.IdOggetto = Oggetti.Create(rv);
            return rv; 
        }

        public bool DeleteRivista(int idRivista)
        {
            if (Oggetti.Get(idRivista) != null)
                return Oggetti.Delete(idRivista);
            return false;
        }

        public bool ExistRivista(int idRivista)
        {
            
            var trovato = Oggetti.Get().Where(x => x.IdOggetto == idRivista);
            if (trovato != null) return true;
            return false; 
        }

        public bool UpdateRivista(Rivista rivista) => Oggetti.Get(rivista.IdOggetto) != null;
        
    }
}
