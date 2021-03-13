/*********************************************************
*   ____  __.                  __                        *
*  |    |/ _|___________      |__|____    ____           *
*  |      < \_  __ \__  \     |  \__  \  /    \          *
*  |    |  \ |  | \// __ \_   |  |/ __ \|   |  \         *
*  |____|__ \|__|  (____  /\__|  (____  /___|  /         *
*          \/           \/\______|    \/     \/          *
*  _________    _  _    _________            .___        *
*  \_   ___ \__| || |__ \_   ___ \  ____   __| _/____    *
*  /    \  \/\   __   / /    \  \/ /  _ \ / __ |/ __ \   *
*  \     \____|  ||  |  \     \___(  <_> ) /_/ \  ___/   *
*   \______  /_  ~~  _\  \______  /\____/\____ |\___  >  *
*          \/  |_||_|           \/            \/    \/   *
*                                                        *
*********** 2021-03-12 ** Richard Krejstrup **************/


using System;

namespace ConsoleGolfer
{

    public class Program
    {

        static void Main(string[] args)
        {
            bool gameEnded = false;
            int gameRounds = 0;
            Player myActivePlayer;
            Golf myGolf;
            Course myCourse;
            Random aRandomObject = new Random();
            Console.WriteLine("Pew Pew - it's a game!\n");
            bool morePlayers = false;
            bool stillPlayersPlaying = true;
            int myPlayerSwingInputAngle;
            int myPlayerSwingInputSpeed;
            int myPlayerSwingResult;
            char CharacterIn;
            int nrOfPlayersInput;

            // ====== Here is where the game begins ===========================
            while (!gameEnded)
            {
                nrOfPlayersInput = 0;
                myGolf = new Golf();
                // ==== This is start of Game! = Comming next: Add players =====
                do
                {
                    Console.Clear();
                    morePlayers = false;
                    Console.WriteLine("*======== Set up gamers ======================");
                    Console.Write("* What's the name of new player {0}? : ", ++nrOfPlayersInput);
                    string inputName = Console.ReadLine();

                    // Create new player
                    myActivePlayer = new Player(Indexer.NextPlayerId(), inputName);
                    myGolf.AddPlayer(myActivePlayer);

                    do
                    {
                        Console.Write("\nMore Players? [y/n] :");
                        CharacterIn = Console.ReadKey().KeyChar;
                    } while ((CharacterIn != 'y') && (CharacterIn != 'n'));

                    if (CharacterIn == 'y') morePlayers = true;

                } while (morePlayers);
                Console.Clear();

                // ============ Create a new Course (later you can add more courses) ===
                int myRandomCourseRange = aRandomObject.Next(100, 701);
                myCourse = new Course(Indexer.NextCourseId(), myRandomCourseRange);
                myGolf.AddCourse(myCourse);
                foreach (Player thePlayers in myGolf.GetPlayers())
                {
                    thePlayers.LengthToGo = myRandomCourseRange;
                }



                stillPlayersPlaying = true;

                //=== To Come: the game rounds start here, until all are finished ========
                do
                {
                    SimpleMenu(++gameRounds);

                    myActivePlayer = myGolf.NextPlayer();
                    Console.WriteLine($"* You are up next: Player {myActivePlayer.Id}: {myActivePlayer.Name}." +
                                        $"\n* To cup:{myActivePlayer.LengthToGo} (of total {myCourse.TotalLength}m)");

                    myPlayerSwingInputAngle = GetNumber($"* Ok, {myActivePlayer.Name} what Angle? [deg elevation] : ");
                    myPlayerSwingInputSpeed = GetNumber($"* Ok, {myActivePlayer.Name} what Speed? [m/s]: ");
                    myPlayerSwingResult = myGolf.MakeStrike(myActivePlayer.Id, myPlayerSwingInputAngle, myPlayerSwingInputSpeed);

                    Console.WriteLine("* Oh god it was {0} meters!", myPlayerSwingResult);


                    //==== Next to come: Are we going to remove player?? ====================
                    if (myActivePlayer.LengthToGo <= -100)
                    {
                        Console.WriteLine("Well - actually {0}, that swing made you of the competiton.", myActivePlayer.Name);
                        myActivePlayer.Done = true;
                    }
                    else if (myActivePlayer.Getstrikes().Length > myCourse.Par + 5)
                    {
                        Console.WriteLine("Well we don't want to see you swing all day. Time for you to finish.");
                        myActivePlayer.Done = true;
                    }
                    if (myActivePlayer.Done)
                    {
                        if (myActivePlayer.LengthToGo == 0) Console.WriteLine("\n\n=== Winner, winner, chicken dinner! ===");
                        EndSceenPlayer(myActivePlayer);
                        //myGolf.RemovePlayer(myActivePlayer);
                    }

                    // === Are there still players left in game?? All Done??

                    stillPlayersPlaying = !myGolf.AreAllDone();


                    if (stillPlayersPlaying)
                    {
                        Console.WriteLine("\n Hit return for a next round.");
                        Console.ReadLine(); // wait for return/enter
                        Console.Clear();
                    }


                } while (stillPlayersPlaying);

                Console.WriteLine("Now we don't have any more competitors... Bye!");
                // Do the Aftermath!
                AfterMath(myGolf);

                // Play again?
                // y: Reset all values for new game...
                // n: exit app

                do
                {
                    Console.Write("\nWanna Play again?? [y/n] :");
                    CharacterIn = Console.ReadKey().KeyChar;
                } while ((CharacterIn != 'y') && (CharacterIn != 'n'));

                if (CharacterIn == 'y')
                {
                    gameEnded = false;
                }
                else
                {
                    gameEnded = true;
                }

            }

        }// ==== Here are the end of game. To Follow: Methods of Program ============


