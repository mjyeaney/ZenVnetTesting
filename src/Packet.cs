using System;
using System.Globalization;

namespace ZenTest
{
    /// <summary>
    /// Models a packet flowing through a ruleset.
    /// </summary>
    public class Packet
    {
        /// <summary>
        /// The destination IP of the packet.
        /// </summary>
        public uint DstIp { get; set; }
        
        /// <summary>
        /// The source IP of the packet
        /// </summary>
        public uint SrcIp { get; set; }

        /// <summary>
        /// The destination port of the packet.
        /// </summary>
        public ushort DstPort { get; set; }

        /// <summary>
        /// The source port of the packet.
        /// </summary>
        public ushort SrcPort { get; set; }

        /// <summary>
        /// Converts this instance into a string form.
        /// </summary>
        public override string ToString()
        {
            return String.Format("Source: {0}:{2}\t\tDestination: {1}:{3}",
                IPAddressUtilities.FromUint(SrcIp),
                IPAddressUtilities.FromUint(DstIp),
                SrcPort == 0 ? "*" : SrcPort.ToString(CultureInfo.InvariantCulture),
                DstPort == 0 ? "*" : DstPort.ToString(CultureInfo.InvariantCulture));
        }
    }
}