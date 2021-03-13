namespace ConsoleGolfer
{
    public class Indexer
    {

        private static int _playerId;
        private static int _strikeId;
        private static int _courseId;

        public static int NextPlayerId() { return ++_playerId; }
        public static int NextStrikeId() { return ++_strikeId; }
        public static int NextCourseId() { return ++_courseId; }

        public static void ResetPlayersId()
        {
            _playerId = 0;
        }
        public static void ResetStrikeId()
        {
            _strikeId = 0;
        }
        public static void ResetCourseId()
        {
            _courseId = 0;
        }

    }
}
