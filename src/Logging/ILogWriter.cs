using System.Threading.Tasks;

namespace ZenTest.Logging
{
    /// <summary>
    /// Defines methods common to all log writer implementations.
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Writes the provided message to the logging provider.
        /// </summary>
        /// <param name="message">The message to write.</param>
        void Write(string message);
    }
}