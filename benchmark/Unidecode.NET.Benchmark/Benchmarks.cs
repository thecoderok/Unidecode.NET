using System.Text;
using BenchmarkDotNet.Attributes;

namespace Unidecode.NET.Benchmark;
[MemoryDiagnoser]
public class Benchmarks
{

  [Benchmark]
  public void FastUnidecodeRussian()
  {
    var converted = "Работа с кириллицей".Unidecode(UnidecodeAlgorithm.Fast);
  }

  [Benchmark]
  public void CompleteUnidecodeRussian()
  {
    var converted = "Работа с кириллицей".Unidecode(UnidecodeAlgorithm.Complete);
  }

  [Benchmark]
  public void FastUnidecodeAscii()
  {
    var converted = "Hello World!".Unidecode(UnidecodeAlgorithm.Fast);
  }
  
  [Benchmark]
  public void CompleteUnidecodeAscii()
  {
    var converted = "Hello World!".Unidecode(UnidecodeAlgorithm.Complete);
  }

  [Benchmark]
  public void UnidecodeRussianChar()
  {
    var converted = 'и'.Unidecode();
  }
  
  [Benchmark]
  public void UnidecodeAsciiChar()
  {
    var converted = 'Z'.Unidecode();
  }


  private readonly static Rune russianRune = new('и');
  [Benchmark]
  public void UnidecodeRussianRune()
  {

    var converted = russianRune.Unidecode();
  }

  private readonly static Rune AsciiRune = new('Z');

  [Benchmark]
  public void UnidecodeAsciiRune()
  {
    var converted = AsciiRune.Unidecode();
  }


}
