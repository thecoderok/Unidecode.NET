using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace Unidecode.NET.Benchmark;
[MemoryDiagnoser]
public class Benchmarks
{

  [Benchmark]
  public void UnidecodeRussian()
  {
    var converted = "Работа с кириллицей".Unidecode();
  }

  [Benchmark]
  public void UnidecodeAscii()
  {
    var converted = "Hello World!".Unidecode();
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

}
