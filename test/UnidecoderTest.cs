using System.Text;
using Xunit;

namespace Unidecode.NET.Tests
{
    public class UnidecoderTest
    {
        [Fact]
        public void DocTest()
        {
            Assert.Equal("Bei Jing ", "\u5317\u4EB0".Unidecode());
        }

        [Fact]
        public void CustomTest()
        {
            Assert.Equal("Rabota s kirillitsei", "Работа с кириллицей".Unidecode());
            Assert.Equal("aouoAOUO", "äöűőÄÖŨŐ".Unidecode());
        }

        [Fact]
        public void PythonTest()
        {
            Assert.Equal("Hello, World!", "Hello, World!".Unidecode());
            Assert.Equal("'\"\r\n", "'\"\r\n".Unidecode());
            Assert.Equal("CZSczs", "ČŽŠčžš".Unidecode());
            Assert.Equal("a", "ア".Unidecode());
            Assert.Equal("a", "α".Unidecode());
            Assert.Equal("a", "а".Unidecode());
            Assert.Equal("chateau", "ch\u00e2teau".Unidecode());
            Assert.Equal("vinedos", "vi\u00f1edos".Unidecode());
        }
        
        [Fact]
        public void RussianAlphabetTest()
        {
            const string russianAlphabetLowercase = "а б в г д е ё ж з и й к л м н о п р с т у ф х ц ч ш щ ъ ы ь э ю я";
            const string russianAlphabetUppercase = "А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ъ Ы Ь Э Ю Я";

            const string expectedLowercase = "a b v g d e io zh z i i k l m n o p r s t u f kh ts ch sh shch ' y ' e iu ia";
            const string expectedUppercase = "A B V G D E Io Zh Z I I K L M N O P R S T U F Kh Ts Ch Sh Shch ' Y ' E Iu Ia";

            Assert.Equal(expectedLowercase, russianAlphabetLowercase.Unidecode());
            Assert.Equal(expectedUppercase, russianAlphabetUppercase.Unidecode());
        }

        [Fact]
        public void CharUnidecodeTest()
        {
            const string input = "а б в г д е ё ж з и й к л м н о п р с т у ф х ц ч ш щ ъ ы ь э ю я А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ъ Ы Ь Э Ю Я";
            const string expected = "a b v g d e io zh z i i k l m n o p r s t u f kh ts ch sh shch ' y ' e iu ia A B V G D E Io Zh Z I I K L M N O P R S T U F Kh Ts Ch Sh Shch ' Y ' E Iu Ia";

            var sb = new StringBuilder(expected.Length);
            foreach (var c in input)
                sb.Append(c.Unidecode());
            
            var result = sb.ToString();

            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void GermanAlphabetTest()
        {
            const string input = "a b c d e f g h i j k l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z ä ö ü ß Ä Ö Ü ẞ";
            const string expected = "a b c d e f g h i j k l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z a o u ss A O U Ss";

            var sb = new StringBuilder(expected.Length);
            foreach (var c in input)
                sb.Append(c.Unidecode());
            
            var result = sb.ToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UnidecodeOnNullShouldReturnEmptyString()
        {
            Assert.Equal("", ((string)null).Unidecode());
        }
    }
}
