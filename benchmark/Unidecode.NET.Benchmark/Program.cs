using BenchmarkDotNet.Running;

namespace Unidecode.NET.Benchmark;

internal class Program
{
  static void Main(string[] args)
  {
    var summary = BenchmarkRunner.Run<Benchmarks>();
  }
}
