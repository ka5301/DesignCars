using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.Configuration;

namespace DesignCar.AppCode
{
    internal static class Designs
    {
        private static readonly Dictionary<string, List<string>> _options = new Dictionary<string, List<string>>();
        private static readonly List<Engine> _engines = new List<Engine>();
        private static readonly List<Wheel> _wheels = new List<Wheel>();
        private static readonly List<Car> _cars = new List<Car>();
        private const int trashSpace = 2;
        private static readonly Stack<Car> _carsInTrash = new Stack<Car>(trashSpace);

        private static bool HasEngineAndWheelsForType(string type)
        {

            return _engines.Any(e => e.VType == type) && _wheels.Any(w => w.VType == type);
        }
        private static bool IsValidColor(string color, out string msg)
        {
            if (color.Length < 3 || color.Length > 12)
            {
                msg = "Color Name should be of length 3 to 12.";
                return false;
            }

            var nameChar = color.ToCharArray();
            if (nameChar.Any(c => !char.IsLetter(c)))
            {
                msg = "Color Name can only have alphabets";
                return false;
            }

            msg = "";
            return true;
        }
        private static string ConvertToString(dynamic str)
        {
            if (str == null) { return null; }
            else { return str.ToString(); }
        }
        private static void BindEngineDesigns(Excel.Range engines)
        {
            int rows = engines.Rows.Count;
            for (int i = 2; i <= rows; i++)
            {
                try
                {
                    var VType = ConvertToString(engines.Cells[i, 2].value());
                    var Name = ConvertToString(engines.Cells[i, 3].value());
                    var Cylinders = ConvertToString(engines.Cells[i, 4].value());
                    var Displacement = ConvertToString(engines.Cells[i, 5].value());
                    var Torque = ConvertToString(engines.Cells[i, 6].value());
                    var DriveTrain = ConvertToString(engines.Cells[i, 7].value());

                    SaveEngine(new Engine(VType, Name, Cylinders, Displacement, Torque, DriveTrain));
                }
                catch (Exception e)
                {
                    Console.Write($"\nError : {e.Message}\n");
                }
            }
        }
        private static void BindWheelDesigns(Excel.Range wheels)
        {
            int rows = wheels.Rows.Count;
            for (int i = 2; i <= rows; i++)
            {
                try
                {
                    var VType = ConvertToString(wheels.Cells[i, 2].value());
                    var width = ConvertToString(wheels.Cells[i, 3].value());
                    var profile = ConvertToString(wheels.Cells[i, 4].value());
                    var diameter = ConvertToString(wheels.Cells[i, 5].value());
                    var construction = ConvertToString(wheels.Cells[i, 6].value());
                    var speedrating = ConvertToString(wheels.Cells[i, 7].value());

                    SaveWheels(new Wheel(VType, width, profile, diameter, construction, speedrating));
                }
                catch (Exception e)
                {
                    Console.Write($"\nError : {e.Message}\n");
                }
            }
        }
        private static void BindCarDesigns(Excel.Range cars)
        {
            int rows = cars.Rows.Count;
            for (int i = 2; i <= rows; i++)
            {
                try
                {

                    var Name = ConvertToString(cars.Cells[i, 2].value());
                    var Type = ConvertToString(cars.Cells[i, 3].value());
                    var Color = ConvertToString(cars.Cells[i, 4].value());
                    var Sunroof = ConvertToString(cars.Cells[i, 5].value());
                    var Backcamera = ConvertToString(cars.Cells[i, 6].value());
                    var Ipads = ConvertToString(cars.Cells[i, 7].value());
                    var Refrigerator = ConvertToString(cars.Cells[i, 8].value());
                    var engineInfo = _engines.FirstOrDefault(e => e.VType == Type);
                    var wheelInfo = _wheels.FirstOrDefault(w => w.VType == Type);

                    SaveCar(new Car(Name, Type, Color, Sunroof, Backcamera, Ipads, Refrigerator, engineInfo, wheelInfo));
                }
                catch (Exception e)
                {
                    Console.Write($"\nError : {e.Message}\n");
                }
            }
        }
        private static void LoadDesignsUsingMSOInterop()
        {
            string directory = ConfigurationManager.AppSettings["path"].ToString();
            var path = directory + "Designs.xlsx";

            Application excelApp = new Application();
            Workbook excelWB = null;
            _Worksheet excelWS = null;
            Range excelRange = null;

            try
            {
                excelWB = excelApp.Workbooks.Open(path);

                excelWS = excelWB.Sheets[1];
                excelRange = excelWS.UsedRange;
                BindEngineDesigns(excelRange);

                Marshal.ReleaseComObject(excelWS);
                Marshal.ReleaseComObject(excelRange);
                excelWS = excelWB.Sheets[2];
                excelRange = excelWS.UsedRange;
                BindWheelDesigns(excelRange);

                Marshal.ReleaseComObject(excelWS);
                Marshal.ReleaseComObject(excelRange);
                excelWS = excelWB.Sheets[3];
                excelRange = excelWS.UsedRange;
                BindCarDesigns(excelRange);

            }
            catch (Exception e)
            {
                Console.Write($"\n\tError : {e.Message}");
                Console.ReadKey();
            }
            finally
            {
                Marshal.ReleaseComObject(excelWS);
                Marshal.ReleaseComObject(excelRange);
                excelWB.Close();
                Marshal.ReleaseComObject(excelWB);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
            }
        }
        private static void BindDataDefault()
        {
            SaveEngine(new Engine("SUV", "Diesel", "4", "2800cc", "200Nm", "AWD"));
            SaveEngine(new Engine("SUV", "Gasoline", "5", "3200cc", "300Nm", "AWD"));
            SaveEngine(new Engine("SUV", "Gas-Turbine", "6", "3900cc", "350Nm", "AWD"));
            SaveEngine(new Engine("Sedan", "Diesel", "4", "2950cc", "190Nm", "RWD"));
            SaveEngine(new Engine("Sedan", "Gasoline", "4", "2200cc", "200Nm", "RWD"));
            SaveEngine(new Engine("Sedan", "Diesel", "4", "2500cc", "230Nm", "AWD"));

            SaveWheels(new Wheel("SUV", "205", "75", "16", "R", "H"));
            SaveWheels(new Wheel("SUV", "225", "65", "16", "R", "V"));
            SaveWheels(new Wheel("Sedan", "195", "75", "16", "R", "H"));
            SaveWheels(new Wheel("Sedan", "165", "65", "17", "R", "V"));

            SaveCar(new Car("Dawn", "Sedan", "Brown", "false", "true", "true", "false", _engines[3], _wheels[2]));
            SaveCar(new Car("Wraith", "Sedan", "Blue", "true", "true", "false", "false", _engines[5], _wheels[2]));
            SaveCar(new Car("Ghost", "SUV", "Wine", "true", "true", "true", "true", _engines[2], _wheels[1]));

        }

