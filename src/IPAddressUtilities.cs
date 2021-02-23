using System;
using System.Net;

namespace ZenTest
{
    /// <summary>
    /// Utility methods used for IP address / numeric format conversions.
    /// </summary>
    public static class IPAddressUtilities
    {
        /// <summary>
        /// Converts an IP address to a numeric form.
        /// </summary>
        /// <param name="address">The string form of the IP address to convert.</param>
        /// <returns>The numeric form of the address, in correct byte order.</returns>
        public static uint StringToUint(string address)
        {
            byte[] bytes = IPAddress.Parse(address).GetAddressBytes();

            // flip big-endian(network order) to little-endian
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// Converts a numeric IP address format into a string representation.
        /// </summary>
        /// <param name="numericAddress">The numeric IP address value to convert.</param>
        /// <returns>The string represetnation of the IP address in dot-form.</returns>
        public static string FromUint(uint numericAddress)
        {
            byte[] bytes = BitConverter.GetBytes(numericAddress);

            // flip little-endian to big-endian(network order)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return new IPAddress(bytes).ToString();
        }
    }
}