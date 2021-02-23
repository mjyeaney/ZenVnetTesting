using System.Threading.Tasks;
using System.Collections.Generic;

namespace ZenTest.Solvers
{
    /// <summary>
    /// Defines methods used by solver instances.
    /// </summary>
    public interface IVnetSolver
    {
        /// <summary>
        /// Given an Acl ruleset and a stragey, computes and finds any possible solutions.
        /// </summary>
        /// <param name="ruleSet">The Acl ruleset to verify.</param>
        /// <param name="strategy">The Strategy representing the test invariant.</param>
        /// <returns>A list of solution (if any) that satisfy the provided strategy.</returns>
        Task<IEnumerable<Packet>> FindSolutions(Acl ruleSet, Strategy strategy);
    }
}