        /// <summary>
        /// GetNumber reads a user input value and converts and returns it as a int value. Exception is handeled by TryParse.
        /// <returns>GetNumber returns a integer from user input.</returns>
        /// </summary>
        public static int GetNumber(string inputWriteString = "", int wantedCharacters = 80)
        {
            int mySlask;
            bool runAgain = true;

            Console.Write(inputWriteString);
            do
            {
                bool slaskTratt = (int.TryParse(Console.ReadLine(), out mySlask));

                if (slaskTratt) runAgain = false;
                if (!slaskTratt) Console.Write("\n No, Number! Try again : ");

            } while (runAgain);
            return mySlask;
        }


        /// <summary>
        /// User input will take an input and check is it should count as an exact word,
        /// ortherwise just take the first letteras input character.
        /// </summary>
        /// <param name="textMessage">A text message to the player.</param>
        /// <param name="wordLetterExact">The number letters for the exact word.</param>
        /// <returns>A sting containing a matching length of the word, or just a character.</returns>
        static string UserInput(string textMessage, int wordLetterExact = 0)
        {
            // just get a string or valid character from user

            while (true)
            {
                Console.Write(textMessage);
                try
                {
                    string userInput;
                    do
                    {
                        userInput = Console.ReadLine();
                    } while (userInput.Length < 0);

                    if (userInput.Length != wordLetterExact)
                    {
                        char userInputChar = userInput[0];
                        return userInputChar.ToString();
                    }
                    else
                    {
                        return userInput;
                    }
                }
                catch (Exception theException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Exception: UserInputChar()");
                    Console.WriteLine(theException.ToString());
                    Console.ResetColor();
                    return null;
                }
            }

        }

        public static void EndSceenPlayer(Player myActivePlayer)
        {
            Console.WriteLine("OMG!! {0}, you finished the game!", myActivePlayer.Name);
            Console.WriteLine("Statistics: ");
            int myLoop = 1;
            foreach (Strike playerStrikes in myActivePlayer.Getstrikes())
            {
                Console.Write("Swing nr {0} traveled {1} meters.\n", myLoop++, playerStrikes.Distance);
            }
            Console.WriteLine("GG my human!!  Stay and watch the other gamers finish up now.");
            Console.ReadKey();
        }

        public static void SimpleMenu(int inputRounds)
        {
            Console.WriteLine("*===================================*");
            Console.WriteLine("* The Console Golf Game             *");
            Console.WriteLine("* Round: {0}                          *", inputRounds);
        }


        public static void AfterMath(Golf theGame)
        {
            Player[] thePlayers = theGame.GetPlayers();
            int Itarable = 0;
            Console.Clear();
            Console.WriteLine("*===============================================*");
            Console.WriteLine("* The Console Golf: Game Final Statistics       *");
            Console.WriteLine("* the Courts length: {0}  Par: {1}              *", theGame.GetCourses()[0].TotalLength, theGame.GetCourses()[0].Par);
            foreach (Player item in thePlayers)
            {
                Console.WriteLine("* Player {0}: ", item.Name); // Boogie, Birdie, Par...??
                foreach (Strike list in item.Getstrikes())
                {

                    Console.WriteLine("* {0}: {1} meter", ++Itarable, list.Distance);
                }
                Itarable = 0;
            }
            Console.WriteLine("*************************************************");


        }

        // End of Methods
    } // End of Class
} // End of Namespace

