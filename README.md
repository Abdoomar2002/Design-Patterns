# Design Patterns

This repository contains C# examples demonstrating various design patterns. Each pattern is organized as a separate .NET project targeting **net9.0**.

## Builder Patterns

Projects under `Builder/` showcase different builder implementations:

- **Builder** – basic HTML builder example.
- **Faceted Builder** – demonstrates building an object through multiple facets (e.g., job and address).
- **Fluent Builder** – uses a fluent API and generics for building a `Person` object.
- **Functional Builder** – builds objects by composing functions.
- **Stepwise Builder** – enforces a step-by-step creation process, illustrated with a `Car` example.

## Factory Patterns

Projects under `Factory/` provide factory pattern examples:

- **Factory** – contains a simple point factory with asynchronous creation.
- **Abstract Factory** – demonstrates an abstract factory for preparing hot drinks.

## Running the Examples

Each pattern folder includes a `.csproj` and `.sln` file. You can run a specific example with the .NET CLI:

```bash
cd <pattern-folder>/<project-folder>
dotnet run
```

Alternatively, open the solution file (`.sln`) in Visual Studio.

