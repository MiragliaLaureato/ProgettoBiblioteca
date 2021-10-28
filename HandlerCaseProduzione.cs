using RepositoryBiblioteca.Model;
using RepositoryBiblioteca.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class HandlerCaseProduzione
    {
        private readonly ICasaProduzione CaseProduzione;
        public HandlerCaseProduzione(ICasaProduzione caseproduzione)
        {
            CaseProduzione = caseproduzione;
        }

        public CasaProduzione CreaCaseProduzione(string nome, string nazionalità)
        {
            var listaCaseProduzione = CaseProduzione.GetCasaProduzione();

            var exist = listaCaseProduzione.Where(x => x.Nome == nome && x.Nazionalità == nazionalità).SingleOrDefault();

            if (exist != null)
            {
                return exist;
            }

            var casaprod = new CasaProduzione
            {
                Nome = nome,
                Nazionalità = nazionalità
            };

            casaprod.IdCasaProduzione = CaseProduzione.CreateCasaProduzione(casaprod);
            return casaprod;
        }

        public bool existCasaProduzione(int idCasaProduzione)=>(CaseProduzione.GetCasaProduzione().Where(x=>x.IdCasaProduzione == idCasaProduzione)) !=null;

        public bool UpdateCasaProduzione(CasaProduzione casaProduzione) => CaseProduzione.UpdateCasaProduzione(casaProduzione);
        public bool DeleteCasaProduzione(int id) => CaseProduzione.KillcasaProduzione(id);

        public List<CasaProduzione> GetCaseProduzioniByName(string nome) => CaseProduzione.GetCasaProduzione().Where(x => x.Nome == nome).ToList();

        public List<CasaProduzione> GetCaseProduzioniByNazionalità(string nazionalità) => CaseProduzione.GetCasaProduzione().Where(x => x.Nazionalità == nazionalità).ToList();

        public List<CasaProduzione> GetCaseByNazionalità(string nazione) => CaseProduzione.GetCasaProduzione().Where(x => x.Nazionalità == nazione).ToList();

    }
}
