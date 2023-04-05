using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace DesignCar.AppCode
{
    internal partial class Car : Vehicle
    {
        private string _name;
        private string _type;
        private string _color;
        private bool? _sunroof;
        private bool? _backCamera;
        private bool? _iPads;
        private bool? _refrigerator;

        internal string Name
        {
            get
            {
                return Get(_name);
            }
            private set
            {
                _name = value;
            }
        }
        internal string Type
        {
            get
            {
                return Get(_type);
            }
            private set
            {
                _type = value;
            }
        }
        internal Engine EngineInfo { get; private set; }
        internal Wheel WheelsInfo { get; private set; }
        internal string Color
        {
            get
            {
                return Get(_color);
            }
            private set
            {
                _color = value;
            }
        }
        internal string Sunroof
        {
            get
            {
                return Get(_sunroof);
            }
            private set
            {
                _sunroof = SetBool(value);
            }
        }
        internal string BackCamera
        {
            get
            {
                return Get(_backCamera);
            }
            private set
            {
                _backCamera = SetBool(value);
            }
        }
        internal string IPads
        {
            get
            {
                return Get(_iPads);
            }
            private set
            {
                _iPads = SetBool(value);
            }
        }
        internal string Refrigerator
        {
            get
            {
                return Get(_refrigerator);
            }
            private set
            {
                _refrigerator = SetBool(value);
            }
        }

        internal Car() : base()
        {
            
        }

        internal Car(string name, string type, string color, string sunroof, string backCamera, string iPads, string refrigerator, Engine engineInfo, Wheel wheelsInfo) : base()
        {
            IPads = iPads;
            Refrigerator = refrigerator;
            Color = color;
            Sunroof = sunroof;
            BackCamera = backCamera;
            Name = name;
            Type = type;
            EngineInfo = engineInfo;
            WheelsInfo = wheelsInfo;
            SetVehicleInfo(type);
        }

        private static void AppendSpace(ref StringBuilder msg, int n)
        {
            while (--n > 0) msg.Append(" ");
        }
        private static void SetDesignInputPage(ref StringBuilder carInfo, string text)
        {
            carInfo.Append(text);
            App.PrintAppName("");
            App.Print(carInfo.ToString() + "\n");
        }
        private void InputExterior(ref StringBuilder carInfo)
        {
            Color = Designs.ChooseColor();
            SetDesignInputPage(ref carInfo, $"\n\tColor           : {Color}");

            Sunroof = Designs.Want("Sunroof").ToString();
            SetDesignInputPage(ref carInfo, $"\n\tSun Roof        : {Sunroof}");

            BackCamera = Designs.Want("BackCamera").ToString();
            SetDesignInputPage(ref carInfo, $"\n\tBack Camera     : {BackCamera}");
        }
        private void InputInterior(ref StringBuilder carInfo)
        {
            IPads = Designs.Want("Ipads").ToString();
            SetDesignInputPage(ref carInfo, $"\n\tIpads           : {IPads}");

            Refrigerator = Designs.Want("Refrigerator").ToString();
            SetDesignInputPage(ref carInfo, $"\n\tRefrigerator    : {Refrigerator}");
        }
        
        internal static bool DoWantToEscape(string text)
        {
            if (text.ToLower() == "esc" && Designs.Want("to Abort the design")) return true;
            return false;
        }
        
        internal void InputDetails()
        {
            StringBuilder carInfo = new StringBuilder();
            SetDesignInputPage(ref carInfo ,GetVehicleInfo());

            InputCarName:;
            var name = App.Input("Enter the name of the car", 2);
            if(!Designs.IsValidString(name, out string msg))
            {
                App.ShowMessage("Warning", msg);
                goto InputCarName;
            }
            if(Designs.IsCarExists(name)) 
            {
                App.ShowMessage("Warning","You had already created a Design with this name, Please choose another name\n");
                goto InputCarName;
            }
            Name = name;
            SetDesignInputPage(ref carInfo, $"\n\n\tCar Name        : {Name}");

            this.InputExterior(ref carInfo);

            this.InputInterior(ref carInfo);

            Type = Designs.ChooseType();
            SetVehicleInfo(Type);

            SetDesignInputPage(ref carInfo, $"\n\tType            : {Type}\n\tWheel Base      : {WheelBase}\n\tTrack Width     : {Track}\n\tTurning Radius  : {TurningRadius}");

            EngineInfo = Designs.ChooseEngine(Type);
            SetDesignInputPage(ref carInfo, $"\n\tEngine Info     : {EngineInfo.GetEngineInfo()}");

            WheelsInfo = Designs.ChooseWheel(Type);
            SetDesignInputPage(ref carInfo, $"\n\tWheel Info      : {WheelsInfo.GetWheelInfo()}");


            Designs.SaveCar(this);

            App.ShowMessage("Info","\n\tDesign Has been created Successfully\n",true);
        }
        internal void PrintDetails(int vertical = 0)
        {
            StringBuilder msg = new StringBuilder();
            if(vertical == 1)
            {
                msg.Append($"\n\tId              : {Id}");
                msg.Append($"\n\tBrand           : {BrandName}");
                msg.Append($"\n\tModel           : {ModelYear}");
                msg.Append($"\n\tCar Name        : {Name}");
                msg.Append($"\n\tColor           : {Color}");
                msg.Append($"\n\tType            : {Type}");
                msg.Append($"\n\tSun Roof        : {Sunroof}");
                msg.Append($"\n\tBack Camera     : {BackCamera}");
                msg.Append($"\n\tIpads           : {IPads}");
                msg.Append($"\n\tRefrigerator    : {Refrigerator}");
                msg.Append($"\n\tEngine Info     : {EngineInfo.GetEngineInfo()}");
                msg.Append($"\n\tWheel Info      : {WheelsInfo.GetWheelInfo()}");
                msg.Append($"\n\tAirbags         : {Airbags}");
                msg.Append($"\n\tBreaking System : {BreakingSystem}");
                msg.Append($"\n\tWheel Base      : {WheelBase}");
                msg.Append($"\n\tTrack Width     : {Track}");
                msg.Append($"\n\tTurning Radius  : {TurningRadius}");
            }
            else
            {
                msg.Append($"\t{Id}");AppendSpace(ref msg,8 - Id.ToString().Length);
                msg.Append($"Name  : {Name}"); AppendSpace(ref msg, 17 - Name.Length);
                if (Sunroof == "Yes") { msg.Append("Sun Roof"); AppendSpace(ref msg, 11); } else { AppendSpace(ref msg, 19); }
                msg.Append($"{EngineInfo.Name}"); AppendSpace(ref msg, 18 - EngineInfo.Name.Length);
                var wInfo = WheelsInfo.GetWheelInfo();
                msg.Append($"{wInfo}"); AppendSpace(ref msg, 18 - wInfo.Length);
                msg.Append($"Wheel Base     : {WheelBase}");

                msg.Append($"\n\t"); AppendSpace(ref msg, 8);
                msg.Append($"Color : {Color}"); AppendSpace(ref msg, 17 - Color.Length);
                if (BackCamera == "Yes") { msg.Append("Back Camera"); AppendSpace(ref msg, 8); } else { AppendSpace(ref msg, 19); }
                msg.Append($"{EngineInfo.Cylinders}(Cylinders)"); AppendSpace(ref msg, 6);
                AppendSpace(ref msg, 18);
                msg.Append($"Track Width    : {Track}");

                msg.Append($"\n\t"); AppendSpace(ref msg, 8);
                msg.Append($"Type  : {Type}"); AppendSpace(ref msg, 17 - Type.Length);
                if (IPads == "Yes") { msg.Append("Ipads"); AppendSpace(ref msg, 14); } else { AppendSpace(ref msg, 19); }
                msg.Append($"{EngineInfo.Displacement}/{EngineInfo.Torque}"); AppendSpace(ref msg, 6);
                AppendSpace(ref msg, 18);
                msg.Append($"Turning Radius : {TurningRadius}");

                msg.Append($"\n\t"); AppendSpace(ref msg, 32);
                if (Refrigerator == "Yes") { msg.Append("Refrigerator"); AppendSpace(ref msg, 7); } else { AppendSpace(ref msg, 19); }
                msg.Append($"{EngineInfo.DriveTrain}"); AppendSpace(ref msg, 15);
                AppendSpace(ref msg, 18);
            }
            App.Print(msg.ToString());
        }
        internal string GetInfo()
        {
            string info = $"Id : {Id}\tName : {Name}";
            return info;
        }
    }
}
