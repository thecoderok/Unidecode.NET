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
  public void UnidecodeRussianTest()
  {
    var converted = "Работа с кириллицей".Unidecode();
  }
}
