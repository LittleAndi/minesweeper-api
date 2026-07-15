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
  - `Model/Board.cs` is the core: holds a `SquareState[,]` grid (Unknown/Revealed/Mine), places mines at construction, computes adjacent-mine counts and reveal state. `MineCount`/`RevealedCount` are computed by scanning the grid on each access, and win detection in `GameService.RevealSquare` relies on that.
  - `GameService` keeps all games in an in-memory `Dictionary<Guid, Game>` (singleton, no persistence — restart loses all games). It sets `GameStatus` (InProgress/Won/Lost) after each reveal.
  - `GameCreationService` constructs boards; `DependencyInjection.AddApplication()` registers everything as singletons.
  - Randomness is abstracted behind `IRandomGenerator` specifically so tests can inject deterministic mine placement.
- **`Host`** — thin ASP.NET Core layer. `Program.cs` (top-level statements) → `HostExtensions.ConfigureBuilder/ConfigureApp` (Serilog + DI wiring) → `Endpoints.MapEndpoints`, which defines the minimal-API routes under `/api` (`POST /api/game`, `PUT /api/game/{gameId}/{x},{y}`). `GameMapper` converts domain `Game` to `GameDto`. DTOs live in `Endpoints.cs`.

Tests follow a `Test.Level0.<Project>` naming scheme (xUnit + Shouldly). Deterministic board tests use `NonRandomMinePlacementStrategy` in `Tests/Test.Level0.Application/Common.cs` — an `IRandomGenerator` that replays a fixed coordinate sequence to lay mines on a known diagonal-plus-cluster layout (documented in an ASCII diagram in that file). If you change mine placement logic, that fixture and the `BoardTests.ShouldRevealSquare` inline data encode the expected board.

Each project uses a `GlobalUsings.cs` for namespace imports; add new global usings there rather than per-file.

## Conventions

- Coordinates are `(x, y)` with `x` as the first array dimension; board arrays are `SquareState[x, y]`. Watch the dimension order — `GetBoardLayout` and the count loops iterate both dimensions explicitly.
- Namespaces don't match folder/assembly names everywhere (e.g. `HostExtensions` sits in namespace `Host.Exception`); follow the existing file's namespace when editing.
