using System;
using System.Collections.Generic;
using System.Linq;


namespace TreinamentoCotin
{
    public class Exercicios
    {
        Dictionary<string, Action> Exemplos;
        public Exercicios(Dictionary<string, Action> exercicios)
        {
            Exemplos = exercicios;
        }

        public void Processar()
        {
            do
            {


                int i = 1;

                foreach (var exercicio in Exemplos)
                {
                    Console.WriteLine("{0}) {1}", i, exercicio.Key);
                    i++;
                }
                Console.Write("Digite o número (ou vazio para o último)? ou Ctrl+C Finaliza o Programa. ");

                int.TryParse(Console.ReadLine(), out int num);
                bool numValido = num > 0 && num <= Exemplos.Count;
                num = numValido ? num - 1 : Exemplos.Count - 1;

                string nomeDoExercicio = Exemplos.ElementAt(num).Key;

                Console.Write("\nExecutando exercício ");
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(nomeDoExercicio);
                Console.ResetColor();

                Console.WriteLine(String.Concat(
                    Enumerable.Repeat("=", nomeDoExercicio.Length + 21)) + "\n");

                Action executar = Exemplos.ElementAt(num).Value;
                try
                {
                    executar();
                }
                catch (Exception e)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Ocorreu um erro: {0}", e.Message);
                    Console.ResetColor();

                    Console.WriteLine(e.StackTrace);
                }
                Console.ReadKey();

            } while (true);

        }
    }
}
