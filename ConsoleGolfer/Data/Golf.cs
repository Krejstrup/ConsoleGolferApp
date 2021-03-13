using System;



namespace ConsoleGolfer
{
    public class Golf
    {

        private Player[] _players;
        private Course[] _courses;
        private readonly double _gravity = 9.82;

        public Golf()
        {
            _players = new Player[0];
            _courses = new Course[0];
        }

        public bool AddPlayer(Player inputPlayer)
        {

            if (inputPlayer != null)
            {
                int totalNrofPlayers = _players.Length;
                Array.Resize(ref _players, totalNrofPlayers + 1);
                _players[totalNrofPlayers] = inputPlayer;

                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AddCourse(Course inputCourse)
        {
            if (inputCourse != null)
            {
                int totalNrOfCourses = _courses.Length;
                Array.Resize(ref _courses, totalNrOfCourses + 1);
                _courses[totalNrOfCourses] = inputCourse;

                return true;
            }
            else
            {
                return false;
            }
        }

        public Course[] GetCourses()
        {
            return _courses;
        }

        public int MakeStrike(int inputPlayerId, int inputAngle, int inputVecity)
        {
            double myAngleRadians = (Math.PI / 180) * Convert.ToDouble(inputAngle);
            double myDistance = Math.Pow(inputVecity, 2) / _gravity * Math.Sin(2 * myAngleRadians);
            int myIntDistance = Convert.ToInt32(myDistance);

            Strike myNewStrike = new Strike(Indexer.NextStrikeId(), myIntDistance);
            Player theActivePlayer = FindPlayerById(inputPlayerId);
            theActivePlayer.AddStrike(myNewStrike);
            theActivePlayer.LengthToGo = Math.Abs(theActivePlayer.LengthToGo) - myIntDistance;


            // BEWARE => if length to go now is Zero the player is _done = true.

            return myIntDistance;
        }

        public Player[] GetPlayers()
        {
            return _players;
        }

        /// <summary>
        /// Pick the next still playing golfer from the Array. The player with
        /// the farthest way to cup will play. If done skip!
        /// </summary>
        /// <returns>The next player i que for a swing.</returns>
        public Player NextPlayer()
        {

            try
            {
                Player[] collectionOfPlayers = GetPlayers();
                int numberOfPlayers = collectionOfPlayers.Length;
                int[] newIntArray = new int[numberOfPlayers];

                // Fast check if there are more than one player
                if (numberOfPlayers > 1)
                {
                    int longestMeters = 0;   // ?? 
                    int metersLeft;

                    // We have to find the farthest distance away from the cup/flag
                    for (int myLoop = 0; myLoop < numberOfPlayers; myLoop++)
                    {
                        // Skip everyone that are finished
                        if (!collectionOfPlayers[myLoop].Done)
                        {
                            metersLeft = Math.Abs(collectionOfPlayers[myLoop].LengthToGo);
                            longestMeters = (longestMeters > metersLeft) ? longestMeters : metersLeft;
                        }
                    }

                    // Now find the player who had the farthest distance
                    for (int myLoop = 0; myLoop < numberOfPlayers; myLoop++)
                    {
                        if (Math.Abs(collectionOfPlayers[myLoop].LengthToGo) == longestMeters)
                        {
                            return collectionOfPlayers[myLoop];
                        }
                    }
                }
                else if (numberOfPlayers == 1)
                {
                    // if only one person playing - easy:
                    return collectionOfPlayers[0];

                }
            }
            catch (Exception e)
            {
                Console.Write("Exception! " + e.ToString());
            }


            return null;    // something went terrible wrong if Golf is null!
        }

        public Player FindPlayerById(int inputId)
        {
            int myTotalNumberOfPlayers = _players.Length;

            for (int myLoop = 0; myLoop < myTotalNumberOfPlayers; myLoop++)
            {
                if (_players[myLoop].Id == inputId) return _players[myLoop];
            }

            return null;
        }

        public bool RemovePlayer(Player myPlayerToRemove)
        {
            int myIndexOfPlayerToRemove = 0;
            int myTotalPlayers = _players.Length;

            if (myPlayerToRemove != null)
            {
                if (myTotalPlayers > 1)
                {

                    for (int myLoop = 0; myLoop < myTotalPlayers; myLoop++)
                    {
                        if (_players[myLoop].Id == myPlayerToRemove.Id)
                        {
                            myIndexOfPlayerToRemove = myLoop;
                        }
                    }

                    for (int myLoop = myIndexOfPlayerToRemove; myLoop < myTotalPlayers - 1; myLoop++)
                    {
                        _players[myLoop] = _players[myLoop + 1];
                    }
                    Array.Resize(ref _players, myTotalPlayers - 1);
                }
                else
                {
                    _players = new Player[0];
                }
                return true;
            }

            return false;
        }

        public bool AreAllDone()
        {
            //_players  [0].Done [1].Done [2].Done

            int playerNr = _players.Length;
            int PlayersDone = 0;

            for (int myLoop = 0; myLoop < playerNr; myLoop++)
            {
                PlayersDone += _players[myLoop].Done ? 1 : 0;
            }
            if (PlayersDone == playerNr)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
