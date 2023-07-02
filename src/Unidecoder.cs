using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

// this IntenralsVisibleTo attribute is here to allow benchmarking and
// testing of SlowUnidecode, which normally, due to the stackalloc optimization,
// is called only when Unidecode receives a long string
[assembly: InternalsVisibleTo("Unidecode.Net.Benchmark")]
[assembly: InternalsVisibleTo("Unidecode.Net.Tests")]

namespace Unidecode.NET
{   
  /// <summary>
  /// ASCII transliterations of Unicode text
  /// </summary>
  public static partial class Unidecoder
  {
    // for short strings I use a buffer allocated in the stack instead of a stringbuilder.
    // (this is faster and gives less work to the garbage collector)
    private const int STACKALLOC_BUFFER_SIZE = 8192;

    [SkipLocalsInit] // this is to avoid the local raw buffer variable stackBuffer do be zeroed for every call: we don't need it and is very cpu intensive (this attribute needs unsafe compliation)
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

      if (input.Length >= MaxStringLengthForStackAlloc)
        return SlowUnidecode(input, tempStringBuilderCapacity);

      bool noConversionNeeded = true;
      Span<char> stackBuffer = stackalloc char[STACKALLOC_BUFFER_SIZE];
      int buffIdx = 0;
      foreach (char c in input)
      {
        if (c < 0x80)
        {
          stackBuffer[buffIdx++] = c;
          continue;
        }
        noConversionNeeded = false;
        var high = c >> 8;
        if (high >= characters.Length)
          continue;
        var bytes = characters[high];
        if (bytes == null)
          continue;
        var str = bytes[c & 0xff];

        foreach (char ch in str)
          stackBuffer[buffIdx++] = ch;
      }
      if (noConversionNeeded)
        return input;
      return new string(stackBuffer[0..buffIdx]);
    }

    // this implementation is slower but it has no limits for the lenght of the input stirng. it gets called by Unidecode() for long strings
    internal static string SlowUnidecode(this string input, int? tempStringBuilderCapacity = null)
    {
      if (string.IsNullOrEmpty(input))
        return "";

      if (input.All(x => x < 0x80))
        return input;

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
          if (bytes != null)
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
    public static string Unidecode(this in char c)
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

    // I keep a precalculated cache of all the single character strings for ascii characters, so the Unidecode character extension method
    // does not instantiate a new string every time it has to return a single character string for a character <0x80
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

    /// <summary>
    /// this function helps you translate a set of indexes you have found in a decoded stirng to the corresponding indexes
    /// in the original string, <br/>
    /// for example Süßigkeit gets decoded to Sussigkeit, so the first 'i' character has index 4 in the decoded string, and 3 in the source string.
    /// With the avove example Unidecode.GetIndexesInSourceString("Süßigkeit", new int[] {4}) will return {3}
    /// Warning: this implementation assumes that the input IEnumerable is sorted!
    /// </summary>
    public static IEnumerable<int> FindIndexesInSourceString(string sourceString, IEnumerable<int> indexesInDecodedString)
    {
      if (string.IsNullOrEmpty(sourceString))
        yield break;
      if (indexesInDecodedString == null)
        yield break;

      using var indexesEnumerator = indexesInDecodedString.GetEnumerator();
      if (!indexesEnumerator.MoveNext())
        yield break;
      var currIndex = indexesEnumerator.Current;
      if (currIndex < 0)
        throw new ArgumentException("indexes can't be negative values", nameof(indexesInDecodedString));

      int decodedIdx = 0;
      for (int srcIdx=0; srcIdx<sourceString.Length; srcIdx++)
      {
        if (decodedIdx >= currIndex)
        {
          yield return srcIdx;
          var prevIndex = currIndex;
          if (!indexesEnumerator.MoveNext()) 
            yield break; // we decoded all the indexes
          currIndex = indexesEnumerator.Current;
          if (currIndex < 0)
            throw new ArgumentException("indexes can't be negative values", nameof(indexesInDecodedString));
          if (currIndex <= prevIndex)
            throw new ArgumentException("Input sequence of indexes must be strictly increasing", nameof(indexesInDecodedString));
        }

        var ch = sourceString[srcIdx];

        var decodedCh = ch.Unidecode();
        if (decodedCh != null)
          decodedIdx += decodedCh.Length;
      }

    }

    

  }
}
