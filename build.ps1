param([ValidateSet("Windows", "Android")][string]$Target = "Windows")
$ErrorActionPreference = "Stop"
Push-Location $PSScriptRoot
try {
    $framework = if ($Target -eq "Android") { "net10.0-android" } else { "net10.0-windows10.0.19041.0" }
    $project = "MauiHomework1/MauiHomework1.csproj"
    dotnet restore $project "-p:TargetFrameworks=$framework"
    if ($LASTEXITCODE -ne 0) { throw "Restore failed." }
    dotnet build $project -c Debug -f $framework "-p:TargetFrameworks=$framework" --no-restore
    if ($LASTEXITCODE -ne 0) { throw "Build failed." }
} finally {
    Pop-Location
}
