using System;

namespace ZenTest.Logging
{
    /// <summary>
    /// A console-based logger implementation.
    /// </summary>
    public class ConsoleLogWriter : ILogWriter
    {
        /// <summary>
        /// Writes the provided message to the console output.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public void Write(string message)
        {
            string formattedMsg = String.Format("{0:O} - {1}: {2}", DateTime.Now, "INFO", message);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(formattedMsg);
            Console.ResetColor();
        }
    }
}