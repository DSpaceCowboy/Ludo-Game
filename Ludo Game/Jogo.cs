using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo_Game
{
    class Jogo
    {
        Jogador[] players;
        Tabuleiro board;
        public Jogador[] Players
        {
            get { return players; }
            set { players = value; }
        }

        public Tabuleiro Board
        {
            get { return board; }
            set { board = value; }
        }


        public string GetPlayerName(int playerNum)
        {
            Console.WriteLine($"Digite seu nome");
            return Console.ReadLine();
        }

        private readonly bool[] availableColors = { true, true, true, true };
        public string GetPlayerColor()
        {
            string[] colors = { "Vermelho", "Verde", "Amarelo", "Azul" };
            Console.WriteLine("Escolha uma cor:");

            while (true)
            {
                Console.WriteLine("1 - Vermelho\n2 - Verde\n3 - Amarelo\n4 - Azul");
                int escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {
                    case 1:
                        if (availableColors[0])
                        {
                            availableColors[0] = false;
                            return colors[0];
                        }
                        break;
                    case 2:
                        if (availableColors[1])
                        {
                            availableColors[1] = false;
                            return colors[1];
                        }
                        break;
                    case 3:
                        if (availableColors[2])
                        {
                            availableColors[2] = false;
                            return colors[2];
                        }
                        break;
                    case 4:
                        if (availableColors[3])
                        {
                            availableColors[3] = false;
                            return colors[3];
                        }
                        break;
                    default:
                        Console.WriteLine("Escolha inválida, favor escolher uma das opções acima.");
                        break;
                }
                Console.WriteLine("Cor já escolhida por outro jogador. Escolha outra cor.");
            }
        }


        public void StartGame(int playerNum)
        {
            Console.WriteLine($"Jogador {players[0].Nome}, ID: {players[0].Identificador} começará a jogada");
            Console.WriteLine();
            while (!board.CheckWinner())
            {
                for (int i = 0; i < playerNum; i++)
                {
                    bool playerTurn = true;
                    int rollsOfSix = 0;
                    int pawnChoice = 0;

                    while (playerTurn)
                    {
                        Console.WriteLine($"Turno do jogador {players[i].Nome}");
                        Console.WriteLine();
                        int rollResult = players[i].LaunchDice();
                        if (rollResult == 6)
                        {
                            rollsOfSix++;
                            if (rollsOfSix == 3)
                            {
                                Console.WriteLine($"Jogador {players[i].Nome} rolou três 6 consecutivos e perdeu a vez!");
                                playerTurn = false;
                                break;
                            }
                        }

                        if (rollResult == 6)
                        {
                            bool pawnsToInitialize = false;
                            foreach (Peao pawn in players[i].Peoes)
                            {
                                if (pawn.Posicao == -1)
                                {
                                    pawnsToInitialize = true;
                                    break;
                                }
                            }
                            if (pawnsToInitialize)
                            {

                                Console.WriteLine($"Jogador {players[i].Nome}, ID: {players[i].Identificador} pode colocar um peão de sua escolha na trilha");
                                Console.WriteLine();
                                do
                                {
                                    Console.WriteLine("Escolha um peão para mover (1-4):");
                                    pawnChoice = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    if (pawnChoice < 1 || pawnChoice >= 5)
                                    {
                                        Console.WriteLine("Id de peão inválido, escolha o índice de um peão válido (1-4).");
                                    }
                                    else if (!players[i].InitializePawnAvailable(pawnChoice))
                                    {
                                        Console.WriteLine($"Peão {pawnChoice} já está na trilha, escolha outro.");
                                    }
                                } while (!players[i].InitializePawnAvailable(pawnChoice));

                                players[i].Peoes[pawnChoice - 1].Posicao = 0;
                                Console.WriteLine($"Jogador {players[i].Nome}, ID: {players[i].Identificador} iniciou seu peão {players[i].Peoes[pawnChoice - 1].Identificador} na posição {players[i].Peoes[pawnChoice - 1].Posicao}");
                            }

                            playerTurn = true;
                        }
                        else
                        {
                            bool pawnsToMove = false;
                            foreach (Peao pawn in players[i].Peoes)
                            {
                                if (pawn.Posicao >= 0 && pawn.Posicao < 56)
                                {
                                    pawnsToMove = true;
                                    break;
                                }
                            }
                            if (pawnsToMove)
                            {

                                do
                                {
                                    Console.WriteLine("Escolha um peão para mover (1-4):");
                                    pawnChoice = int.Parse(Console.ReadLine());
                                    if (pawnChoice < 1 || pawnChoice >= 5)
                                    {
                                        Console.WriteLine("Id de peão inválido, escolha o índice de um peão válido (1-4).");
                                    }
                                    else if (!players[i].MoveAvailablePawn(pawnChoice))
                                    {
                                        Console.WriteLine($"Peão {pawnChoice} não foi inicializo no tabuleiro ou já atingiu a posição final");
                                    }

                                } while (!players[i].MoveAvailablePawn(pawnChoice));

                                board.MovePawn(i + 1, pawnChoice, rollResult);
                                Console.WriteLine($"Jogador {players[i].Nome}, ID: {players[i].Identificador} moveu seu peão {players[i].Peoes[pawnChoice - 1].Identificador} para a posição {players[i].Peoes[pawnChoice - 1].Posicao}");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine($"Jogador {players[i].Nome}, ID: {players[i].Identificador} rolou um {rollResult} mas não possui peões na trilha para mover.");
                                Console.WriteLine();
                            }

                            //if (players[i].Peoes[pawnChoice - 1].Posicao + rollResult > board.Houses[i].Length - 1)
                            //{
                            //    Console.WriteLine($"Peão {players[i].Peoes[pawnChoice - 1]} excederá o número de casas permitido. Jogador {players[i].Nome} deve escolher outro peão para mover ou passar a vez caso não tenha peões restantes na trilha");
                            //}
                            //else if (players[i].Peoes[pawnChoice - 1].Posicao + rollResult == 56)
                            //{
                            //    players[i].Peoes[pawnChoice - 1].Posicao = 56;
                            //    Console.WriteLine($"Peão {players[i].Peoes[pawnChoice - 1].Identificador} do jogador {players[i].Nome} chegou na posição { players[i].Peoes[pawnChoice - 1].Posicao} e completou seu ciclo no tabuleiro");
                            //}
                            playerTurn = false;
                        }
                    }
                }
            }
        }

        public void FinishGame()
        {
            if(board.CheckWinner())
            {
                Console.WriteLine("Jogo finalizado");
            }
        }
        public Jogo()
        {

            int playersQuant;
            do
            {
                Console.WriteLine("Digite a quantidade de jogadores");
                playersQuant = int.Parse(Console.ReadLine());
                if (playersQuant < 2 || playersQuant > 4)
                {
                    Console.WriteLine("Número de jogadores inválido. O número permitido é de 2 a 4 jogadores");
                }
            } while (playersQuant < 2 || playersQuant > 4);



            players = new Jogador[playersQuant];
            for (int i = 0; i < players.Length; i++)
            {
                Console.WriteLine($"Jogador {i + 1}:");
                string playerName = GetPlayerName(i + 1);
                string color = GetPlayerColor();
                players[i] = new Jogador(color, i + 1, playerName);
            }
            board = new Tabuleiro(playersQuant, players);

            StartGame(playersQuant);
            FinishGame();
        }

    }
}
