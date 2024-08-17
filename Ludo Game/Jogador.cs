using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_Game
{
    class Jogador
    {
        private string cor;
        private int identificador;
        private string nome;
        private Peao[] peoes;

        public string Cor
        {
            get { return this.cor; }
            set { this.cor = value; }
        }

        public int Identificador
        {
            get { return this.identificador; }
            set { this.identificador = value; }
        }

        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
        }

        public Peao[] Peoes
        {
            get { return this.peoes; }
            set { this.peoes = value; }
        }

        public Jogador(string cor, int identificador, string nome)
        {
            this.cor = cor;
            this.identificador = identificador;
            this.nome = nome;
            this.peoes = new Peao[4];
            for (int i = 0; i < peoes.Length; i++)
            {
                peoes[i] = new Peao(cor, i + 1);
            }
        }

        private static Random r = new Random();

        public int LaunchDice()
        {
            int roll = r.Next(1, 7);
            Console.WriteLine($"Jogador {Nome}, {Identificador} rolou um {roll}");
            return roll;
        }

        public bool InitializePawnAvailable(int pawnID)
        {
            if (pawnID > 0 && pawnID <= peoes.Length)
            {
                if (peoes[pawnID - 1].Posicao == -1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool MoveAvailablePawn(int pawndID)
        {
            if(pawndID > 0 && pawndID <= peoes.Length)
            {
                if(peoes[pawndID -1].Posicao >=0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
