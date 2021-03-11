using System.Threading.Tasks;
using System.Collections.Generic;

namespace ZenTest.Results
{
    /// <summary>
    /// Defines methods used for writing results to an output format.
    /// </summary>
    public interface IResultsWriter
    {
        /// <summary>
        /// Writes solver results to an output format.
        /// </summary>
        /// <param name="strategyName">The name of the strategy used.</param>
        /// <param name="solutions">The solver results to write.</param>
        Task WriteResults(string strategyName, IEnumerable<Packet> solutions);
    }
}