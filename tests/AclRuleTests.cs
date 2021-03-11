using Xunit;
using ZenLib;

namespace ZenTest
{
    public class AclRuleTests
    {
        [Fact]
        public void PacketsMatchOnExactFields()
        {
            Packet p = new Packet();
            p.DstIp = IPAddressUtilities.StringToUint("10.0.1.10");
            p.DstPort = 80;
            p.SrcIp = IPAddressUtilities.StringToUint("1.1.1.1");
            p.SrcPort = 5000;

            AclRule rule = new AclRule();
            rule.DstIpLow = p.DstIp;
            rule.DstIpHigh = p.DstIp;
            rule.DstPort = p.DstPort;
            rule.SrcIpLow = p.SrcIp;
            rule.SrcIpHigh = p.SrcIp;
            rule.SrcPort = p.SrcPort;

            Assert.True(rule.Matches((Zen<Packet>)p).Equals((Zen<bool>)true));
        }

        [Fact]
        public void PacketsDoNotMatchOnDifferingPorts()
        {
            Packet p = new Packet();
            p.DstIp = IPAddressUtilities.StringToUint("10.0.1.10");
            p.DstPort = 80;
            p.SrcIp = IPAddressUtilities.StringToUint("1.1.1.1");
            p.SrcPort = 5000;

            AclRule rule = new AclRule();
            rule.DstIpLow = p.DstIp;
            rule.DstIpHigh = p.DstIp;
            rule.DstPort = 8080;
            rule.SrcIpLow = p.SrcIp;
            rule.SrcIpHigh = p.SrcIp;
            rule.SrcPort = 5001;

            Assert.False(rule.Matches((Zen<Packet>)p).Equals((Zen<bool>)true));
        }

        [Fact]
        public void PacketsDoNotMatchOnDifferingIP()
        {
            Packet p = new Packet();
            p.DstIp = IPAddressUtilities.StringToUint("10.0.1.10");
            p.DstPort = 80;
            p.SrcIp = IPAddressUtilities.StringToUint("1.1.1.1");
            p.SrcPort = 5000;

            uint testDstIP = IPAddressUtilities.StringToUint("10.0.2.10");
            uint testSrcIP = IPAddressUtilities.StringToUint("1.1.1.2");

            AclRule rule = new AclRule();
            rule.DstIpLow = testDstIP;
            rule.DstIpHigh = testDstIP;
            rule.DstPort = p.DstPort;
            rule.SrcIpLow = testSrcIP;
            rule.SrcIpHigh = testSrcIP;
            rule.SrcPort = p.SrcPort;

            Assert.False(rule.Matches((Zen<Packet>)p).Equals((Zen<bool>)true));
        }
    }
}