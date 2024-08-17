using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Jogo jogo = new Jogo();


            Console.WriteLine($"{jogo.Players[0].Peoes[0].Posicao}");

            Console.ReadLine();
        }
    }
}
