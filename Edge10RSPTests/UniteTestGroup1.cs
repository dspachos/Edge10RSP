using System;
using Edge10RSP;
using Xunit;

namespace Edge10RSPTests
{
    public class UniteTestGroup1
    {
        [Fact]
        public void TacticalValueTest() {
            Player p1 = new Player("Human Player",PlayType.manual);
            Player p2 = new Player("Random Computer Player",PlayType.random);
            Match match = new Match(5);
            match.player1 = p1;
            match.player2 = p2;
            Move rock = new Move("Rock");
            Move scissors = new Move("Scissors");
            Move paper = new Move("Paper");
            rock.Beats(scissors);
            scissors.Beats(paper);
            paper.Beats(rock);

            match.AddMove(rock);
            match.AddMove(scissors);
            match.AddMove(paper);

            match.MovesHistory.Add(new Tuple<int, int>(0, 0));
            Assert.Equal(2, match.GetTacticalMove(3));

            match.MovesHistory.Add(new Tuple<int, int>(1, 1));
            Assert.Equal(0, match.GetTacticalMove(3));

            match.MovesHistory.Add(new Tuple<int, int>(2, 2));
            Assert.Equal(1, match.GetTacticalMove(3));
        }

        [Fact]
        public void DoubleMoveLabelTest() {
            Match match = new Match();
            Move rock = new Move("Rock");
            Move rockLowercase = new Move("rock");
            Move scissors = new Move("Scissors");
            Assert.True(match.AddMove(rock));
            Assert.True(match.AddMove(scissors));
            Assert.False(match.AddMove(rockLowercase));
        }

    }
}
