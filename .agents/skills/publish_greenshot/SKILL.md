---
name: publish_greenshot
description: Guidelines, commands, and scripts to compile, build installers, and publish/deploy Greenshot
---

# How to Compile, Build, and Publish Greenshot

Whenever the user asks "How do I publish this application?", "How do I build the installer?", or "How do I compile Greenshot?", use the instructions below to guide them.

---

## 🚀 1. Compiling the Release Executable (Corporate/Ramsay Network Bypass)
Because of the Ramsay corporate firewall block on `Tools.InnoSetup`, you can compile the entire standalone Greenshot application in **Release** mode by bypassing the installer setup.

### Step 1: Restore Release dependencies (bypassing the Ramsay block)
Run this from your repository root:
```powershell
dotnet restore src\Greenshot.sln /p:Configuration=Release /p:SkipInstallerRestore=true
```

### Step 2: Build the Release Executable
Run this MSBuild command to compile:
```powershell
& "C:\Program Files\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin\MSBuild.exe" src\Greenshot.sln /p:Configuration=Release /t:Greenshot /p:SkipInstallerRestore=true
```
* **Output Location**: This compiles everything cleanly into:
  `src\Greenshot\bin\Release\net480\Greenshot.exe`

---

## 📦 2. Building the Full Installer (Home/Personal Network)
To compile the actual Inno Setup installer package (`.exe`), you must be connected to an unblocked/personal network so that the `Tools.InnoSetup` NuGet package can be fetched.

Run this command:
```powershell
& "C:\Program Files\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin\MSBuild.exe" src\Greenshot.sln /p:Configuration=Release /t:Restore,Rebuild
```
* **How it works**: This will automatically restore `Tools.InnoSetup`, compile the codebase, run Inno Setup (`ISCC.exe`), and package the installation files.
* **Output Location**: The final installer `.exe` will be generated in:
  `installer/` (relative to the repository root directory).

---

## 🚢 3. Automated Build, Tag, and GitHub Release Publishing
We have a fully automated PowerShell script [build-and-deploy.ps1](file:///c:/Development/dev_Other/greenshot/build-and-deploy.ps1) in the repository root.

This script automatedly:
1. Pulls latest updates.
2. Restores and builds the entire solution in Release configuration.
3. Automatically extracts the version number from the compiled installer.
4. Generates a standalone ZIP archive of portable files.
5. Generates a Git tag (e.g. `v1.4.200`) and pushes it.
6. Publishes a GitHub Release with the installer and ZIP artifacts attached.

### Usage:
1. Ensure your GitHub Personal Access Token (PAT) has `Contents` and `Pages` read/write access.
2. Open PowerShell as Administrator and enable script execution if needed:
   ```powershell
   Set-ExecutionPolicy RemoteSigned -Scope Process
   ```
3. Execute the script from the repository root:
   ```powershell
   .\build-and-deploy.ps1
   ```
4. Paste your token when prompted.
