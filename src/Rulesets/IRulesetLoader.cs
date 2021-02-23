using System.Threading.Tasks;

namespace ZenTest.Rulesets
{
    /// <summary>
    /// Defines methods used by ruleset loader implementations.
    /// </summary>
    public interface IRulesetLoader
    {
        /// <summary>
        /// Loads a ruleset from an underlying source.
        /// </summary>
        /// <returns>A deferred Acl object, representing the ruleset.</returns>
        Task<Acl> LoadRuleset();
    }
}