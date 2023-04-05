using AppLoggerLibrary;
using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DesignCar.AppCode
{
    internal static class App
    {
        private static readonly string directory = ConfigurationManager.AppSettings["path"].ToString();
        private static readonly string _logFilePath = directory + "Logs.txt";
        private static readonly Logger _appLogs = new Logger(_logFilePath);

        internal static void Start()
        {
            var bindData = Task.Run(() => { Designs.BindData(); });
            try
            {
                UI.IndexPage(bindData).Wait();
            }
            catch (Exception e)
            {
                Print("\n\n\t" + e.Message);
            }
        }

        internal static void ShowMessage(string category = "", string message="",bool hold = false, string stackTrace = "",  
            [CallerFilePath] string callerPath = "", [CallerLineNumber] int line = 0, [CallerMemberName] string memberName = "")
        {
            if(category != "Event")
            {
                Console.Write("\n\t" + message);
                if (hold)
                {
                    Console.Write("\n\tPress any key to continue..");
                    Console.ReadKey();
                }
            }
            try
            {
                if(stackTrace == "") stackTrace = callerPath + "   Line:" + line + "   " + memberName; 
                _appLogs.Log(new LogData(category, message, stackTrace));
            }
            catch(Exception e)
            {
                Console.Write(e.Message);
                Console.ReadKey();
            }
        }
        internal static void Print(string text)
        {
            Console.Write(text);
        }
        internal static string Input(string info, int newLines = 0)
        {
            while(newLines-- >0) { Console.Write("\n");}
            Console.Write("\t" + info + " : ");
            var choice = Console.ReadLine();
            return choice;
        }
        internal static void ComingSoon(string option = "")
        {
            Console.Clear();
            ShowMessage("Info",$"Options will be coming soon : {option}", true);
        }
        internal static void PrintAppName(string note = "Type 'Esc' or '0' and press enter to exit")
        {
            Console.Clear();
            if(note != "") Console.Write($"\n\tNote : {note}.");
            Console.Write("\n\n\tWelcome to the Design Car App");
        }

    }
}
