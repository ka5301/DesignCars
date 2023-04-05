using System.IO;
namespace AppLoggerLibrary
{
    public class Logger
    {
        private readonly string LogFilePath;

        public Logger(string FilePath)
        {
            LogFilePath = FilePath;
            //using (var file = new FileStream(LogFilePath,FileMode.Truncate)) { }
        }

        public void Log(LogData data)
        {
            var text = File.ReadAllText(LogFilePath);
            File.WriteAllText(LogFilePath, data.ToString() + text);
        }
    }
}
