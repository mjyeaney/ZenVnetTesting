using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ZenTest.Logging;
using ZenTest.Results;
using ZenTest.Rulesets;
using ZenTest.Solvers;

namespace ZenTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // welcome banner
            Console.WriteLine();
            Console.WriteLine("**********************************");
            Console.WriteLine("     SMT Network Testing Demo     ");
            Console.WriteLine("**********************************");
            Console.WriteLine();

            // Load config and setup service
            ILogWriter logger = new ConsoleLogWriter();
            IConfiguration config = new ConfigurationSettings();
            IRulesetLoader loader = new DemoRulesetLoader(config, logger);
            //IRulesetLoader loader = new AzureRulesetLoader(config, logger);
            IVnetSolver solver = new ZenVnetSolver(config, logger);
            IResultsWriter writer = new ConsoleResultsWriter(config, logger);

            // Load ruleset
            Acl ruleset = await loader.LoadRuleset();

            // Find solutions
            Strategy invariant = Strategies.InboundPacketsAllowed();
            IEnumerable<Packet> solutions = await solver.FindSolutions(ruleset, invariant);

            // Print results
            await writer.WriteResults(solutions);
        }
    }
}
