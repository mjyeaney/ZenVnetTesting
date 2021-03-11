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
        /// Writes solver results to an output format.
        /// </summary>
        /// <param name="strategyName">The name of the strategy used.</param>
        /// <param name="solutions">The solver results to write.</param>
        public Task WriteResults(string strategyName, IEnumerable<Packet> solutions)
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
                Console.WriteLine($"No solutions found for the \"{strategyName}\" strategy.\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"The following solutions satisfy the \"{strategyName}\" strategy constraints:");
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