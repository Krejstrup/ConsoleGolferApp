using Xunit;

namespace ConsoleGolfer.Tests.Data
{
    public class GolfTests
    {

        // =======================NEXTPLAYER==================(1/2)
        [Fact]
        public void NextPlayer_CreatePlayers_FurthestAwayWillPlay()
        {
            //Arrange
            Indexer.ResetPlayersId();
            string sorePlayer;
            string expectedPlayerName = "Barry";
            Golf myGolf = new Golf();
            Player myPlayer = new Player(Indexer.NextPlayerId(), "Harry");
            myPlayer.LengthToGo = 100;
            myGolf.AddPlayer(myPlayer);
            myPlayer = new Player(Indexer.NextPlayerId(), "Barry");
            myPlayer.LengthToGo = 200;
            myGolf.AddPlayer(myPlayer);
            myPlayer = new Player(Indexer.NextPlayerId(), "Jerry");
            myPlayer.LengthToGo = 50;
            myGolf.AddPlayer(myPlayer);

            //Act
            sorePlayer = myGolf.NextPlayer().Name;

            //Assert
            Assert.Equal(expectedPlayerName, sorePlayer);
        }


        // =======================NEXTPLAYER==================(2/2)
        [Fact]
        public void NextPlayer_CreatePlayers_FurthestAwayNotDoneWillPlay()
        {
            //Arrange
            Indexer.ResetPlayersId();
            string sorePlayer;
            string expectedPlayerName = "Harry";
            Golf myGolf = new Golf();
            Player myPlayer = new Player(Indexer.NextPlayerId(), "Harry");
            myPlayer.LengthToGo = 100;
            myGolf.AddPlayer(myPlayer);
            myPlayer = new Player(Indexer.NextPlayerId(), "Barry");
            myPlayer.Done = true;
            myPlayer.LengthToGo = 200;
            myGolf.AddPlayer(myPlayer);
            myPlayer = new Player(Indexer.NextPlayerId(), "Jerry");
            myPlayer.LengthToGo = 50;
            myGolf.AddPlayer(myPlayer);

            //Act
            sorePlayer = myGolf.NextPlayer().Name;

            //Assert
            Assert.Equal(expectedPlayerName, sorePlayer);
        }
        // =======================NEXTPLAYER==================(3/3)
        [Fact]
        public void NextPlayer_CreateOnePlayer_OnlyPlayerThereWillPlay()
        {
            //Arrange
            Indexer.ResetPlayersId();
            string sorePlayer;
            string expectedPlayerName = "Harry";
            Golf myGolf = new Golf();
            Player myPlayer = new Player(Indexer.NextPlayerId(), "Harry");
            myPlayer.LengthToGo = 100;
            myGolf.AddPlayer(myPlayer);


            //Act
            sorePlayer = myGolf.NextPlayer().Name;

            //Assert
            Assert.Matches(expectedPlayerName, sorePlayer);
        }

        //============== AddCourse =================================
        [Fact]
        public void AddCourse_CreateOneInput_OnlyOneThere()
        {
            //Arrange
            Course localCourse = new Course(0, 200);
            Golf myGolf = new Golf();
            myGolf.AddCourse(localCourse);
            int expectedNoCourses = 1;
            int actualNoCourses;

            //Act
            actualNoCourses = myGolf.GetCourses().Length;

            //Assert
            Assert.Equal(expectedNoCourses, actualNoCourses);
        }


    }
}
