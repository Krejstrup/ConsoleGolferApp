using System;

namespace ConsoleGolfer
{
    public class Player
    {
        private int _Id;
        private string _name;
        private bool _done;
        private Strike[] _strikes;
        private int _lengthToGO;


        public Player(int inputId = 0, string inputName = "John")
        {
            _Id = inputId;

            if (inputName != null)
            {
                _name = inputName;
            }
            else
            {
                _name = "";
            }
            _strikes = new Strike[0];
        }

        public bool Done
        {
            get { return _done; }
            set { _done = value; }
        }


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                {
                    _name = value;
                }
            }
        }

        public int LengthToGo
        {
            get
            {
                return _lengthToGO;
            }
            set
            {
                _lengthToGO = value;
                if (_lengthToGO == 0) _done = true;
            }
        }

        public int Id
        {
            get { return _Id; }
        }

        public bool AddStrike(Strike inputStrike)
        {

            if (inputStrike != null)
            {
                int totalNumberOfStrikes = _strikes.Length;

                Array.Resize(ref _strikes, totalNumberOfStrikes + 1);
                _strikes[totalNumberOfStrikes] = inputStrike;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Strike[] Getstrikes()
        {
            return _strikes;
        }
    }
}
