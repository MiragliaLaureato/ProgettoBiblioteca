using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class HandlerImpiegati
    {
        private readonly IImpiegato Impiegati;

        public HandlerImpiegati(IImpiegato impiegati)
        {
            Impiegati = impiegati;
        }

        public Impiegato CreaImpiegato(string nome, string cognome)
        {
            var listaImpiegati = Impiegati.GetImpiegato();

            var exist = listaImpiegati.Where(x => x.Nome == nome && x.Cognome == cognome).SingleOrDefault();

            if(exist !=null)
            {
                return exist;

            }

            var impiegato = new Impiegato
            {
                Nome = nome,
                Cognome = cognome
            };

            impiegato.Id = Impiegati.CreateImpiegato(impiegato);
            return impiegato;
        }

        public bool ExistImpiegato(int idImpiegato) => (Impiegati.GetImpiegato().Where(x => x.Id == idImpiegato)) != null;

        public bool UpdateImpiegato(Impiegato impiegati) => Impiegati.UpdateImpiegato(impiegati);

        public bool DeleteImpiegato(int id) => Impiegati.KillImpiegato(id);

        public List<Impiegato> GetImpiegatiByNome(string nome) => Impiegati.GetImpiegato().Where(x => x.Nome == nome).ToList();

        public List<Impiegato> GetImpiegatiByCognome(string cognome) => Impiegati.GetImpiegato().Where(x=>x.Cognome == cognome).ToList();
    }
}
