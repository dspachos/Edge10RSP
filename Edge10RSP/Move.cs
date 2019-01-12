using System;
using System.Collections.Generic;

namespace Edge10RSP
{
    /// <summary>
    /// Move class
    /// </summary>
    public class Move {
        public String Label { get; set; } // e.g. Rock, Paper, Scissors, Lizard, Spock
        private List<Move> BeatRules = new List<Move>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:rps2.Move"/> class.
        /// </summary>
        public Move() {
         this.Label = "Rock"; // default value
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:rps2.Move"/> class.
        /// </summary>
        /// <param name="moveLabel">Move label.</param>
        public Move(String moveLabel) {
            this.Label = moveLabel;
        }

        /// <summary>
        /// Inserts a move into the BeatRules list. That is, current move beats all moves in that list
        /// </summary>
        /// <param name="move">Move</param>
        public void Beats(Move move) {
            this.BeatRules.Add(move);
        }

        /// <summary>
        /// Finds if current move beats the move m
        /// </summary>
        /// <returns><c>true</c>, if parameter move exists in BeatRules list return true, <c>false</c> otherwise.</returns>
        /// <param name="m">Object Move</param>
        public bool FindIfBeats(Move m) {
            if ( this.BeatRules.Find(el => el.Label==m.Label)!=null ) {
                return true;
            }
            return false;
        }

    }
}


