using System;
using Xunit;

namespace Lykke.Common.Crypto.Test
{
    public class StringCryptoExtTest
    {
        private static string test = "Test string test string";
        private static string key = "TestKey";

        [Fact]
        public void EqualAlgorithm()
        {
           
            var t = test.AesEncript(key);
            var tt = t.AesDecript(key);
            Assert.Equal(test, test.AesEncript(key).AesDecript(key));
        }

        [Fact]
        public void TestEncriptAlorithm()
        {

            var t = test.AesEncript(key);
            Assert.Equal("0b4929ccad4ab59a1d0b3efb7bad03bf6464e7b993bdd228d71e41299ad95936820dd2d6d3bd3edda9d654f7300080b9820dd2d6d3bd3edda9d654f7300080b9", t);

 
        }
    }
}
