using System;
using Xunit;

namespace Lykke.Common.Crypto.Test
{
    public class StringCryptoExtTest
    {
        [Fact]
        public void EqualAlgorithm()
        {
            string test = "testString test String";
            string key = "testKey";
            var t = test.AesEncript(key);
            var tt = t.AesDecript(key);
            Assert.Equal(test, test.AesEncript(key).AesDecript(key));
        }
    }
}
