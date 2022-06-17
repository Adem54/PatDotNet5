using System;

namespace Interface {

    public class SmsLogger : ILogger
    {
        public void WriteLogg()
        {
         Console.WriteLine("SmsLogger implemented");

        }
    }
}