# Blazor localized form validation

Validation and localization sample for Blazor, targeting .NET 11 RC1.

This repository validates the localized form-message scenario from
[dotnet/aspnetcore#69524](https://github.com/dotnet/aspnetcore/issues/69524).

Validated locally on Windows using the SDK pinned by `global.json`:

```text
.NET SDK 11.0.100-rc.1.26425.128
```

The builds may display `NETSDK1057`, which is the expected preview SDK notice.

## Tested revision and environment

- Tested commit: `45989620f0ccfe9734832f045ce61fd53eeaa8c8`
- .NET SDK: `11.0.100-rc.1.26425.128`
- .NET host runtime: `11.0.0-rc.1.26425.128`
- MSBuild: `18.11.0-1.26425.128+3551975be`
- OS: Windows 11 x64
- OS version: `10.0.26200`
- RID: `win-x64`
- Browser: Google Chrome 151
- IDE: Visual Studio 2022 17.10.2

## Projects in scope

| Project | Render mode | Validation |
|---|---:|---|
| `BlazorSSRSample/BlazorSSRSample` | Static SSR | Required issue scenario using the shared `[ValidatableType] ContactModel` |
| `BlazorWasmSample/BlazorWasmSample.Client` | Interactive WebAssembly | Required issue scenario using the same shared-source `ContactModel` and matching localized resources |
| `SharedValidation/SharedValidation` | Shared library | Shared display-name resources and shared-source validation model inputs used by the required modes |

`BlazorWasmSample/BlazorWasmSample` still hosts an Interactive Server page for
exploratory validation-lab scenarios, but that page is outside the required
issue evidence.

## How to run

From the repository root, run the required samples with the HTTP launch profile:

```powershell
dotnet run --project .\BlazorSSRSample\BlazorSSRSample\BlazorSSRSample.csproj --launch-profile http
dotnet run --project .\BlazorWasmSample\BlazorWasmSample\BlazorWasmSample.csproj --launch-profile http
```

Open:

- Static SSR:
  - `http://localhost:5127/validation?culture=fr-FR&ui-culture=fr-FR`
  - `http://localhost:5127/validation?culture=de-DE&ui-culture=de-DE`
- Interactive WebAssembly:
  - `http://localhost:5018/client-validation?culture=fr-FR&ui-culture=fr-FR`
  - `http://localhost:5018/client-validation?culture=de-DE&ui-culture=de-DE`

If older app instances are running on different ports, stop them first so the
validation is performed against the current build.

## How to verify

Build and test the repository state:

```powershell
dotnet test .\BlazorWasmSample\BlazorWasmSample\BlazorWasmSample.Tests\BlazorWasmSample.Tests.csproj --filter "(FullyQualifiedName~SharedContactLocalizationTests|FullyQualifiedName~FallbackRequiredAttributeTests)" -nologo -v:minimal
dotnet build .\BlazorSSRSample\BlazorSSRSample\BlazorSSRSample.csproj --no-restore -nologo -v:minimal
dotnet build .\BlazorWasmSample\BlazorWasmSample\BlazorWasmSample.csproj --no-restore -nologo -v:minimal
```

## Completed validation coverage

The completed validation covers the following required scenarios:

- Static SSR and Interactive WebAssembly both use the same shared
  `[ValidatableType] ContactModel`.
- `Name` uses a conventional `[Required]` lookup with no explicit error
  message.
- `Email` uses `[Required(ErrorMessage = "EmailRequired")]`.
- Language A (`fr-FR`) shows localized conventional and explicit messages in
  both required modes:
  - `FR convention membre : saisissez le nom.`
  - `FR clé explicite : saisissez l’e-mail.`
- Language B (`de-DE`) omits the conventional `Name` key, so `Name` falls back
  to a readable built-in required message while `Email` remains translated:
  - `The Name field is required.`
  - `DE expliziter Schlüssel: Geben Sie die E-Mail ein.`
- Static SSR and Interactive WebAssembly display matching `Name` and `Email`
  messages for both tested cultures.
- Correcting `Name` clears the `Name` validation message in both required modes.
- Correcting `Email` clears the `Email` validation message in both required
  modes.
- The visible UI culture indicator matches the selected culture on each run.

The exploratory Interactive Server validation lab and the older missing-resource
field scenario are retained only as extra product exploration and are not used
as evidence for the required issue verdict.

## Tested configurations

- Windows 11 x64
- Windows version `10.0.26200`
- .NET SDK `11.0.100-rc.1.26425.128`
- .NET host runtime `11.0.0-rc.1.26425.128`
- Google Chrome 151
- Visual Studio 2022 17.10.2
- Static SSR
- Interactive WebAssembly
- French (`fr-FR`) and German (`de-DE`) UI cultures

## Evidence

- Manual validation steps and expected outcomes are documented in the `Evidence/` folder.
- Canonical validation report:
  `Evidence/69524-Localized-Form-Messages-Validation-Report.docx`
- Supporting screenshots and videos:
  `Evidence/`
- Report metadata wording and exact collected environment details:
  [REPORT-FOLLOWUP-COMMANDS.md](D:/BlazorLocalizedFormValidation/Evidence/REPORT-FOLLOWUP-COMMANDS.md)


## Current validation status

The overall result is **passed**. All requested validation scenarios and
requirements were executed and verified in the required Static SSR and
Interactive WebAssembly configurations. No requested validation is pending.

### Problems Found

None. No functional, rendering, behavioral, accessibility-related, or
validation-blocking issues were identified in the required scenario.

### Not Covered

None within the requested validation scope. All explicitly requested test cases,
configurations, and mandatory validation scenarios were completed.

## Public references

- [Validation in ASP.NET Core](https://learn.microsoft.com/aspnet/core/fundamentals/validation?view=aspnetcore-11.0)
- [Blazor forms validation](https://learn.microsoft.com/aspnet/core/blazor/forms/validation?view=aspnetcore-11.0)
- [Blazor client-side form validation in static SSR](https://learn.microsoft.com/aspnet/core/blazor/forms/validation-client-side?view=aspnetcore-11.0)
- [Blazor globalization and localization](https://learn.microsoft.com/aspnet/core/blazor/globalization-localization?view=aspnetcore-11.0)
