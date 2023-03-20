//Gabriel Lemes de Oliveira 

using System;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string exit = "";
            while (exit != "0")
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                    Console.ResetColor();

                    //Obter os dados do Usuario
                    Console.Write("\nDigite o ano: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Digite o mês: ");
                    int month = int.Parse(Console.ReadLine());

                    //try-catch para validar a DateTime 
                    try
                    {
                        // Obter o primeiro dia da semana do mês
                        DateTime firtDay = new DateTime(year, month, 1);
                        int firstDayOfWeek = (int)firtDay.DayOfWeek;

                        // Obter o numero de dias no mes
                        int daysInMonth = DateTime.DaysInMonth(year, month);

                        // Imprimir o cabeçalho do calendário
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\n\n \t Calendario");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\n {0}   \t\t {1}", GetMonthName(month), year);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(" ============================");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(" Dom Seg Ter Qua Qui Sex Sáb");
                        Console.ResetColor();

                        //Primeiro espaçamento para inicio dos dias
                        for (int i = 0; i < firstDayOfWeek; i++)
                        {
                            Console.Write("    ");
                        }

                        // Imprimir os dias do mês
                        for (int i = 1; i <= daysInMonth; i++)
                        {

                          
                            //imprimir o número do dia do mês formatado com uma largura fixa de 3 caractere
                            Console.Write("{0,4}", i);

                            //Se for sabado pular uma linha 
                            if ((firstDayOfWeek + i - 1) % 7 == 6)
                            {
                                Console.WriteLine();
                            }
                        }
                    }
                    //Error quando a Data for invalida
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("\n  ERRO: A data especificada não é válida.");
                    }
                }

                //Error quando o usuario nao digita um numero 
                catch (FormatException)
                {
                    Console.WriteLine("\n  ERRO: Digite um número válido.");
                }

                //finally sera executado mesmo com erros
                finally
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\tDigite 0 para Sair...");
                    Console.ResetColor();
                    exit = Console.ReadLine();

                }
            }
        }

        // Função para obter o nome do mês a partir do número do mês
        static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "Janeiro";
                case 2:
                    return "Fevereiro";
                case 3:
                    return "Março";
                case 4:
                    return "Abril";
                case 5:
                    return "Maio";
                case 6:
                    return "Junho";
                case 7:
                    return "Julho";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setembro";
                case 10:
                    return "Outubro";
                case 11:
                    return "Novembro";
                case 12:
                    return "Dezembro";
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
