using System;

namespace ZenTest
{
    /// <summary>
    /// Configuration settings available to the application.
    /// </summary>
    public class ConfigurationSettings : IConfiguration
    {
        /// <summary>
        /// Creates a new ConfigurationSettings instance.
        /// </summary>
        public ConfigurationSettings()
        {
            this.MaxReportedResults = 25;
        }

        /// <summary>
        /// The maximum number of results to report.
        /// </summary>
        public int MaxReportedResults { get; set; }
    }
}