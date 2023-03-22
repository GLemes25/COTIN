using NorthWind.Data.Logic.Data;
using NorthWind.Data.Logic.Interface;
using NorthWind.Data.Logic.Repository;
using NorthWindService.Logic;
using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;

namespace NorthWindService
{
    public partial class ServiceNorthWind : ServiceBase
    {
        private static int _EsperaInicial;
        private static string _Periodo = string.Empty;
        private static int _Intervalo;
        private Thread mThread;
        public ServiceNorthWind()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.iniciar_Ambiente();
            this.preparar_Thread();
        }

        protected override void OnStop()
        {
        }

        private void WriteToFile(string text)
        {
            string path = "C:\\Users\\mathe\\source\\repos\\CriandoProgramaComWilson\\Log\\ProjetoServiceLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }
        protected void iniciar_Ambiente()
        {
            _EsperaInicial = (Convert.ToInt32(ConfigurationManager.AppSettings["ESPERAINICIAL_SEGUNDOS"]) * 1000);
            _Periodo = ConfigurationManager.AppSettings["PERIODO"].ToString();
            _Intervalo = (Convert.ToInt32(ConfigurationManager.AppSettings["INTERVALOMINUTO"]) * 60000);
            this.WriteToFile("Projeto Service Ambiente Started {0}");
        }

        private void executar_Tarefa()
        {
            while (true)
            {
                Boolean AplicarIntervalo = false;
                try
                {
                    if (Servico.PossoExecutarServico(_Periodo))
                    {

                        IRepository<Orders> _Repository = new Repository<Orders>();

                        var listaOrders = _Repository.ObterTodos();

                        foreach (var item in listaOrders)
                        {
                            var dataAnterior = item.OrderDate;
                            item.OrderDate = DateTime.Now;
                            _Repository.Alterar(item);
                            this.WriteToFile("Order número: " + item.OrderID + " Data anterior: " + dataAnterior + " Nova Data: " + item.OrderDate + "{0}");
                        }

                        _Repository.Salvar();

                    }
                }
                catch (Exception ex)
                {
                    this.WriteToFile("Projeto Service Error on: {0} " + ex.Message + ex.StackTrace);
                    AplicarIntervalo = true;
                }
                finally
                {
                    if (AplicarIntervalo)
                    {
                        Thread.Sleep(_Intervalo);
                    }
                }
            }
        }
        private void preparar_Thread()
        {
            Thread.Sleep(_EsperaInicial);
            ThreadStart job = new ThreadStart(executar_Tarefa);
            this.mThread = new Thread(job);
            this.mThread.Start();
        }
    }
}
