using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_Game
{
    class Tabuleiro
    {
        private int[][] houses;
        Jogador[] players;


        public int[][] Houses
        {
            get { return houses; }
            set { houses = value; }
        }


        public Tabuleiro(int playersQuant, Jogador[] players)
        {
            houses = new int[playersQuant][];
            this.players = players;
            for (int i = 0; i < playersQuant; i++)
            {
                houses[i] = new int[57];
                for (int j = 0; j < houses[i].Length; j++)
                {
                    houses[i][j] = j + 1;

                    if (j % 13 == 0 || j == 8 || j == 21 || j == 34 || j == 47)
                    {
                        houses[i][j] = 0;
                    }
                }
            }
        }

        public void MovePawn(int playerId, int pawnId, int steps)
        {
            if (playerId < 1 || playerId > players.Length)
            {
                Console.WriteLine("ID inválido");
                return;
            }


            Jogador currentPlayer = players[playerId - 1];
            Peao pawnToMove = currentPlayer.Peoes[pawnId - 1];
            int newPosition = (pawnToMove.Posicao + steps) % 56;

            Console.WriteLine($"{currentPlayer.Nome} está movendo o peão {pawnToMove.Identificador} para a posição {newPosition}");

            if (CheckSafeHouse(playerId - 1, newPosition))
            {
                Console.WriteLine($"O peão {pawnToMove.Identificador} do jogador {currentPlayer.Nome} está na casa segura {newPosition}");
            }
            else if (CheckCapture(playerId - 1, newPosition))
            {
                Console.WriteLine($"O peão {pawnToMove.Identificador} do jogador {currentPlayer.Nome} capturou um peão!");
            }
            if(CheckFinalLine(newPosition))
            {
                Console.WriteLine($"O peão {pawnToMove.Identificador} do jogador {currentPlayer.Nome} entrou na trilha final");
            }

            pawnToMove.Posicao = newPosition;
        }

        public bool CheckFinalLine(int position)
        {
            if(position >= 51 && position <= 55)
            {
                return true;
            }
            return false;
        }


        public bool CheckSafeHouse(int player, int position)
        {
            if (position >= 0 && position < houses[player].Length)
            {
                if (houses[player][position] == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CheckCapture(int playerIndex, int position)
        {
            int relativePosition = (position + playerIndex * 13) % 56;
            foreach (Jogador player in players)
            {
                if (player.Identificador != playerIndex + 1)
                {
                    foreach (Peao pawn in player.Peoes)
                    {
                        int oppRelativePosition = (pawn.Posicao + (player.Identificador - 1) * 13) % 56;
                        if (relativePosition == oppRelativePosition)
                        {
                            pawn.Posicao = -1;
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        public bool CheckWinner()
        {
            foreach (Jogador player in players)
            {
                bool allPawnsInPosition = true;
                foreach (Peao pawn in player.Peoes)
                {
                    if (pawn.Posicao != 56)
                    {
                        allPawnsInPosition = false;
                        break;
                    }
                }

                if (allPawnsInPosition)
                {
                    Console.WriteLine($"Jogador {player.Nome} venceu o jogo!");
                    return true;
                }
            }

            return false;
        }

    }
}
