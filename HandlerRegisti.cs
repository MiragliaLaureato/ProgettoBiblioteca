using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class HandlerRegisti
    {
        public readonly IRegista Registi;

        public HandlerRegisti(IRegista registi)
        {
            Registi = registi;
        }


        public Regista CreaRegista(string nome, string cognome, string nazionalità)
        {
            var listaRegisti = Registi.GetRegista();
            var exist = listaRegisti.
                Where(x => x.Nome == nome
                && x.Cognome == cognome
                && x.Nazionalità == nazionalità).SingleOrDefault();

            if (exist != null) return exist;

            var regista = new Regista
            {
                Nome = nome,
                Cognome = cognome,
                Nazionalità = nazionalità,
                Cancellato = false
            };

            regista.Id = Registi.CreateRegista(regista);
            return regista;
        }



        public bool existRegista(int idRegista)
        {
            var listaRegisti = Registi.GetRegista();
            var trovato = listaRegisti.Where(x => x.Id == idRegista);
            if (trovato != null) return true;
            return false;
        }

        public bool UpdateRegista(Regista regista)
        {
            var check = Registi.GetRegista(regista.Id);
            if (check != null)
            {
                check = regista;
                return true;
            }
            return false;
        }



    }
}
