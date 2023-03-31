using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsServiceThread.Logic;

namespace WindowsServiceThread
{
    public partial class Service1 : ServiceBase
    {
        private static int _EsperaInicial;
        private static string _Periodo = string.Empty;
        private static int _Intervalo;
        private Thread mThread;
        const String fileName = "Daems_27-03-2023.txt";
        public Service1()
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
            //this.WriteToFile("Projeto Service transferred {0}");
            this.mThread.Abort();
        }
        private void WriteToFile(string text, string pathName = "C:\\ArquivosRecebidos")
        {

            string path = pathName+"\\" + fileName;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine();
                writer.WriteLine(string.Format(text + "  -  "+ pathName));
                writer.Close();
            }
        }
        protected void iniciar_Ambiente()
        {
            _EsperaInicial = (Convert.ToInt32(ConfigurationManager.AppSettings["ESPERAINICIAL_SEGUNDOS"]) * 1000);
            _Periodo = ConfigurationManager.AppSettings["PERIODO"].ToString();
            _Intervalo = (Convert.ToInt32(ConfigurationManager.AppSettings["INTERVALOMINUTO"]) * 60000);
            this.WriteToFile("Arquivo a ser transferido");
        }
        private void executar_Tarefa()
        {
            while (true)
            {
                Boolean AplicarIntervalo = false;
                try
                {
                    string caminhoOrigem = "C:\\ArquivosRecebidos";
                    string caminhoDestino = "C:\\ArquivosProcessados"; 
                    // Verifica se o arquivo existe no diretório de origem
                    if (!File.Exists(Path.Combine(caminhoOrigem, fileName)))
                    {
                        throw new FileNotFoundException("Arquivo não encontrado no diretório de origem", fileName);
                    }

                    // Verifica se o diretório de destino existe, caso não exista, cria
                    if (!Directory.Exists(caminhoDestino))
                    {
                        Directory.CreateDirectory(caminhoDestino);
                    }

                    // Move o arquivo para o diretório de destino
                    File.Move(Path.Combine(caminhoOrigem, fileName), Path.Combine(caminhoDestino, fileName));
                    this.WriteToFile("Arquivo Transferido", caminhoDestino);


                }
                catch (Exception ex)
                {
                  
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
