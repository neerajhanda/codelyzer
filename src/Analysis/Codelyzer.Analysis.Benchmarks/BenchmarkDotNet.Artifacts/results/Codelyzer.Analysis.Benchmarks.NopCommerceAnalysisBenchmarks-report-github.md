``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.20348.887)
Intel Xeon Platinum 8375C CPU 2.90GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2
  Job-KRMMYA : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2

LaunchCount=1  RunStrategy=Monitoring  

```
|                   Method |    Mean |   Error |  StdDev | Ratio | RatioSD |        Gen0 |       Gen1 |      Gen2 | Allocated | Alloc Ratio |
|------------------------- |--------:|--------:|--------:|------:|--------:|------------:|-----------:|----------:|----------:|------------:|
|          AnalyzeSolution | 40.64 s | 1.380 s | 0.913 s |  1.00 |    0.00 | 175000.0000 | 81000.0000 | 6000.0000 |   4.88 GB |        1.00 |
| AnalyzeSolutionGenerator | 62.22 s | 1.295 s | 0.857 s |  1.53 |    0.03 | 169000.0000 | 76000.0000 | 1000.0000 |   4.85 GB |        0.99 |
