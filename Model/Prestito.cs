﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RepositoryBiblioteca.Model
{
    public class Prestito<T> where T:Oggetto
    {
        public int IdPrestito { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        public Utente Utente { get; set; }
        public Impiegato ImpPrestito { get; set; }
        public Impiegato ImpRestituzione { get; set; }
        public T Oggetto { get; set; }



        public Prestito()
        {

        }
    }

}