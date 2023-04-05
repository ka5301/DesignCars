using System;
using System.Threading.Tasks;

namespace DesignCar.AppCode
{
    internal static class UI
    {
        private static void DesignNewCarPage()
        {
            App.ShowMessage("Event", "Redirected to Design New Car Page");
            if (!Designs.HaveEnginesAndWheels())
            {
                App.PrintAppName("");
                App.ShowMessage("Info","\n\tSome Engines and Wheels Designs are required to create a car design\n\tbut here no designs are present so you can't create the design.\n", true);
                return;
            }

            var car = new Car();
            car.InputDetails();
        }
        private static void DisplayDesignedCarsPage()
        {
            App.ShowMessage("Event", "Redirected to Display Designed Car Page");
            App.PrintAppName("");
            Designs.DisplayCarDesigns();
        }
        private static void DeleteDesignedCarPage()
        {
            App.ShowMessage("Event", "Redirected to Delete Designed Car Page");
            DeleteCarDesign:;
            var cars = Designs.GetCars();
            if (cars.Count == 0)
            {
                if (Designs.IsTrashEmpty())
                {
                    App.PrintAppName("");
                    App.ShowMessage("Info", "\n\tThere is no design to delete\n\t", true);
                    return;
                }
                else
                {
                    App.PrintAppName("Type 'Esc' or '0' and press enter to go back to main menu,\n\t       Type 'Restore' and press enter to Restore from Trash");
                    App.ShowMessage("Info","\n\tThere is no design to delete\n\n");
                    var choice = App.Input("Enter your choice");
                    if (choice.ToLower() == "esc")
                    {
                        if (Designs.Want($"to Go back")) return;
                        goto DeleteCarDesign;
                    }
                    else if (choice.ToLower() == "restore")
                    {
                        Designs.UndoDelete();
                    }
                    else
                    {
                        App.ShowMessage("Warning","Provide some valid Input");

                    }

                }

            }
            else
            {
                App.PrintAppName("Type 'Esc' or '0' and press enter to go back to main menu,\n\t       Type 'Restore' and press enter to Restore the deleted Design");
                App.Print("\n\n\tCar Designs Info : \n");
                foreach (var car in cars)
                {
                    App.Print("\n\t" + car.GetInfo());
                }

                var id = App.Input("Enter Car Id which you want to delete", 2);
                if (id.ToLower() == "esc")
                {
                    if (Designs.Want($"to Go back")) return;
                    goto DeleteCarDesign;
                }
                else if (id.ToLower() == "restore")
                {
                    Designs.UndoDelete();
                }

                else if (int.TryParse(id, out int carId))
                {
                    if (carId == 0)
                    {
                        if (Designs.Want($"to Go back")) return;
                        goto DeleteCarDesign;
                    }

                    try
                    {
                        var deletedCar = Designs.DeleteCarDesign(carId);
                        App.ShowMessage("Info","Selected Design has been successfully deleted\n");
                        deletedCar.PrintDetails(1);
                        App.ShowMessage("", "",true);
                        goto DeleteCarDesign;

                    }
                    catch (Exception e)
                    {
                        App.ShowMessage("Error", e.Message,false,e.StackTrace.ToString());
                        //goto DeleteCarDesign;
                    }
                }
                else
                {
                    App.ShowMessage("Warning","Provide some valid Input");
                }

            }

            Task.Delay(1500).Wait();
            goto DeleteCarDesign;
        }

        public static async Task IndexPage(Task bindData)
        {
            App.ShowMessage("Event", "Redirected to Index Page");
            while (true)
            {

                App.PrintAppName();
                App.Print("\n\n\t1. Design A New Car");
                App.Print("\n\t2. List Designed Cars");
                App.Print("\n\t3. Delete Any Design");
                App.Print("\n\t4. Display the Loaded Data");

                var choice = App.Input("Enter your choice", 2).ToLower();
                if(choice == "1" || choice == "2" || choice == "3" || choice == "4") App.Print("\tProcessing the request....");
                await bindData;
                switch (choice)
                {
                    case "esc":
                    case "0":
                        if (Designs.Want("to Quit"))
                        {
                            App.Print("\n\n\t");
                            App.ShowMessage("Event", "Exit through App");
                            return;
                        }
                        break;
                    case "1":
                        DesignNewCarPage();
                        break;
                    case "2":
                        DisplayDesignedCarsPage();
                        break;
                    case "3":
                        DeleteDesignedCarPage();
                        break;
                    case "4":
                        Designs.DisplayLoadedData();
                        break;
                    default:
                        App.ShowMessage("Warning", "Please enter the correct choice");
                        Task.Delay(800).Wait();
                        break;
                }
            }
        }
    }
}
