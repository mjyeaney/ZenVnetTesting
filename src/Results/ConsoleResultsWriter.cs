using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ZenTest.Logging;

namespace ZenTest.Results
{
    /// <summary>
    /// A console-based results writer, which displays solver solutions via STDOUT.
    /// </summary>
    public class ConsoleResultsWriter : IResultsWriter
    {
        private const int COLUMN_WIDTH = 80;
        private ILogWriter _logger;
        private IConfiguration _config;

        /// <summary>
        /// Creates a new instance of the ConsoleResultsWriter.
        /// </summary>
        public ConsoleResultsWriter(IConfiguration config, ILogWriter logger)
        {
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// Writes the solver results to the console.
        /// </summary>
        /// <param name="solutions">The list of solutions found by the solver.</param>
        public Task WriteResults(IEnumerable<Packet> solutions)
        {
            _logger.Write("Writing solution results");

            Console.WriteLine();
            Console.WriteLine("**********************************");
            Console.WriteLine("          Summary Report          ");
            Console.WriteLine("**********************************");
            Console.WriteLine();

            if (!solutions.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No solutions found!\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"The following solutions satisfy the model constraints:");
                Console.WriteLine($"(displaying a maximum of {_config.MaxReportedResults} results)");
                Console.WriteLine();
                Console.ResetColor();

                foreach (Packet s in solutions)
                {
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine();

            _logger.Write("Completed writing solution results");
            return Task.CompletedTask;
        }
    }
}