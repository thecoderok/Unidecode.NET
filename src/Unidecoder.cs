using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Unidecode.NET
{
  /// <summary>
  /// ASCII transliterations of Unicode text
  /// </summary>
  public static partial class Unidecoder
  {
    // for short strings I use a buffer allocated in the stack instead of a stringbuilder (this should give less work to the garbage collector
    private const int STACKALLOC_BUFFER_SIZE = 40956;

    /// <summary>
    /// Transliterate Unicode string to ASCII string.
    /// </summary>
    /// <param name="input">String you want to transliterate into ASCII</param>
    /// <param name="tempStringBuilderCapacity">
    ///     If you know the length of the result,
    ///     pass the value for StringBuilder capacity.
    ///     InputString.Length*2 is used by default.
    /// </param>
    /// <returns>
    ///     ASCII string. There are [?] (3 characters) in places of some unknown(?) unicode characters.
    ///     It is this way in Python code as well.
    /// </returns>
    public static string Unidecode(this string input, int? tempStringBuilderCapacity = null)
    {
      if (string.IsNullOrEmpty(input))
        return "";

      if (input.All(x => x < 0x80))
        return input;

     /* if (input.Length < MaxStringLengthForStackAlloc)
      {
        Span<char> stackBuffer = stackalloc char[STACKALLOC_BUFFER_SIZE];
        int buffIdx = 0;
        foreach (char c in input)
        {
          if (c < 0x80)
          {
            stackBuffer[buffIdx++] = c;
            continue;
          }
          var high = c >> 8;
          if (high < characters.Length)
            continue;
          var bytes = characters[high];
          if (bytes == null)
            continue;
          var str = bytes[c & 0xff];
          foreach (char ch in str)
             stackBuffer[buffIdx++] = ch;
        }

        return new string(stackBuffer[0..buffIdx]);
      }*/


      // Unidecode result often can be at least two times longer than input string.
      var sb = new StringBuilder(tempStringBuilderCapacity ?? input.Length * 2);
      foreach (var c in input)
      {
        // Copypaste is bad, but sb.Append(c.Unidecode()); would be a bit slower.
        if (c < 0x80)
        {
          sb.Append(c);
        }
        else
        {
          var high = c >> 8;
          if (high >= characters.Length)
            continue;
          var low = c & 0xff;
          var bytes = characters[high];
          if (bytes!=null)
          {
            sb.Append(bytes[low]);
          }
        }
      }

      return sb.ToString();
    }

    /// <summary>
    /// Transliterate Unicode character to ASCII string.
    /// </summary>
    /// <param name="c">Character you want to transliterate into ASCII</param>
    /// <returns>
    ///     ASCII string. Unknown(?) unicode characters will return [?] (3 characters).
    ///     It is this way in Python code as well.
    /// </returns>
    public static string Unidecode(this char c)
    {
      if (c < 0x80)
        return AsciiCharacter.AsString[c];

      var high = c >> 8;
      if (high >= characters.Length)
        return null;
      var bytes = characters[high];
      if (bytes == null)
        return null;
                
      return bytes[c & 0xff];
    }

    private static class AsciiCharacter
    {
      public static readonly string[] AsString;
      static AsciiCharacter()
      {
        AsString = new string[0x80];
        for (char ch = '\0'; ch < AsString.Length; ch++)
          AsString[ch] = new string(ch,1);
      }
      
    }

  }
}
