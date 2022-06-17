using System;

namespace Interface {

    public class FileLogger : ILogger
    {
        public void WriteLogg()
        {
           Console.WriteLine("File logger implemented");
        }
    }
}