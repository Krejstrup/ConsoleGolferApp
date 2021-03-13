namespace ConsoleGolfer
{
    public class Strike
    {
        private int _id;
        private int _distance;
        private int _angle;

        public Strike(int inputId, int inputDistance)
        {
            _id = inputId;
            _distance = inputDistance;
        }

        public int Distance
        {
            get
            {
                return _distance;
            }
        }

        public int Angle
        {
            get
            {
                return _angle;
            }
        }
        public int Id
        {
            get { return _id; }
        }

    }
}
