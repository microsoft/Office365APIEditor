Dim msiPath
msiPath = Wscript.Arguments(0)

Dim outPath
outPath = Wscript.Arguments(1)

Dim installer, database
Set installer = CreateObject("WindowsInstaller.Installer")
Set database = installer.OpenDatabase (msiPath, 0)

Dim view
Set view = database.OpenView ("SELECT Value FROM Property WHERE Property='ProductVersion'")
view.Execute

Dim result, version
Set result = view.Fetch
version = result.StringData(1)

Dim fileSystem, file

Set fileSystem = CreateObject("Scripting.FileSystemObject")
Set file= fileSystem.CreateTextFile(outPath)

file.WriteLine(version)
file.Close()

Set file = Nothing
Set fileSystem = Nothing