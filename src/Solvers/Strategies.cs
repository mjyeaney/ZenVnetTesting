using System;
using ZenLib;
using static ZenLib.Language;

namespace ZenTest.Solvers
{
    /// <summary>
    /// Pre-configured strategies used for various VNET testing methods.
    /// </summary>
    public class Strategies
    {
        /// <summary>
        /// This strategy finds all instances where packets are allowed from external (i.e., internet)
        /// based sources.
        /// </summary>
        /// <returns>A Strategy instance representing this test.</returns>
        public static Strategy InboundPacketsAllowed()
        {
            Strategy inboundAllowed = new Strategy();
            inboundAllowed.Name = "Find Inbound Allowed";
            inboundAllowed.Invariant = (packet, result) =>
                {
                    Zen<uint> srcIp = packet.GetSrcIp();
                    return And(
                        Or(
                            And(
                                srcIp >= IPAddressUtilities.StringToUint("1.0.0.1"),
                                srcIp < IPAddressUtilities.StringToUint("10.0.1.0")
                            ),
                            And(
                                srcIp > IPAddressUtilities.StringToUint("10.0.1.255"),
                                srcIp <= IPAddressUtilities.StringToUint("223.255.255.255")
                            )
                        ), // Looking for packets _not_ coming from the VNET ..
                        result == true // ..that are allowed
                    );
                };
            return inboundAllowed;
        }
    }
}