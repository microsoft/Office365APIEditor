# How to register a V2 Web application and certificate for App Only Token

This tutorial introduces how to register the application using Microsoft Azure portal. Once you have registered an application by following this tutorial, you can use it as a "Web app (Use App Only Token by certificate)" in Office365APIEditor.

1. Launch Windows PowerShell.
2. Run the following command.
  
  ```powershell
  New-SelfSignedCertificate -Subject "CN=<Your Application Name>" -CertStoreLocation "Cert:\CurrentUser\My" -KeyExportPolicy Exportable -KeySpec Signature
  ```

  e.g.

  ```powershell
  New-SelfSignedCertificate -Subject "CN=MyAppOnlyWebApp" -CertStoreLocation "Cert:\CurrentUser\My" -KeyExportPolicy Exportable -KeySpec Signature
  ```

3. Run certmgr.msc and open the certificate management console.
4. Click [Personal] - [Certificates].
5. Find the issued certificate and click [All Tasks] - [Export] from the right-click menu.
6. Click [Next].
7. Select [No, do not export the private key] and click [Next].
8. Select [DER encoded binary X.509 (.CER)] and click [Next].
9. Enter the file name (e.g. c:\temp\MyAppOnlyWebApp.cer) and click [Next].
10. Click [Finish].
11. Right click the certificate and click [All Tasks] - [Export] again.
12. Click [Next].
13. Select [Yes, export the private key] and click [Next].
14. Select [Personal Information Exchange - PKCS #122 (.PFX)], [Include all certificates in the certificate path if possible] and click [Next].
15. Select [Password], enter the password for the exported certificate and click [Next].
16. Enter the file name (e.g. c:\temp\MyAppOnlyWebApp.pfx) and click [Next].
17. Click [Finish].
18. Go to https://portal.azure.com/ and sign in using your tenant administrator account.
19. Click [Azure Active Directory] in the left panel.
20. Click [App registration].
21. Click [New registration].
22. Enter the name of your application. (e.g. MyAppOnlyWebApp)
23. It doesn't matter what you select for [Supported account types].
24. Select [Web], and enter the redirect URI of your application in [Redirect URI]. (e.g. https&#58;<span></span>//localhost/MyAppOnlyWebApp <- Whatever is fine.)
25. Click [Register].
26. Copy the value of [Overview] - [Application (client) ID].
27. Click [Certificates & secrets].
28. Click [Upload certificate].
29. Select the certificate which was exported by the step 10. and click [Add].
30. Click [API permissions] and add the necessary permissions for your application. If your application has to read the messages in the users' mailbox using Microsoft Graph, you have to add the [Mail.Read] permission under [Application permissions] of [Microsoft Graph].
31. Click [Grant admin consent for <Your tenant name>].
32. Click [Yes].
33. Check the following values and use them in Office365APIEditor.

  | Value in Azure portal  | Textbox in Office365APIEditor |  
  |:-----------------------|-------------------------------|  
  |Application (client) ID |Application ID                 |  

  [Tenant name] is your Office 365 tenant name. (e.g. contoso.onmicrosoft.com)  
  [Certificate Path] is the file path of the exported PFX file, and [Password] is the password of the PFX file.