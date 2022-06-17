using System;

namespace Interface {

    public class DatabaseLogger : ILogger
    {
        public void WriteLogg()
        {
             Console.WriteLine("DatabaseLogger implemented");

        }
    }
}