        internal static bool IsValidString(string str, out string msg)
        {
            string pattern = "^[A-Za-z0-9 ]+$";
            Regex regex = new Regex(pattern);
            if (str.Length < 3 || str.Length > 15)
            {
                msg = "Car Name should be of length 3 to 15.";
                return false;
            }

            if (!regex.IsMatch(str))
            {
                msg = "Name can't have any special character";
                return false;
            }
            msg = "";
            return true;
        }
        internal static bool HaveEnginesAndWheels()
        {
            return _options["types"].Any(type => (_engines.Any(e => e.VType == type) && _wheels.Any(w=> w.VType == type)));
        }
        internal static bool IsTrashEmpty()
        {
            return _carsInTrash.Count() == 0;
        }
        internal static bool IsCarExists(string Name)
        {
            return _cars.Any(car => car.Name == Name);
        }

        internal static void DisplayLoadedData()
        {
            App.PrintAppName("");
            
            Console.WriteLine("\n\n\tEngine Designs");
            foreach(var engine in _engines)
            {
                Console.Write($"\t{engine.VType,-15} {engine.Name,-15} {engine.Cylinders,-15} {engine.Displacement,-15} {engine.Torque,-15} {engine.DriveTrain,-15}\n");
            }

            Console.WriteLine("\n\n\tWheels Designs");
            foreach(var wheel in _wheels)
            {
                Console.Write($"\t{wheel.VType,-15} {wheel.Width,-15} {wheel.Profile,-15} {wheel.Diameter,-15} {wheel.Construction,-15} {wheel.SpeedRating,-15}\n");
            }

            Console.WriteLine("\n\n\tCar Designs");
            foreach (var car in _cars)
            {
                Console.Write($"\t{car.Name,-15} {car.Color,-15} {car.Type,-15} {car.Sunroof,-15} {car.BackCamera,-15} {car.IPads,-15} {car.Refrigerator,-15}\n");
            }

            Console.Write("\n\n\tPress any key to Go Back");
            Console.ReadKey();
        }
        internal static void BindData()
        {
            _options["types"] = new List<string> { "Sedan","SUV","Hatchback","Limousine","Roadster" };
            _options["Colors"] = new List<string> { "Black", "White", "Grey"};
            LoadDesignsUsingMSOInterop();
        }

