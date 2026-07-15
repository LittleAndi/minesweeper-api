# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

A Minesweeper game exposed as a REST API. ASP.NET Core minimal API on .NET 10 (`net10.0`). Still in early development — the README describes the intended API surface (game creation, square reveal, stats), but only game creation and square reveal are implemented so far.

## Commands

```powershell
dotnet build                 # build the solution (minesweeper-api.sln at repo root)
dotnet test                  # run all tests
dotnet run --project Source/Host   # run the API locally
```

Run a single test or test class (xUnit, filtered by fully qualified name):

```powershell
dotnet test --filter "FullyQualifiedName~BoardTests"
dotnet test --filter "FullyQualifiedName~BoardTests.ShouldRevealSquare"
```

Tests are also tagged `[Trait("Category", "L0")]`, so `dotnet test --filter "Category=L0"` runs the level-0 suite. CI (`.github/workflows/dotnet.yml`) runs restore/build/test on pushes to `main` and `feature/*`, `fix/*`, `refactor/*` branches.

## Architecture

Two projects plus mirrored test projects, all under `Source/`:

- **`Application`** (`MineSweeper.Application`) — all game logic, no web dependencies.
  - `Model/Board.cs` is the core: mine and revealed state live in two separate `bool[,]` grids, mines are placed at construction (dimensions and mine count validated there), and `RevealSquare` computes adjacent-mine counts. `MineCount`/`RevealedCount` are computed by scanning the grids on each access; win detection in `GameService.RevealSquare` relies on that, and `RevealedCount` deliberately excludes revealed mines.
  - `GameService` keeps all games in an in-memory `ConcurrentDictionary<Guid, Game>` (singleton, no persistence — restart loses all games). Reveal + win/loss transition happen atomically under a per-game lock (`Game.SyncRoot`); revealing on a finished game throws `InvalidOperationException`.
  - Failures use typed exceptions (`GameNotFoundException`, `SquareOutOfBoundsException` in `Exceptions.cs`) that the Host maps to status codes.
  - `GameCreationService` constructs boards; `DependencyInjection.AddApplication()` registers everything as singletons.
  - Randomness is abstracted behind `IRandomGenerator` specifically so tests can inject deterministic mine placement.
- **`Host`** — thin ASP.NET Core layer. `Program.cs` (top-level statements) → `HostExtensions.ConfigureBuilder/ConfigureApp` (Serilog + DI wiring) → `Endpoints.MapEndpoints`, which defines the minimal-API routes under `/api` (`POST /api/game`, `PUT /api/game/{gameId}/{x},{y}`) and maps exceptions to responses (not-found/out-of-bounds → 404, finished game or invalid parameters → 400). `GameMapper` converts domain objects to DTOs, including the `GameStatus` → camelCase string mapping. DTOs live in `Endpoints.cs`.

Tests follow a `Test.Level0.<Project>` naming scheme (xUnit + Shouldly). Deterministic board tests use `NonRandomMinePlacementStrategy` in `Tests/Test.Level0.Application/Common.cs` — an `IRandomGenerator` that replays a fixed coordinate sequence to lay mines on a known diagonal-plus-cluster layout (documented in an ASCII diagram in that file). If you change mine placement logic, that fixture and the `BoardTests.ShouldRevealSquare` inline data encode the expected board. `Test.Level0.Host` also has end-to-end endpoint tests via `WebApplicationFactory` (enabled by `public partial class Program` at the bottom of `Program.cs`), which swap in a stateful row-major mine placer — each test builds its own factory so placement stays predictable.

Each project uses a `GlobalUsings.cs` for namespace imports; add new global usings there rather than per-file.

## Conventions

- Coordinates are `(x, y)` with `x` as the first array dimension; board arrays are `SquareState[x, y]`. Watch the dimension order — `GetBoardLayout` and the count loops iterate both dimensions explicitly.
- Namespaces don't match folder/assembly names everywhere (e.g. `HostExtensions` sits in namespace `Host.Exception`); follow the existing file's namespace when editing.
