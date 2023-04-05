using DesignCars.AppCode;
using System;
using System.Text;

namespace DesignCar.AppCode
{
    internal class Vehicle : GetSetValue
    {
        private static int allotId = 1;
        internal int Id { get; private set; }
        protected const string BrandName = "Rolls Royce";
        protected static readonly string ModelYear = DateTime.Now.Year.ToString();
        protected string WheelBase;
        protected string Track;
        protected string TurningRadius;
        protected const string Airbags = "8";
        protected const string BreakingSystem = "ABS";

        protected Vehicle() {
            Id = allotId++;
        }
        
        protected void SetVehicleInfo(string vType)
        {
            if (vType == "Sedan")
            {
                WheelBase = "4970mm";
                Track = "2430mm";
                TurningRadius = "10.4m";
            }

            else if (vType == "SUV")
            {
                WheelBase = "5320mm";
                Track = "2830mm";
                TurningRadius = "10.7m";
            }

            else
            {
                WheelBase = "4370mm";
                Track = "2430mm";
                TurningRadius = "10.4m";
            }
        }
        protected string GetVehicleInfo()
        {
            StringBuilder msg = new StringBuilder();

            msg.Append($"\n\n\tBrand : {BrandName}\t\tModel : {ModelYear}");
            msg.Append($"\n\tAirbags : {Airbags}\t\tBreaking System : {BreakingSystem}");

            return msg.ToString();
        }
    }
}
