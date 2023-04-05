using DesignCar.AppCode;

namespace DesignCars.AppCode
{
    internal class GetSetValue
    {   
        protected string Get(string value)
        {
            if (value == null) return "Not Found";
            else if (!Designs.IsValidString(value, out string msg)) return "Invalid Data";
            else return value;
        }
        protected string Get(int value)
        {
            if (value == 0) return "Not Found";
            else if (value == -1) return "Invalid Data";
            else return value.ToString();
        }
        protected string Get(bool? value)
        {
            if (value == null) return "Invalid Data";
            else return (value == true) ? "Yes" : "No";
        }
        protected string Get(char value)
        {
            if (value == '!') return "Not Found";
            else if (value == '*') return "Invalid Data";
            else return value.ToString();
        }
        
        protected int SetInt(string value)
        {
            if (value == null) return 0; //Not Found
            else if (int.TryParse(value, out int x) && x > 0) return x;
            else return -1; //Invalid Data  
        }
        protected bool? SetBool(string value)
        {
            if (bool.TryParse(value, out bool x)) return x; 
            else return null; //Invalid Data
        }
        protected char SetChar(string value)
        {
            if (value == null) return '!';  //Not Found
            else if (char.TryParse(value, out char ch) && char.IsLetter(ch)) return ch;
            else return '*'; //Invalid Data
        }
        
        protected GetSetValue() { }
    }
}
