# Performance engineering

The performance submodule contains reproducible BenchmarkDotNet projects and is versioned independently from the application repository.

## Benchmark groups

- Portable benchmarks cover prediction-cache access and phrase normalization.
- Integration benchmarks cover SQLite vocabulary reads and writes.
- `PerfCritcalAttribute` and `PerfIgnoreAttribute` identify benchmark participation and support both class-level and method-level usage.

## Workflow

1. Build the application and performance projects with the supported SDKs.
2. Run the portable benchmarks before provider or platform-dependent scenarios.
3. Run integration benchmarks with isolated temporary data.
4. Compare time and allocation results only against the same runner class.
5. Investigate regressions before changing a target ceiling.

The generated report is maintained by `scripts/Update-PerformanceReport.ps1` and is intentionally treated as machine-specific evidence, not a portable SLA.
