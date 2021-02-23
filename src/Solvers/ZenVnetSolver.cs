using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using ZenLib;
using static ZenLib.Language;
using ZenTest.Logging;

namespace ZenTest.Solvers
{
    /// <summary>
    /// A ZenLib based solver implementation.
    /// </summary>
    public class ZenVnetSolver : IVnetSolver
    {
        private IConfiguration _config;
        private ILogWriter _logger;

        /// <summary>
        /// Creates a new instance of the ZenVnetSolver.
        /// </summary>
        public ZenVnetSolver(IConfiguration config, ILogWriter logger)
        {
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Given an Acl ruleset and a stragey, computes and finds any possible solutions.
        /// </summary>
        /// <param name="ruleSet">The Acl ruleset to verify.</param>
        /// <param name="strategy">The Strategy representing the test invariant.</param>
        /// <returns>A list of solution (if any) that satisfy the provided strategy.</returns>
        public Task<IEnumerable<Packet>> FindSolutions(Acl ruleSet, Strategy strategy)
        {
            // Create Zen func and run input evaluation
            Stopwatch sw = new Stopwatch();
            ZenFunction<Packet, bool> func = Function<Packet, bool>(p => ruleSet.Allowed(p));
            _logger.Write("Starting model evaluation");
            _logger.Write($"Using the \"{strategy.Name}\" strategy");
            
            sw.Start();

            IEnumerable<Packet> solutions = func.FindAll(strategy.Invariant, 
                backend: ZenLib.ModelChecking.Backend.Z3)
            .Take(_config.MaxReportedResults)
            .ToList(); // Force evaluation

            sw.Stop();
            
            _logger.Write($"Model evaluation finished ({sw.ElapsedMilliseconds} msec)");
            return Task.FromResult(solutions);
        }
    }
}