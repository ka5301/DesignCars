using DesignCars.AppCode;

namespace DesignCar.AppCode
{
    internal class Wheel : GetSetValue
    {
        private static int allotId = 1;
        private string _vType;
        private int _width;
        private int _profile;
        private int _diameter;
        private char _construction;
        private char _speedRating;

        internal readonly int Id;
        internal string VType { 
            get 
            { 
                return Get(_vType);
            } 
            private set 
            { 
                _vType = value; 
            } 
        }
        internal string Width {
            get
            {
                return Get(_width);
            } 
            private set
            {
                _width = SetInt(value);
            } 
        }
        internal string Profile
        {
            get
            {
                return Get(_profile);
            }
            private set
            {
                _profile = SetInt(value);
            }
        }
        internal string Diameter
        {
            get
            {
                return Get(_diameter);
            }
            private set
            {
                _diameter = SetInt(value);
            }
        }
        internal string Construction {
            get
            {
                return Get(_construction);
            } 
            private set
            {
                _construction = SetChar(value);
            } 
        }
        internal string SpeedRating {
            get
            {
                return Get(_speedRating);
            } 
            private set
            {
                _speedRating = SetChar(value);
            } 
        }

        internal Wheel() {
            Id = allotId++;
        }

        internal Wheel(string vType, string width, string profile, string diameter, string construction, string speedRating) : this()
        {
            VType = vType;
            Width = width;
            Profile = profile;
            Diameter = diameter;
            Construction = construction;
            SpeedRating = speedRating;
        }

        internal string GetWheelInfo(bool withId = false)
        {
            string info = $"{Width}/{Profile}/{Construction}{Diameter} {SpeedRating}";
            return (withId)? $"\n\tId:{Id} - " + info:info;
        }
    }
}
