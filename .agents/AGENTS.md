# Greenshot Workspace Rules & Behavioral Guidelines

This workspace targets a custom, modern build of Greenshot on .NET Framework 4.8 (`net480` branch).

## Custom Build and Publishing Parameters

### 🔒 Corporate Network Firewall Bypass
Due to a corporate firewall block on the `Tools.InnoSetup` NuGet package package-restore pipeline:
- **Rule**: Never run standard `dotnet restore` or standard `msbuild /t:Restore` in `Release` configuration unless explicitly requested to build the installer.
- **Bypass Parameter**: Always append `/p:SkipInstallerRestore=true` to all restore and compile commands to bypass the block.

### 🚀 Compilation Commands
* **To compile a standalone Release executable**:
  ```powershell
  dotnet restore src\Greenshot.sln /p:Configuration=Release /p:SkipInstallerRestore=true
  & "C:\Program Files\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin\MSBuild.exe" src\Greenshot.sln /p:Configuration=Release /t:Greenshot /p:SkipInstallerRestore=true
  ```
  This places the compiled `Greenshot.exe` in `src\Greenshot\bin\Release\net480\`.

* **To build the full installer (requires personal/home network)**:
  ```powershell
  & "C:\Program Files\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin\MSBuild.exe" src\Greenshot.sln /p:Configuration=Release /t:Restore,Rebuild
  ```
  This creates the `.exe` installer inside `installer/`.

### 🚢 Automated Publishing Script
A complete, automated script is located in the repository root: `.\build-and-deploy.ps1`. When run, it builds the release, packages the portable version, generates a Git version tag, pushes the tag, and creates a GitHub Release with the installer and ZIP files attached.
