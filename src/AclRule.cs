using ZenLib;
using static ZenLib.Language;

namespace ZenTest
{
    /// <summary>
    /// An individual ACL rule.
    /// </summary>
    public class AclRule
    {
        /// <summary>
        /// If true, matching packets will be permitted; if false, they will be blocked.
        /// </summary>
        public bool Permit { get; set; }
        
        /// <summary>
        /// The lo-side of the destination IP prefix, in numeric form.
        /// </summary>
        public uint DstIpLow { get; set; }
        
        /// <summary>
        /// The high-side of the destination IP prefix, in numeric form.
        /// </summary>
        public uint DstIpHigh { get; set; }
        
        /// <summary>
        /// The destination port, or zero for "any".
        /// </summary>
        public ushort DstPort { get; set; }
        
        /// <summary>
        /// The lo-side of the source IP prefix, in numeric form.
        /// </summary>
        public uint SrcIpLow { get; set; }
        
        /// <summary>
        /// The high-side of the source IP prefix, in numeric form.
        /// </summary>
        public uint SrcIpHigh { get; set; }
        
        /// <summary>
        /// The source port, or zero for "any".
        /// </summary>
        public ushort SrcPort { get; set; }
        
        /// <summary>
        /// The priority of this rule.
        /// </summary>
        public uint Priority { get; set; }

        /// <summary>
        /// Checks if a given packet matches this rule.
        /// </summary>
        /// <param name="packet">The packet to test.</param>
        /// <returns>True if the packet matches; otherwise false.</returns>
        public Zen<bool> Matches(Zen<Packet> packet)
        {
            // TODO: If ports are zero, do NOT include in predicate (zero implies "ANY")
            return And(packet.GetDstIp() >= this.DstIpLow,
                       packet.GetDstIp() <= this.DstIpHigh,
                       packet.GetDstPort() == this.DstPort,
                       packet.GetSrcIp() >= this.SrcIpLow,
                       packet.GetSrcIp() <= this.SrcIpHigh,
                       packet.GetSrcPort() == this.SrcPort);
        }
    }
}