using System;

namespace AppLoggerLibrary
{
    public class LogData
    {
        private DateTime _createdAt;
        private string categoryName;
        private string message;
        private string stackTrace;

        public LogData(string categoryName, string message, string stackTrace)
        {
            _createdAt = DateTime.Now;
            this.categoryName = categoryName;
            this.message = message;
            this.stackTrace = stackTrace;
        }

        public override string ToString()
        {
            string log = _createdAt.ToString("dddd, dd/MM/yyyy hh:mm:ss") + $"\n{categoryName} > {message} > {stackTrace}\n\n";
            return log;
        }
    }
}
