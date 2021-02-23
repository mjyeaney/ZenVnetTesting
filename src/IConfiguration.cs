using System;

namespace ZenTest
{
    /// <summary>
    /// Defines base configuration structure.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// The maximum number of results to report.
        /// </summary>
        int MaxReportedResults { get; set; }
    }
}