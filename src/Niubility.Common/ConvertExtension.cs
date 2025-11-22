namespace Niubility.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConvertExtension
    {
        private static readonly char[] DIGITALS_HEX = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        /// <summary>
        /// Converts the numeric value of each element of a specified array of bytes to its equivalent hexadecimal string representation.
        /// </summary>
        /// <returns>A string of hexadecimal pairs separated by hyphens, where each pair represents the corresponding element in value; for example, "7f2c4a00".</returns>
        public static string ToHexString(this byte[] bytes)
        {
            switch (bytes?.Length)
            {
                case null:
                    return null;
                case 0:
                    return string.Empty;
                default:
                    var swap = new char[bytes.Length * 2];

                    var pos = 0;
                    foreach (var b in bytes)
                    {
                        swap[pos++] = DIGITALS_HEX[b >> 4];
                        swap[pos++] = DIGITALS_HEX[b & 0xF];
                    }

                    return new string(swap);
            }
        }
    }
}