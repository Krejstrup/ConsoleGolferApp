namespace ConsoleGolfer
{
    public class Course
    {
        private int _length;
        private int _id;
        private int _par;


        public Course(int inputId = 0, int inputLenght = 100)
        {
            _id = inputId;
            _length = inputLenght;
            _par = 3;
            if (inputLenght > 200)
            {
                _par = 4;
            }
            if (inputLenght > 400)
            {
                _par = 5;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public int TotalLength
        {
            get
            {
                return _length;
            }
        }

        public int Par { get { return _par; } }



    }
}
