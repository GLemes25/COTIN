using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Logic
{
    public class UfService
    {
        private List<Uf> lista = new List<Uf>();

        public UfService()
        {
            CarregarListadeUf();
        }

        public List<Uf> GetUf(string uf)
        {

            return lista.Where(x => x.SiglaID.Contains(uf) || x.Nome.Contains(uf)).ToList();
        }
        public void PersistUf(Dictionary<string, string> dict)
        {
            foreach (var obj in dict)
            {
                var Uf = new Uf() { SiglaID = obj.Key, Nome = obj.Value };
                lista.Add(Uf);
            }

        }
        private void CarregarListadeUf()
        {

            Dictionary<string, string> dict = new Dictionary<string, string>();


            dict.Add("MT", "Mato Grosso");
            dict.Add("BH", "Bahia");
            dict.Add("MG", "Minas Gerais");
            dict.Add("PR", "Paraná");
            dict.Add("SC", "Santa Catarina");
            dict.Add("GO", "Goiás");
            dict.Add("RS", "Rio Grande do Sul");
            dict.Add("SP", "São Paulo");
            dict.Add("CE", "Ceará");
            dict.Add("PE", "Pernambuco");
            dict.Add("PA", "Pará");
            dict.Add("AM", "Amazonas");
            dict.Add("MA", "Maranhão");
            dict.Add("ES", "Espirito Santo");
            dict.Add("RJ", "Rio de Janeiro");
            dict.Add("PI", "Piauí");
            dict.Add("RN", "Rio Grande do Norte");
            dict.Add("PB", "Paraiba");
            dict.Add("MS", "Mato Grosso do Sul");
            dict.Add("AL", "Alagoas");
            dict.Add("RO", "Rondônia");
            dict.Add("RR", "Roraima");
            dict.Add("AC", "Acre");
            dict.Add("TO", "Tocantins");
            dict.Add("SE", "Sergipe");
            dict.Add("AP", "Amapá");

            PersistUf(dict);
        }
    }
}

