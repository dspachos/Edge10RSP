using System;
using System.Collections.Generic;
using static System.Console;

namespace Edge10RSP
{
    public enum PlayType {
        manual = 1,
        random = 2,
        tactical = 3
    };

    class RSPGame {
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args) {

            // Create available players to play with
            Player p1 = new Player("Human Player",PlayType.manual);
            Player p2 = new Player("Random Computer Player",PlayType.random);
            Player p3 = new Player("Tactical Computer Player",PlayType.tactical);
            List<Player> AvailablePlayers = new List<Player>();
            AvailablePlayers.Add(p1);
            AvailablePlayers.Add(p2);
            AvailablePlayers.Add(p3);

            String s="";
            do {
                if(s.ToUpper()=="N") {
                    DisplayAvailablePlayers(AvailablePlayers);
                    Write("Your choice:");
                    int player2Index = GetUserInput(AvailablePlayers.Count);
                    if(player2Index!=0) {
                        // Let's set up and play the actual match now
                        Match mainMatch = new Match();
                        Player player1 = new Player("Player 1",PlayType.manual);
                        Player player2 = AvailablePlayers[player2Index-1];

                        mainMatch.player1 = player1;
                        mainMatch.player2 = player2;

                        // Create available moves
                        Move rock = new Move("Rock");
                        Move scissors = new Move("Scissors");
                        Move paper = new Move("Paper");

                        // Some rules
                        rock.Beats(scissors);
                        scissors.Beats(paper);
                        paper.Beats(rock);

                        mainMatch.AddMove(rock);
                        mainMatch.AddMove(scissors);
                        mainMatch.AddMove(paper);

                        // Ready to play
                        PlayMatch(ref mainMatch);

                        mainMatch.DisplayMatchHistory();
                        Write("\nPress any key to continue");
                        ReadKey();
                        Clear();
                    }
                } else if(s.Length>0)
                {
                    WriteLine("Invalid option. Try again");
                }
                WriteLine("You are Player 1. Please select");
                WriteLine("(N) New game");
                WriteLine("(Q) Quit program");
                ;
            } while ( (s = ReadLine()).ToUpper() != "Q" );
        }

        /// <summary>
        /// Displays the available players
        /// </summary>
        /// <param name="availablePlayersList">Available players list.</param>
        static void DisplayAvailablePlayers(List<Player> availablePlayersList )
        {
            WriteLine("Play againist:");
            foreach (Player p in availablePlayersList)
            {
                WriteLine("{0}. {1}", availablePlayersList.IndexOf(p)+1 , p.PlayerName); // indexOf method performs a linear search; therefore, this method is an O(n) operation which I don't like, but's that ok for now
            }
            RSPGame.printMainMenuOption();
        }

        /// <summary>
        /// Method that plays the actual match
        /// </summary>
        /// <param name="match">Object match</param>
        static void PlayMatch(ref Match match) {
            if (match == null) {
                throw new ArgumentNullException(nameof(match));
            }
            //Clear();
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Match starts! \nYou (Player 1) vs {0} (Player 2)", match.player2.PlayerName);
            ResetColor();

            int maxMoveRange = match.GetMaxNumberOfMoves();
            int p1Move=0;
            int p2Move=0;
            match.DisplayAvailableMoves();

            while(match.NumberOfGamesPlayed<match.NumberOfTotalGames)
            {
                WriteLine("\nGame {0}",match.NumberOfGamesPlayed+1);
                Write("Your move:");
                p1Move = GetUserInput(maxMoveRange)-1;

                if(p1Move==-1) // User choose 0 to move to the main menu
                    break;

                // Player's 2 move
                switch (match.player2.PlayerType) {
                    case PlayType.manual:
                        Write("Player's 2 move:");
                        p2Move = GetUserInput(maxMoveRange)-1;
                        break;
                    case PlayType.random:
                        p2Move = GetRandomMove(maxMoveRange);
                        break;
                    case PlayType.tactical:
                        p2Move = match.GetTacticalMove(maxMoveRange);
                        break;
                    default:
                        break;
                }
                WriteLine("You played: \t {0}", match.GetMoveLabel(p1Move));
                WriteLine("Player 2: \t {0} ", match.GetMoveLabel(p2Move));
                match.MovesHistory.Add(new Tuple<int, int>(p1Move, p2Move));
                match.NumberOfGamesPlayed++;
            }
            return;
        }


        /// <summary>
        /// Helper fucntion: Compares two moves
        /// </summary>
        /// <returns>-1 if m1 beats m2, 1 if m2 beats m1, 0 otherwise</returns>
        /// <param name="m1">Move object m1</param>
        /// <param name="m2">Move object m2</param>
        public static int CompareMoves(Move m1, Move m2) {
            if ( m1.FindIfBeats(m2)) {
                return -1;
            } else if ( m2.FindIfBeats(m1)) {
                return 1;
            }

            return 0;
        }


        /// <summary>
        /// Helper function:: Gets integer input from command line between 0 and maxRange
        /// </summary>
        /// <returns>The user input as an integer</returns>
        /// <param name="maxRange">MaxRange: defines the upper max that is accepted from user</param>
        static int GetUserInput(int maxRange) {
            String input;
            int returnValue=0;
            do {
                if(returnValue<0 || returnValue>maxRange) {
                    Write("Invalid option. Try again:");
                }

                input = ReadLine();
                if(Int32.TryParse(input, out returnValue)) {
                    returnValue = Convert.ToInt16(input);
                } else {
                    returnValue=-1;
                }
           } while(returnValue<0 || returnValue>maxRange);
            return returnValue;
        } //end GetUserInput

        static int GetRandomMove(int maxValue) {
            Random random = new Random();
            int randomNumber = random.Next(0, maxValue-1);
            return randomNumber;
        } // end GetRandomMove

        public static void printMainMenuOption()
        {
            ForegroundColor = ConsoleColor.Blue;
            WriteLine("0. Exit and back to main menu");
            ResetColor();
        }

   }
}


