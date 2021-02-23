using ZenLib;
using static ZenLib.Language;

namespace ZenTest
{
    /// <summary>
    /// An ACL with a list of prioritized rules.
    /// </summary>
    public class Acl
    {
        /// <summary>
        /// The name of the ACL
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of rules.
        /// </summary>
        public AclRule[] Rules { get; set; }

        /// <summary>
        /// Tests to see if a the packet is allowed based on the ruleset.
        /// </summary>
        /// <param name="packet">The packet to test.</param>
        /// <returns>True if the packet is allowed; otherwise false.</returns>
        public Zen<bool> Allowed(Zen<Packet> packet)
        {
            return Allowed(packet, 0);
        }

        /// <summary>
        /// Compute whether a packet is allowed by the ACL recursively
        /// </summary>
        /// <param name="packet">The packet to test.</param>
        /// <param name="lineNumber">The rule line to check.</param>
        /// <returns>True if the packet is allowed; otherwise false.</returns>
        private Zen<bool> Allowed(Zen<Packet> packet, int lineNumber)
        {
            if (lineNumber >= this.Rules.Length)
            {
                return false; // Zen implicitly converts false to Zen<bool>
            }

            var line = this.Rules[lineNumber];

            // if the current line matches, then return the action, otherwise continue to the next line
            return If(line.Matches(packet), line.Permit, this.Allowed(packet, lineNumber + 1));
        }
    }
}