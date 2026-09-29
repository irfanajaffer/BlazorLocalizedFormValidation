# Blazor localized form validation (.NET 11)

Validation and localization sample for Blazor, targeting .NET 11 RC1 and later.

This repository demonstrates how to localize validation messages, display names,
and client-side validation metadata for Blazor in several render modes.

## Build

Validated locally on Windows using the SDK pinned by `global.json`:

```text
.NET SDK 11.0.100-rc.1.26425.128
```

Build from the repository root:

```powershell
dotnet restore .\BlazorLocalizedFormValidation.slnx
dotnet build .\BlazorLocalizedFormValidation.slnx --no-restore -nologo -v:minimal
```

The builds may display `NETSDK1057`, an informational notice that a preview
.NET SDK is in use. Successful builds should produce no errors.

## Sample applications

| Project | Render mode | Purpose |
|---|---:|---|
| `BlazorSSRSample/BlazorSSRSample` | Static SSR | Demonstrates static server-side rendering with localized client validation metadata and a client-rule matrix |
| `BlazorWasmSample/BlazorWasmSample` | Interactive WebAssembly | Shows WebAssembly client with metadata generated from a `.Client` assembly |
| `BlazorWasmSample/BlazorWasmSample.Server` | Interactive Server (host) | Host for the WASM sample and interactive Server scenario |

(Adjust paths above if your workspace layout differs.)

## How to run

From the repository root, run the sample you want to inspect.

Static SSR example:

```powershell
dotnet run --project .\BlazorSSRSample\BlazorSSRSample\BlazorSSRSample.csproj --launch-profile http
```

Interactive Server / WebAssembly host:

```powershell
dotnet run --project .\BlazorWasmSample\BlazorWasmSample\BlazorWasmSample.csproj --launch-profile http
```

When the app starts, open the URLs shown by `dotnet run` and navigate to the
validation pages. Example paths used during local validation:

- Static SSR localization and client-rule matrix:
  `http://localhost:5127/validation?culture=fr-FR&ui-culture=fr-FR`
  `http://localhost:5127/client-rules?culture=fr-FR&ui-culture=fr-FR`
- Interactive Server / WASM host validation pages:
  `http://localhost:<port>/server-validation?culture=fr-FR&ui-culture=fr-FR`
  `http://localhost:<port>/client-validation?culture=fr-FR&ui-culture=fr-FR`

## How to verify

Build the projects and confirm successful output with minimal verbosity:

```powershell
dotnet build .\BlazorSSRSample\BlazorSSRSample\BlazorSSRSample.csproj --no-restore -nologo -v:minimal
dotnet build .\BlazorWasmSample\BlazorWasmSample.slnx --no-restore -nologo -v:minimal
```

Manual verification checklist (covered by this sample):

- Page renders expected panels, forms, and localized messages.
- Localized display names and attribute messages are shown for fr-FR and de-DE.
- Client-side validation metadata is present in static SSR and preserved for
  interactive WASM after prerendering.
- Nested objects and collection-item validation messages render correctly.
- Missing resource key fallbacks behave as documented.

Detailed manual steps, inputs, and expected outputs are documented in
[docs/manual-validation.md](docs/manual-validation.md).

## Configuration

Prerequisites and notable configuration:

- .NET SDK `11.0.100-rc.1.26425.128` (see `global.json`)
- Browser with JavaScript enabled for client-side and interactive scenarios
- The samples reference `Microsoft.AspNetCore.Components.WebAssembly` and
  `Microsoft.Extensions.Localization` in the client projects

To test the app-wide static client-validation opt-out, set
`Validation:DisableClientValidation` to `true` in
`BlazorSSRSample/BlazorSSRSample/appsettings.json`, restart the app, and use
the client-rule matrix. Restore the value to `false` after the test.

## Evidence

- Manual validation steps and expected outcomes: `docs/manual-validation.md`
- Static client-rule matrix pages and interactive validation pages are useful
  for quick checks and screenshots.

## Current validation status

The overall result is **undetermined** in this repository snapshot. The
sample harness includes manual verification steps in `docs/manual-validation.md`.
Run the builds and follow the manual checklist to confirm the current state.

### Problems Found

None recorded here. If manual validation reveals issues, file an issue in the
repository with steps to reproduce.

### Not Covered

- MAUI Hybrid and standalone server-less automation were outside the original
  validation scope and are not covered by these samples.

## Public references

- [Validation in ASP.NET Core](https://learn.microsoft.com/aspnet/core/fundamentals/validation?view=aspnetcore-11.0)
- [Blazor forms validation](https://learn.microsoft.com/aspnet/core/blazor/forms/validation?view=aspnetcore-11.0)
- [Blazor client-side form validation in static SSR](https://learn.microsoft.com/aspnet/core/blazor/forms/validation-client-side?view=aspnetcore-11.0)
- [Blazor globalization and localization](https://learn.microsoft.com/aspnet/core/blazor/globalization-localization?view=aspnetcore-11.0)
