using System;
using System.Text;
using Xunit;
using Unidecode.NET;

namespace Unidecode.NET.Tests
{
  // these are the same tests you find in UnidecoderTest, but here we are testing directly the internal function SlowUnidecode
  // which is the Unidecode implementation that normally gets called only for very long strings
  // (because it is slower, but it has no limits on the size of the input string)
  public class SlowUnidecoderTest
  {
    [Fact]
    public void DocTest()
    {
      Assert.Equal("Bei Jing ", "\u5317\u4EB0".SlowUnidecode());
    }

    [Fact]
    public void CustomTest()
    {
      Assert.Equal("Rabota s kirillitsei", "Работа с кириллицей".SlowUnidecode());
      Assert.Equal("aouoAOUO", "äöűőÄÖŨŐ".SlowUnidecode());
    }



    [Fact]
    public void PythonTest()
    {
      
      Assert.Equal("Hello, World!", "Hello, World!".SlowUnidecode());
      Assert.Equal("'\"\r\n", "'\"\r\n".SlowUnidecode());
      Assert.Equal("CZSczs", "ČŽŠčžš".SlowUnidecode());
      Assert.Equal("a", "ア".SlowUnidecode());
      Assert.Equal("a", "α".SlowUnidecode());
      Assert.Equal("a", "а".SlowUnidecode());
      Assert.Equal("chateau", "ch\u00e2teau".SlowUnidecode());
      Assert.Equal("vinedos", "vi\u00f1edos".SlowUnidecode());
    }

    [Fact]
    public void RussianAlphabetTest()
    {
      const string russianAlphabetLowercase = "а б в г д е ё ж з и й к л м н о п р с т у ф х ц ч ш щ ъ ы ь э ю я";
      const string russianAlphabetUppercase = "А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ъ Ы Ь Э Ю Я";

      const string expectedLowercase = "a b v g d e io zh z i i k l m n o p r s t u f kh ts ch sh shch ' y ' e iu ia";
      const string expectedUppercase = "A B V G D E Io Zh Z I I K L M N O P R S T U F Kh Ts Ch Sh Shch ' Y ' E Iu Ia";

      Assert.Equal(expectedLowercase, russianAlphabetLowercase.SlowUnidecode());
      Assert.Equal(expectedUppercase, russianAlphabetUppercase.SlowUnidecode());
    }


    [Fact]
    public void UnidecodeOnNullShouldReturnEmptyString()
    {
      Assert.Equal("", ((string)null).SlowUnidecode());
    }

  }
}
