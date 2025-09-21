# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unidecode.NET is a .NET library that provides ASCII transliterations of Unicode text. The library converts Unicode strings to ASCII representations, useful for URL slugs, legacy system integration, and text processing where ASCII-only output is required.

## Architecture

### Core Components

- **Unidecoder.cs** (`src/Unidecoder.cs`): Main static class containing the transliteration logic
  - Supports two algorithms: `Fast` (optimized but limited to codepoints ≤65535) and `Complete` (proper handling of all Unicode codepoints including Chinese/Japanese)
  - Uses embedded resource file `unidecoder-decodemap.txt` for character mappings
  - Provides extension methods for string transliteration
  - Configurable algorithm selection via static `Algorithm` property

### Project Structure

- `src/`: Main library project (Unidecode.NET.csproj)
- `test/`: Unit tests using xUnit framework
- `benchmark/`: Performance benchmarking project
- `assets/`: Contains the Unicode-to-ASCII mapping data file

## Development Commands

### Building
```bash
dotnet build -c Release
```

### Running Tests
```bash
dotnet test -c Release --no-build
```

### Running a Single Test
```bash
dotnet test -c Release --filter "TestMethodName"
```

### Packaging
```bash
dotnet pack -c Release
```

## Framework and Dependencies

- Target Framework: .NET 7.0
- Test Framework: xUnit 2.4.1
- No external runtime dependencies for the main library
- Uses embedded resources for character mapping data

## Code Style

- Follows .editorconfig settings: UTF-8, LF line endings, 2-space indentation, max line length 160
- XML documentation enabled for public APIs
- Unsafe code blocks allowed for performance optimization

## Testing

Tests are located in `test/` directory:
- `UnidecoderTest.cs`: Basic functionality tests
- `CompleteUnidecoderTest.cs`: Comprehensive Unicode testing

The CI/CD pipeline (GitHub Actions) automatically runs build and tests on push.