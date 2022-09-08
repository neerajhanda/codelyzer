``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.20348.887)
Intel Xeon Platinum 8375C CPU 2.90GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2
  Job-WHKMPK : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2

LaunchCount=1  RunStrategy=Monitoring  

```
|                   Method |     Mean |   Error |  StdDev | Ratio | RatioSD |        Gen0 |       Gen1 |      Gen2 | Allocated | Alloc Ratio |
|------------------------- |---------:|--------:|--------:|------:|--------:|------------:|-----------:|----------:|----------:|------------:|
|          AnalyzeSolution |  39.95 s | 0.409 s | 0.270 s |  1.00 |    0.00 | 171000.0000 | 75000.0000 | 2000.0000 |   4.88 GB |        1.00 |
| AnalyzeSolutionGenerator | 141.83 s | 3.001 s | 1.985 s |  3.55 |    0.05 | 202000.0000 | 89000.0000 | 1000.0000 |   5.73 GB |        1.17 |
