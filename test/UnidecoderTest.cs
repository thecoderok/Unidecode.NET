using System.Linq;
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

    /// <summary>
    ///   Test that code points with the maximum low byte of 255 do not 
    ///   cause an IndexOutOfRangeException (see commit: acd8fb4)
    /// </summary>
    [Fact]
    public void MaximumLowByteTest()
    {
      byte low = 0xFF;
      for (var high = (char)0; high <= byte.MaxValue; high++)
      {
        var codePoint = (char)((high << 8) | low);
        try
        {
          codePoint.Unidecode();
        }
        catch (System.IndexOutOfRangeException)
        {
          Assert.True(false);
        }
      }
    }


    [Fact]
    public void test()
    {
      var translated = "Süßigkeit".Unidecode();
      Assert.Equal("Sussigkeit", translated);
      // I search some letters in the decoded string
      var idx_u = translated.IndexOf("u");
      var idx_i = translated.IndexOf("i");
      var idx_e = translated.IndexOf("e");
      var idx_t = translated.IndexOf("t");


      Assert.Equal(1, idx_u);
      Assert.Equal(4, idx_i);
      Assert.Equal(7, idx_e);
      Assert.Equal(9, idx_t);

      // I want to know where are the corresponding locations in the source string of these occourrences

      var srcIndexes = Unidecoder.FindIndexesInSourceString("Süßigkeit", new int[] { idx_u, idx_i, idx_e, idx_t }).ToArray();

      Assert.Equal(1, srcIndexes[0]);
      Assert.Equal(3, srcIndexes[1]);
      Assert.Equal(6, srcIndexes[2]);
      Assert.Equal(8, srcIndexes[3]);
    }


    /// <summary>
    ///   Tests that Unidecode "stackAlloc" optimized implementation falls back to the slowest SlowUnidecode implementation for long strings,
    ///   instead of raising an error
    /// </summary>
    [Fact]
    public void SlowUnidecodeIsCalledForLongStrings()
    {
      var srcBuilder = new StringBuilder();
      var expectedBuilder = new StringBuilder();
      for (int i = 0; i < 100; i++)
      {
        srcBuilder.Append("а б в г д е ё ж з и й к л м н о п р с т у ф х ц ч ш щ ъ ы ь э ю я А Б В Г Д Е Ё Ж З И Й К Л М Н О П Р С Т У Ф Х Ц Ч Ш Щ Ъ Ы Ь Э Ю Я");
        expectedBuilder.Append("a b v g d e io zh z i i k l m n o p r s t u f kh ts ch sh shch ' y ' e iu ia A B V G D E Io Zh Z I I K L M N O P R S T U F Kh Ts Ch Sh Shch ' Y ' E Iu Ia");
      }
      var src = srcBuilder.ToString();
      var expected = expectedBuilder.ToString();
      var result = src.Unidecode();
      Assert.Equal(expected, result);

    }
  }
}
