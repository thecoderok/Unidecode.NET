using System;
using Xunit;

namespace Unidecode.NET.Tests
{
    public class UnidecoderTest
    {
        [Fact]
        public void Test()
        {
            Assert.Equal("Bei Jing ", "\u5317\u4EB0".Unidecode());
            Assert.Equal("Rabota s kirillitsey", "Работа с кириллицей".Unidecode());
            Assert.Equal("aouoAOUO", "äöűőÄÖŨŐ".Unidecode());
            Assert.Equal("Hello, World!", "Hello, World!".Unidecode());
            Assert.Equal("'\"\r\n", "'\"\r\n".Unidecode());
            Assert.Equal("CZSczs", "ČŽŠčžš".Unidecode());
            Assert.Equal("a", "ア".Unidecode());
            Assert.Equal("a", "α".Unidecode());
            Assert.Equal("a", "а".Unidecode());
            Assert.Equal("chateau", "ch\u00e2teau".Unidecode());
            Assert.Equal("vinedos", "vi\u00f1edos".Unidecode());
        }
    }
}
