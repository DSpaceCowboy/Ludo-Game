using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_Game
{
    class Peao
    {

        private string cor;
        private int identificador;
        private int posicao;

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

        public int Posicao
        {
            get { return this.posicao; }
            set { this.posicao = value; }
        }

        public Peao(string cor, int identificador)
        {
            this.cor = cor;
            this.identificador = identificador;
            this.posicao = -1;
        }

    }
}