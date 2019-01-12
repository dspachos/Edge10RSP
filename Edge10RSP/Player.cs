using System;

namespace Edge10RSP
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player {
        public String PlayerName { get; set; } // E.g. Human, Random Computer, Tactical Computer
        public PlayType PlayerType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:rps2.Player"/> class.
        /// </summary>
        public Player() {
            this.PlayerName = "Human"; // by default every new Player will be human
            this.PlayerType = PlayType.manual;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:rps2.Player"/> class.
        /// </summary>
        /// <param name="playerName">Player name.</param>
        /// <param name="playerType">Player type.</param>
        public Player(String playerName, PlayType playerType) {
            this.PlayerName = playerName;
            this.PlayerType = playerType;
        }

    }
}


