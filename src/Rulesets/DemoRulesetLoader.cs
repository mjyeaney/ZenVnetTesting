using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ZenTest.Logging;

namespace ZenTest.Rulesets
{
    /// <summary>
    /// Ruleset loader that build a demonstration set of rules.
    /// </summary>
    public class DemoRulesetLoader : IRulesetLoader
    {
        private IConfiguration _config;
        private ILogWriter _logger;

        /// <summary>
        /// Constructs a new DemoRulesetLoader instance.
        /// </summary>
        /// <param name="config">An IConfiguration instance.</param>
        /// <param name="logger">An ILogger instance.</param>
        public DemoRulesetLoader(IConfiguration config, ILogWriter logger)
        {
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Loads the demo ruleset.
        /// </summary>
        /// <returns>The populated demo ruleset.</returns>
        public Task<Acl> LoadRuleset()
        {
            _logger.Write("Loading default ruleset");

            // Create an ACL using the NSG rules we find
            Acl acl = new Acl();

            // Create some rules
            List<AclRule> rules = new List<AclRule>();

            // Allow VNET traffic
            AclRule vnetTraffic = new AclRule();
            vnetTraffic.Priority = 100;
            vnetTraffic.Permit = true;
            vnetTraffic.DstIpLow = IPAddressUtilities.StringToUint("10.0.1.0");
            vnetTraffic.DstIpHigh = IPAddressUtilities.StringToUint("10.0.1.255");
            vnetTraffic.SrcIpLow = IPAddressUtilities.StringToUint("10.0.1.0");
            vnetTraffic.SrcIpHigh = IPAddressUtilities.StringToUint("10.0.1.255");
            rules.Add(vnetTraffic);

            // Allow other VNET to talk to us ONLY on port 22 (ssh)
            AclRule otherVnetInbound = new AclRule();
            otherVnetInbound.Priority = 200;
            otherVnetInbound.Permit = false;
            otherVnetInbound.DstIpLow = IPAddressUtilities.StringToUint("10.0.1.0");
            otherVnetInbound.DstIpHigh = IPAddressUtilities.StringToUint("10.0.1.255");
            otherVnetInbound.DstPort = 22;
            otherVnetInbound.SrcIpLow = IPAddressUtilities.StringToUint("192.168.1.10");
            otherVnetInbound.SrcIpHigh = IPAddressUtilities.StringToUint("192.168.1.10");
            rules.Add(otherVnetInbound);

            // Can we reach Google DNS?
            AclRule googleTest = new AclRule();
            googleTest.Priority = 300;
            googleTest.Permit = false;
            googleTest.SrcIpLow = IPAddressUtilities.StringToUint("10.0.1.0");
            googleTest.SrcIpHigh = IPAddressUtilities.StringToUint("10.0.1.255");
            googleTest.DstIpLow = IPAddressUtilities.StringToUint("8.8.8.8");;
            googleTest.DstIpHigh = IPAddressUtilities.StringToUint("8.8.8.8");
            rules.Add(googleTest);

            // Block all inbound internet traffic
            AclRule internetInbound = new AclRule();
            internetInbound.Priority = 65000;
            internetInbound.Permit = false;
            internetInbound.DstIpLow = IPAddressUtilities.StringToUint("10.0.1.0");
            internetInbound.DstIpHigh = IPAddressUtilities.StringToUint("10.0.1.255");
            internetInbound.SrcIpLow = IPAddressUtilities.StringToUint("1.0.0.1");
            internetInbound.SrcIpHigh = IPAddressUtilities.StringToUint("223.255.255.255");
            rules.Add(internetInbound);
            
            acl.Rules = rules.OrderBy(r => r.Priority).ToArray();
            _logger.Write("Default ruleset loaded");
            return Task.FromResult(acl);
        }
    }
}