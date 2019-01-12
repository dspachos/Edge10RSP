using System;
using System.Collections.Generic;
using static System.Console;

namespace Edge10RSP
{
    /// <summary>
    /// Match class
    /// </summary>
    public class Match {
        public int NumberOfTotalGames { get; set; }
        public int NumberOfGamesPlayed { get; set; }
        public Player player1 { get; set; } // It is always the real user that playes the game
        public Player player2 { get; set; } // Can be any of players defined in program

        private List<Move> AvailableMoves { get; set; }
        public List<Tuple<int, int>> MovesHistory { get; set; }

        /// <summary>
        /// Default constructor. Initializes a new instance of the <see cref="T:rps2.Match"/> class.
        /// </summary>
        public Match() {
            this.NumberOfTotalGames = 3;
            this.NumberOfGamesPlayed = 0;
            this.AvailableMoves = new List<Move>();
            this.MovesHistory = new List<Tuple<int, int>>();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:rps2.Match"/> class.
        /// </summary>
        /// <param name="numberOfTotalGames">Number of total games.</param>
        public Match(int numberOfTotalGames) {
            numberOfTotalGames = Math.Abs(numberOfTotalGames);
            /* if(numberOfTotalGames%2==0) {
                numberOfTotalGames++; //  Convert to odd, increase chances to have winner
            } */
            this.NumberOfTotalGames = numberOfTotalGames;
            this.AvailableMoves = new List<Move>();
            this.MovesHistory = new List<Tuple<int, int>>();
        }

        /// <summary>
        /// Helper function: Displays the available moves.
        /// </summary>
        public void DisplayAvailableMoves() {
            WriteLine("Available moves:");
            foreach (var m in this.AvailableMoves)
            {
                WriteLine("{0}. {1}", this.AvailableMoves.IndexOf(m)+1 , m.Label);
            }
            RSPGame.printMainMenuOption();
        }

        /// <summary>
        /// Helper function: Displays the match history.
        /// </summary>
        public void DisplayMatchHistory() {
            WriteLine("\nMatch move history:");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Game       \tPlayer 1       \tPlayer 2       \tWinner");
            ResetColor();

            int gameRound = 1;
            int CompareMovesSum = 0;
            foreach (var move in this.MovesHistory) {
                var label1 = this.AvailableMoves[move.Item1].Label;
                var label2 = this.AvailableMoves[move.Item2].Label;
                var result = RSPGame.CompareMoves(this.AvailableMoves[move.Item1],this.AvailableMoves[move.Item2]);
                CompareMovesSum += result;

                string gameWinner = "Tie";
                if(result<0) gameWinner="Player 1";
                else if(result>0) gameWinner="Player 2";
                WriteLine("Game {0}   \t{1,-10}   \t{2,-10}   \t{3}" , gameRound++, label1, label2, gameWinner);
            }

            string winner;
            if (CompareMovesSum<0) winner="Player 1";
            else if (CompareMovesSum>0) winner="Player 2";
            else winner="Tie";
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Match winner: \t{0}", winner);
            ResetColor();

        }

        /// <summary>
        /// Gets a tactical move based on previous Player 1 move. If we are currently in Game 1 (no previous move), then a random move is returned
        /// </summary>
        /// <returns>The tactical move.</returns>
        /// <param name="maxRange">Max range to choose a move from the list of available moves.</param>
        public int GetTacticalMove(int maxRange) {
            // if this is game 1 (not previous move from Player 1) return random
            if(this.MovesHistory.Count==0) {
                return new Random().Next(1, maxRange);
            } else {
                // Previous move of Player 1
                int item = this.MovesHistory[this.MovesHistory.Count - 1].Item1;
                Move previousMove = AvailableMoves[item];
                foreach (var moveItem in AvailableMoves) {
                    if(moveItem.FindIfBeats(previousMove)) {
                        // this move beats previous Player 1 move
                        return AvailableMoves.FindIndex(a => a.Label == moveItem.Label);
                    }
                } //end foreach

                return new Random().Next(1, maxRange); // Added because of a warning in editor and just in case I missed something
            } //end else

        }

        public bool AddMove(Move move) {
            if(AvailableMoves.FindIndex(a => a.Label.ToLower() == move.Label.ToLower()) == -1) {
                AvailableMoves.Add(move);
                return true;
            } else {
                WriteLine("Move with label {0} exists!",move.Label);
            }
            return false;
        }


        public int GetMaxNumberOfMoves(){
            return AvailableMoves.Count;
        }

        /// <summary>
        /// Helper function: Gets the move label.
        /// </summary>
        /// <returns>The move label.</returns>
        /// <param name="index">Index.</param>
        public string GetMoveLabel(int index) {
            return this.AvailableMoves[index].Label;
        }
    }
}


