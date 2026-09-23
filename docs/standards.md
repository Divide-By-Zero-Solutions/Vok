# Coding and documentation standards

## C#

- Target the framework declared by each project; do not introduce Xamarin.Forms APIs into the .NET MAUI application.
- Use descriptive PascalCase names for types and members and clear camelCase names for locals and parameters.
- Prefer dependency injection through interfaces from `Vok.Domain`.
- Keep infrastructure concerns out of domain models.
- Use asynchronous APIs for I/O and propagate `CancellationToken` when the existing contract supports it.
- Dispose owned streams, HTTP responses, timers, and database connections according to the owning API's lifetime rules.
- Preserve nullability annotations and validate external input at service boundaries.
- Keep platform-specific behavior isolated to MAUI or infrastructure adapters.
- Use `PerfCritcalAttribute` and `PerfIgnoreAttribute` for benchmark filtering where applicable. Both attributes support method and class usage.

## XML documentation

- Every public and internal type, constructor, method, property, field, event, and enum member must have an XML documentation comment when it is part of maintained source.
- Use `<summary>` for behavior and responsibility, `<param>` for inputs, `<returns>` for values, and `<exception>` for documented exceptional conditions.
- Describe observable behavior and side effects rather than restating the member name.
- Use `<see cref="..."/>` for related symbols and `<remarks>` for lifecycle, persistence, threading, or platform constraints.
- Generated code, designer files, and framework overrides that cannot carry useful documentation are excluded.

## Markdown

- Each project has a README that explains purpose, boundaries, dependencies, entry points, testing, and links to the canonical docs.
- Mermaid diagrams use fenced `mermaid` blocks and must have a short explanation immediately before or after the diagram.
- Use relative links and verify that links resolve from the file containing them.
- Record architecture decisions and behavior discovered in code; do not document aspirational behavior as implemented behavior.

## Testing and review

- Add or update tests when behavior changes.
- Name tests using `Subject_Scenario_ExpectedOutcome` where practical.
- Keep tests deterministic and isolate filesystem/database state in temporary locations.
- Review documentation changes with the same care as code changes, including links, diagrams, examples, and generated API output.
