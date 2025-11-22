using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Niubility.Common
{
    public static class HashAlgorithmExtension
    {
        public static string ComputeHash<THashAlgorithm>(this string payload, Encoding encoding)
            where THashAlgorithm : HashAlgorithm
        {
            if (string.IsNullOrEmpty(payload))
            {
                return string.Empty;
            }
            var bytes = (encoding ?? Encoding.UTF8).GetBytes(payload);
            var result = ComputeHash<THashAlgorithm>(bytes);
            return result;
        }
        public static string ComputeHash<THashAlgorithm>(this byte[] bytes)
            where THashAlgorithm : HashAlgorithm
        {
            if (!(bytes?.Any() ?? false))
            {
                return string.Empty;
            }
            var data = HashAlgorithmInstances<THashAlgorithm>.Instance.ComputeHash(bytes);
            var result = ConvertExtension.ToHexString(data);
            return result;
        }
    }
}