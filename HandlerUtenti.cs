using RepositoryBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryBiblioteca.Repository;

namespace Logica
{
    public class HandlerUtenti
    {
        private readonly IUtente Utenti;

        public HandlerUtenti(IUtente utenti)
        {
            Utenti = utenti;
        }

        public Utente CreaUtente(string nome, string cognome, string numtelefono)
        {
            var listautenti = Utenti.GetUtente();


            var utenteExsist = listautenti
                .Where(x => x.Nome == nome && x.Cognome == cognome && x.NumTelefono == numtelefono)
                .SingleOrDefault();

            if (utenteExsist != null)
                return utenteExsist;

            var utente = new Utente
            {
                Nome = nome,
                Cognome = cognome,
                NumTelefono = numtelefono,
                Cancellato = false
            };
         
            utente.Id = Utenti.CreateUtente(utente);
            return utente;

        }

        public bool ExistUtente(int idutente) => (Utenti.GetUtente().Where(x => x.Id == idutente).SingleOrDefault() != null);
        
        public bool DeleteUtente(int idutente)
        {
            var u = Utenti.GetUtente(idutente);
            if (u != null)
            {
                return Utenti.KillUtente(idutente);
            }
            return false; 
        }

        public bool Updateutente(Utente utente)
        {
            var check = Utenti.GetUtente(utente.Id);
            if (check != null)
            {
                return Utenti.UpdateUtente(utente);
            }
            return false; 
        }

        public List<Utente> GetUtentiByName(string nome) => Utenti.GetUtente().Where(x => x.Nome == nome).ToList();

        public List<Utente> GetUtenteByCognome(string cognome) => Utenti.GetUtente().Where(x => x.Cognome == cognome).ToList();
       
        public List<Utente> GetUtenteByNumTelefono(string num) => Utenti.GetUtente().Where(x => x.NumTelefono == num).ToList();

        
    }
}
