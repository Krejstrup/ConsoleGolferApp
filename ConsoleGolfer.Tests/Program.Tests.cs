using Xunit;

namespace ConsoleGolfer.Tests
{
    public class ProgramTests
    {



        [Fact]
        public void NextPlayer_AllPlayersOneStrike_FirstPlayerLeastStrikes() // Invalid by NextPlayer refactoring to "furthest away"
        {
            //Arrange
            string expectedPlayer = "Harry";
            Indexer.ResetPlayersId();
            Golf myGolfPlay = new Golf();

            Player myPlayer = new Player(Indexer.NextPlayerId(), "Harry");
            Strike myStrike = new Strike(Indexer.NextStrikeId(), 230);
            myPlayer.AddStrike(myStrike);
            //myStrike = new Strike(Indexer.NextStrikeId(), 201);
            //myPlayer.AddStrike(myStrike);
            myGolfPlay.AddPlayer(myPlayer);

            myPlayer = new Player(Indexer.NextPlayerId(), "Barry");
            myStrike = new Strike(Indexer.NextStrikeId(), 130);
            myPlayer.AddStrike(myStrike);
            //myStrike = new Strike(Indexer.NextStrikeId(), 80);
            //myPlayer.AddStrike(myStrike);
            myGolfPlay.AddPlayer(myPlayer);

            myPlayer = new Player(Indexer.NextPlayerId(), "Merry");
            myStrike = new Strike(Indexer.NextStrikeId(), 230);
            myPlayer.AddStrike(myStrike);
            myGolfPlay.AddPlayer(myPlayer);

            myPlayer = new Player(Indexer.NextPlayerId(), "Cherry");
            myStrike = new Strike(Indexer.NextStrikeId(), 218);
            myPlayer.AddStrike(myStrike);
            myGolfPlay.AddPlayer(myPlayer);


            //Act
            string theNextPLayer = myGolfPlay.NextPlayer().Name;

            //Assert
            Assert.Equal(expectedPlayer, theNextPLayer);
        }

        [Fact]
        public void NextPlayer_SetupPlayers_LastPlayerLastStrikes() // Invalid by NextPlayer refactoring to "furthest away"
        {
            //Arrange
            string expectedPlayer = "Harry";
            Indexer.ResetPlayersId();
            Golf myGolfPlay = new Golf();

            Player myPlayer = new Player(Indexer.NextPlayerId(), "Harry");
            Strike myStrike = new Strike(Indexer.NextStrikeId(), 230);
            myPlayer.AddStrike(myStrike);
            myStrike = new Strike(Indexer.NextStrikeId(), 201);
            myPlayer.AddStrike(myStrike);
            myGolfPlay.AddPlayer(myPlayer);

            myPlayer = new Player(Indexer.NextPlayerId(), "Barry");
            myStrike = new Strike(Indexer.NextStrikeId(), 130);
            myPlayer.AddStrike(myStrike);
            myStrike = new Strike(Indexer.NextStrikeId(), 80);
            myPlayer.AddStrike(myStrike);
            myGolfPlay.AddPlayer(myPlayer);

            myPlayer = new Player(Indexer.NextPlayerId(), "Merry");
            myStrike = new Strike(Indexer.NextStrikeId(), 230);
            myPlayer.AddStrike(myStrike);
            myStrike = new Strike(Indexer.NextStrikeId(), 145);
            myPlayer.AddStrike(myStrike);
            myGolfPlay.AddPlayer(myPlayer);

            myPlayer = new Player(Indexer.NextPlayerId(), "Cherry");
            myStrike = new Strike(Indexer.NextStrikeId(), 218);
            myPlayer.AddStrike(myStrike);
            myGolfPlay.AddPlayer(myPlayer);


            //Act
            string theNextPLayer = myGolfPlay.NextPlayer().Name;

            //Assert
            Assert.Equal(expectedPlayer, theNextPLayer);
        }
    }
}
