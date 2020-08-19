param(
    [string]$wwwrootPath
)

$InstallersPath = Join-Path $wwwrootPath "installers"

$MsiFile = Get-ChildItem -Path $InstallersPath -Filter *.msi

if ($MsiFile.GetType().Name -ne "FileInfo") {
	# 2 or more MSI files are existing.
	exit 1
}

Rename-Item $MsiFile.FullName -NewName "Setup.msi"

$MsiFile = Get-ChildItem -Path $InstallersPath -Filter *.msi

$ZipFile = Get-ChildItem -Path $InstallersPath -Filter *.zip

if ($ZipFile.GetType().Name -ne "FileInfo") {
	# 2 or more ZIP files are existing.
	exit 2
}

Rename-Item $ZipFile.FullName -NewName "Office365APIEditor.zip"

$ZipFile = Get-ChildItem -Path $InstallersPath -Filter *.zip

$VersionOutPath = Join-Path $wwwrootPath "latestMsi.txt"
$Installer = New-Object -ComObject "WindowsInstaller.Installer"
$Database = $Installer.OpenDatabase($MsiFile.FullName, 0)
$View = $Database.OpenView("SELECT Value FROM Property WHERE Property='ProductVersion'")
$View.Execute()
$Result = $View.Fetch()
$Version = $Result.StringData(1)

$Version | Out-File -FilePath $VersionOutPath -Encoding UTF8