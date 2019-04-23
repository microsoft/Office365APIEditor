param(
    [string]$MsiPath,
    [string]$VersionOutPath,
    [string]$ReleaseFolder,
    [string]$ZipOutDir
)

$Installer = New-Object -ComObject "WindowsInstaller.Installer"
$Database = $Installer.OpenDatabase($MsiPath, 0)
$View = $Database.OpenView("SELECT Value FROM Property WHERE Property='ProductVersion'")
$View.Execute()
$Result = $View.Fetch()
$Version = $Result.StringData(1)

$Version | Out-File -FilePath $VersionOutPath -Encoding UTF8

$ZipFileName = "Office365APIEditor.zip"

$ZipFilePath = Join-Path $ZipOutDir $ZipFileName

Compress-Archive -Path:("$ReleaseFolder\*") -DestinationPath:$ZipFilePath -Force