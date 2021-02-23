using ZenLib;

namespace ZenTest
{
    /// <summary>
    /// Helper extension methods for manipulating packets from Zen wrappers.
    /// </summary>
    public static class PacketExtensions
    {
        public static Zen<uint> GetDstIp(this Zen<Packet> packet)
        {
            return packet.GetField<Packet, uint>("DstIp");
        }

        public static Zen<uint> GetSrcIp(this Zen<Packet> packet)
        {
            return packet.GetField<Packet, uint>("SrcIp");
        }

        public static Zen<ushort> GetDstPort(this Zen<Packet> packet)
        {
            return packet.GetField<Packet, ushort>("DstPort");
        }

        public static Zen<ushort> GetSrcPort(this Zen<Packet> packet)
        {
            return packet.GetField<Packet, ushort>("SrcPort");
        }
    }
}