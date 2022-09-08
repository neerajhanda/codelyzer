``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.20348.887)
Intel Xeon Platinum 8375C CPU 2.90GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.400
  [Host]     : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2
  Job-DMOYNL : .NET 6.0.8 (6.0.822.36306), X64 RyuJIT AVX2

LaunchCount=1  RunStrategy=Monitoring  

```
|                   Method |    Mean |   Error |  StdDev | Ratio | RatioSD |        Gen0 |       Gen1 |      Gen2 | Allocated | Alloc Ratio |
|------------------------- |--------:|--------:|--------:|------:|--------:|------------:|-----------:|----------:|----------:|------------:|
|          AnalyzeSolution | 39.26 s | 0.567 s | 0.375 s |  1.00 |    0.00 | 175000.0000 | 82000.0000 | 6000.0000 |   4.88 GB |        1.00 |
| AnalyzeSolutionGenerator | 62.38 s | 0.619 s | 0.409 s |  1.59 |    0.02 | 169000.0000 | 77000.0000 | 1000.0000 |   4.85 GB |        0.99 |