        internal static void SaveCar(Car car)
        {
            _cars.Add(car);
        }
        internal static void SaveEngine(Engine engine)
        {
            _engines.Add(engine);
        }
        internal static void SaveWheels(Wheel wheels)
        {
            _wheels.Add(wheels);
        }
        
        
        internal static ReadOnlyCollection<Car> GetCars()
        {
            var cars = _cars;
            cars.Sort((a, b) => a.Id > b.Id ? 1 : -1);
            return cars.AsReadOnly();
        }
        internal static void DisplayCarDesigns()
        {
            var cars = _cars;

            if (cars.Count == 0)
            {
                App.ShowMessage("Warning","\n\tNo design is created yet\n",true);
                return;
            }

            cars.Sort((a,b)=> a.Id>b.Id?1:-1);

            var NoOfCarsOnPage = 2;

            var currentPage = 1;
            var totalPages = Math.Ceiling(cars.Count / (decimal) NoOfCarsOnPage);

            displayPage:;            
            var skipCars = (currentPage - 1) * NoOfCarsOnPage;
            var takeCars = Math.Min(NoOfCarsOnPage, cars.Count - skipCars);

            var displayCars = cars.Skip(skipCars).Take(takeCars);

            App.PrintAppName("Use Esc Key to go back and Left/Right Arrow keys to go to Previous/Next page");
            App.Print($"\n\n\t\t\t\t\t\tPage {currentPage} out of {totalPages}");
            App.Print($"\n\n\tId     CarInfo                 Features          Engine             Wheel             Others");
            foreach (var car in displayCars)
            {
                App.Print("\n\n");
                car.PrintDetails();
                App.Print("\n");
            }

            App.Print($"\n\n\n\t\t\t\t\t\t\t< {currentPage} > ");

            App.Print($"\n\t\t\t\t\t\t\t  ");
            var ch = Console.ReadKey().Key;
            switch (ch)
            {
                case ConsoleKey.Escape:
                    if (Want($"to Go back")) return;
                    break;
                case ConsoleKey.LeftArrow:
                    if (currentPage > 1) currentPage--;
                    else
                    {
                        App.ShowMessage("Warning","You are on First Page");
                        Task.Delay(800).Wait();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (currentPage < totalPages) currentPage++;
                    else
                    {
                        App.ShowMessage("Warning","You are on Last Page");
                        Task.Delay(800).Wait();
                    }
                    break;

                default:
                    App.ShowMessage("Error", "Provide some valid Input");
                    Task.Delay(800).Wait();
                    break;
            }
            goto displayPage;
            //App.ShowMessage("","",true);
        }
        internal static Car DeleteCarDesign(int carId)
        {
            if (_cars.Any(c => c.Id == carId))
            {
                if (Want($"to Delete the Car Design with Id \'{carId}\' "))
                {
                    int index = _cars.FindIndex(c => c.Id == carId);
                    var deletedCar = _cars[index];
                    _cars.RemoveAt(index);
                    _carsInTrash.Push(deletedCar);
                    return deletedCar;
                }
                else
                {
                    throw new Exception("You just cancelled the Deletion");
                }
            }
            else
            {
                throw new Exception("No Car Exist for the entered ID");
            }
        }
        internal static void UndoDelete()
        {
            var car = _carsInTrash.Peek();
            if (car != null)
            {
                if (Want($"to Re-store the Design of '{car.Name}' Car"))
                {
                    _carsInTrash.Pop();
                    _cars.Add(car);
                    App.ShowMessage("Info","Design has been Re-stored Successfully");
                }
                else
                {
                    App.ShowMessage("Warning","You just abort the Undo operation");
                }
            }
            else
            {
                App.ShowMessage("Warning","Nothing is their in Trash to Restore");
            }
        }


        internal static bool Want(string option)
        { 
            WantOption:;
            var choice = App.Input($"Do you want {option} (y/n)",1).ToLower();
            if (choice == "y" || choice == "n")
            {
                return (choice == "y");
            }
            else
            {
                App.ShowMessage("Error","Provide the valid input (y/n)\n",false,$"AppCode/Designs.cs   Line : 518   Want() {option} ");
                goto WantOption;
            }
        }
        internal static string ChooseColor()
        {
            App.Print("\n\tChoose the Color : ");

            var colors = _options["Colors"];
            App.Print("\n\n\t0. Custom\n");
            for (int i = 0; i < colors.Count; i++)
            {
                App.Print($"\n\t{i + 1}. {colors[i]}");
            }

            ChooseType:;
            var id = App.Input("Enter the color number",2);
            if (int.TryParse(id, out int colorId) && (colorId >= 0 && colorId < colors.Count + 1))
            {
                if (colorId == 0)
                {
                    var colorName =  App.Input("Enter the Color Name", 1);
                    while (!IsValidColor(colorName, out string msg))
                    {
                        App.ShowMessage("Warning",msg);
                        colorName = App.Input("Enter the Color Name", 2);
                    }
                    return colorName;
                }
                return colors[colorId - 1];
            }
            else
            {
                App.ShowMessage("Warning","Please choose the number from the above list");
                goto ChooseType;
            }
        }
        internal static Engine ChooseEngine(string vType)
        {
            App.Print("\n\tChoose an Engine : ");

            var engines = _engines.FindAll(e => e.VType == vType);
            foreach (var engine in engines)
            {
                App.Print(engine.GetEngineInfo(true));
            }

            ChooseEngineId:;
            var id = App.Input("Enter Engine Id",2);
            if (int.TryParse(id, out int engineId) && engines.Any(e => e.Id == engineId && e.VType == vType))
            {
                return _engines.Find(e => e.Id == engineId);
            }
            else
            {
                App.ShowMessage("Error","Please Choose an Engine from the above list via Id");
                goto ChooseEngineId;
            }
        }
        internal static Wheel ChooseWheel(string vType)
        {
            App.Print("\n\tChoose the Wheels : ");

            var wheels = _wheels.FindAll(e => e.VType == vType);
            foreach (var wheel in wheels)
            {
                App.Print(wheel.GetWheelInfo(true));
            }

            ChooseWheelId:;
            var id = App.Input("Enter Wheel Id", 2);
            if (int.TryParse(id, out int wheelId) && wheels.Any(w => w.Id == wheelId && w.VType == vType))
            {
                return wheels.Find(w => w.Id == wheelId);
            }
            else
            {
                App.ShowMessage("Error","Please Choose a Wheel from the above list via Id");
                goto ChooseWheelId;
            }
        }
        internal static string ChooseType()
        {
            App.Print("\n\tChoose the Car Type : ");

            var types = _options["types"].Where(type=> HasEngineAndWheelsForType(type)).ToList();
            for (int i = 0; i < types.Count; i++)
            {
                App.Print($"\n\t{i + 1}. {types[i]}");
            }

            ChooseType:;
            var id = App.Input("Enter the type number",2);

            if (int.TryParse(id, out int typeId) && (typeId > 0 && typeId < types.Count + 1))
            {
                return types[typeId - 1];
            }
            else
            {
                App.ShowMessage("Error", "Please choose the type from above list");
                goto ChooseType;
            }
        }
    }
}
