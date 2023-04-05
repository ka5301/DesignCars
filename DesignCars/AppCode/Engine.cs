using DesignCars.AppCode;

namespace DesignCar.AppCode
{
    internal class Engine : GetSetValue
    {
        private static int allotId = 1;
        
        private string _vType;
        private string _name;
        private int _cylinders;
        private string _displacement;
        private string _torque;
        private string _driveTrain;

        internal readonly int Id;
        internal string VType
        {
            get 
            {
                return Get(_vType);
            }
            private set 
            { 
                _vType = value; 
            }
        }
        internal string Name {
            get
            {
                return Get(_name);
            } 
            private set
            {
                _name = value;
            } 
        }
        internal string Cylinders 
        {
            get
            {
                return Get(_cylinders);
            } 
            private set
            {
                _cylinders = SetInt(value);
            } 
        }
        internal string Displacement {
            get
            {
                return Get(_displacement);
            }
            private set
            {
                _displacement = value;
            }
        }
        internal string Torque
        {
            get
            {
                return Get(_torque);
            }
            private set
            {
                _torque = value;
            }
        }
        internal string DriveTrain
        {
            get
            {
                return Get(_driveTrain);
            }
            private set
            {
                _driveTrain = value;
            }
        }

        internal Engine() {
            Id = allotId++;
        }
        internal Engine(string vType, string name, string cylinders, string displacement, string torque, string driveTrain) : this()
        {
            VType = vType;
            Name = name;
            Cylinders = cylinders;
            Displacement = displacement;
            Torque = torque;
            DriveTrain = driveTrain;
        }
        internal string GetEngineInfo(bool withId = false)
        {
            string info = $"{Name}/{Cylinders}(Cylinders)/{Displacement}/{Torque}/{DriveTrain}";
            return (withId)? $"\n\tId:{Id} - " + info:info;
        }

    }
}
