# Edge10RSP

## A simple console application written in C# to process a match of rock, paper, scissors.

A match takes place between 2 players and is made up of 3 or more games, with the overall winner being the player who wins the most games.
Result may be a tie also.

Player 1 is always the (real) human player. Plays againist other types of players, such as

<ul>
<li>Human Player</li>
<li>Random Computer Player</li>
<li>Tactical Computer Player</li>
</ul>

The program supports unlimited types of players, but only 3 types of plays are supported: manual move, random move and tactical move.

The program also supports unlimited number of moves.

## Structure

There are 3 main classes, the `Match` class, the `Move` class, the `Player` class (and of course the `Main` class).

In the `Main` class, we create at first the types of the players, e.g.:

```csharp
Player p1 = new Player("Human Player", PlayType.manual);
Player p2 = new Player("Random Computer Player", PlayType.random);
...
```
and we add them in the `List`: `AvailablePlayers`

The real human player must choose one from the available players to play with.

We create the main `match`, an instance of the `Match` class.

We also create the moves for the match, with their according rules e.g.:

```csharp
Move rock = new Move("Rock");
Move scissors = new Move("Scissors");
rock.Beats(scissors);
...
```
and add them in the `AvailableMoves` list of the match object.

In each round (game) the real human player plays 1st.

We store the whole match history in a list (note: we store integers which represents the index of their items in the `AvailableMoves` list). We use that information to make tactical moves, and to calculate the final winner after the last game.

## Assumptions

1. In tactical moves, in the 1st game of each match, the tactical move is random (because there is no previous move history for the current match)

2. When comparing 2 moves, with no rules defined between them, the result is tie

3. If we have conflicting rules, e.g. `move1.Beats('move2)` and `move2.Beats('move1)` then in `CompareMoves(Move m1, Move m2)` function, left parameter wins


## Testing

I used the `Xunit` to write 2 very simple tests as a demonstration.
Cd to `Edge10RSPTests` and type from terminal `dotnet test` to run the tests.


## Tools

I worked on Mac OS, I installed the Visual Studio Community edition, but the actual coding was done on Visual Studio Code, the .Net Core SDK, some C# extension from the marketplace and the dotnet cli. Block comments generated with the Visual Studio (and I changed the text a little).

## What refactoring I could make in the future:

Some future refactoring can be:

1. Tactics improve: give the ability to add new tactics while for the match

2. Some better testing for more than 3 players and moves

3. Better implementation for console input and type checking

4. Some methods (such as `indexOf` performs a linear search; therefore, this method is an O(n) operation which I don't like, but's that ok for now. Probably for real-world apps I'd try to find something more efficient

5. Better code workflow and design of the `main` class

6. A proper way to <strong>mask</strong> the input of player 1 (e.g. use an asterix) for Human vs Human matches

Generally I'd spend some more time on designing the project (classes, members, methods, scope etc).

