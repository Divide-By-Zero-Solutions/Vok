# Testing strategy

## Test layers

| Layer | Project or location | Purpose |
| --- | --- | --- |
| Unit and service behavior | `Vok.Tests` | Validate domain-facing contracts and infrastructure behavior with xUnit and Moq. |
| Persistence integration | `Vok.Tests` and performance integration benchmarks | Validate SQLite schema, reads, writes, and cleanup behavior. |
| UI smoke coverage | MAUI build and manual workflow | Verify composition, routes, and platform packaging. |
| Performance regression | `performance/Vok.Performance` | Measure portable cache and text operations. |
| Performance integration | `performance/Vok.Performance.Integration` | Measure SQLite-backed operations and report allocation/time ceilings. |

## Test conventions

Tests should isolate state, use deterministic inputs, and assert observable outcomes. Temporary SQLite files must be created outside the repository and deleted during cleanup. Network and provider integrations should use fakes or mocks in ordinary tests; benchmark scenarios must document when they intentionally exercise local or provider-like implementations.

## Coverage and CI

`Vok.Tests/coverage.runsettings` configures coverage collection. CI builds the solution, runs tests, publishes coverage artifacts, validates documentation, and runs performance reporting only through the configured workflow paths. Benchmark results are machine-specific and are used for regression tracking rather than cross-machine comparison.
