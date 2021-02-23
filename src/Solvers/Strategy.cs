using System;
using ZenLib;

namespace ZenTest.Solvers
{
    /// <summary>
    /// Defines an invariant used from solver instances.
    /// </summary>
    public class Strategy
    {
        /// <summary>
        /// The name of this stragey.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The invariant method used from the solver engine.
        /// </summary>
        public Func<Zen<Packet>, Zen<bool>, Zen<bool>> Invariant { get; set; }
    }
}