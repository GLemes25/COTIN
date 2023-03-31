using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Logic
{
    public class Service
    {
        public static Boolean PossoExecutarServico(string Periodo)
        {
            try
            {
                String[] Periodos = Periodo.Split(';');
                Int32 _HHmmNow = Convert.ToInt32(DateTime.Now.ToString("HHmm"));
                Int32 _HHmmIni = 0;
                Int32 _HHmmFim = 0; Boolean DentroDoIntervalo = false;
                Boolean PossoExecutar = true;
                int i = 0;
                while (i < Periodos.Length)
                {
                    if ((!string.IsNullOrEmpty(Periodos[i])) & (!string.IsNullOrEmpty(Periodos[i])))
                    {
                        _HHmmIni = Convert.ToInt32(Periodos[i].Replace(":", ""));
                        _HHmmFim = Convert.ToInt32(Periodos[i + 1].Replace(":", ""));
                        DentroDoIntervalo = (DentroDoIntervalo | (_HHmmNow >= _HHmmIni) & (_HHmmNow <= _HHmmFim));
                    }
                    i += 2;
                }
                return (DentroDoIntervalo & PossoExecutar);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
