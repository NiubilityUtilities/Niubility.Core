using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Niubility.Common
{
    public static class HashAlgorithmInstances<THashAlgorithm>
        where THashAlgorithm : HashAlgorithm
    {
        [ThreadStatic]
        private static THashAlgorithm instance;
        public static THashAlgorithm Instance { get => instance ?? Create(); }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static THashAlgorithm Create()
        {
            var createMethod = typeof(THashAlgorithm).GetMethod(nameof(HashAlgorithm.Create),
                BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
                Type.DefaultBinder, Type.EmptyTypes, null);

            if (null != createMethod)
            {
                instance = (THashAlgorithm)createMethod.Invoke(null, null);
            }
            else
            {
                instance = Activator.CreateInstance<THashAlgorithm>();
            }

            return instance;
        }
    }
}