using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.ResourceManager.Network;
using Azure.ResourceManager.Network.Models;
using Azure.Identity;

namespace ZenTest.Rulesets
{
    public class AzureRulesetLoader : IRulesetLoader
    {
        /// <summary>
        /// Loads a ruleset from an underlying source.
        /// </summary>
        /// <returns>A deferred Acl object, representing the ruleset.</returns>
        public async Task<Acl> LoadRuleset()
        {
            var subscriptionId = Environment.GetEnvironmentVariable("ARM_SUBSCRIPTION_ID");
            var cliCred = new DefaultAzureCredential();
            var client = new NetworkManagementClient(subscriptionId, cliCred);
            var nsgList = client.NetworkSecurityGroups.ListAllAsync();
            var rules = new List<SecurityRule>();
            var acl = new Acl();
            var aclRules = new List<AclRule>();

            // Read rules
            await foreach (var nsg in nsgList)
            {
                foreach (var rule in nsg.SecurityRules)
                {
                    rules.Add(rule);
                }
            }

            // Sort rules and add to our out list
            foreach (var rule in rules.OrderBy(r => r.Priority))
            {
                // Convert NSG rule to ACL rule (TODO: Need helper here)
                aclRules.Add(new AclRule()
                {
                    Permit = (rule.Access == SecurityRuleAccess.Allow),
                    Priority = (uint)rule.Priority,
                    DstPort = ushort.Parse(rule.DestinationPortRange),
                    SrcPort = ushort.Parse(rule.SourcePortRange),
                    //SrcIpLow
                    //SrcIpHigh
                    //DstIpLow
                    //DstIpHigh
                });
            }

            acl.Rules = aclRules.ToArray();
            return acl;
        }
    }
}