# How to register a V1 Web application for App Only Token

This tutorial introduces how to register the application using Microsoft Azure portal.
Once you registered an application by following this tutorial, you can use it as a "Web application (Use App Only Token)" in Office365APIEditor.

1. Go to https://manage.windowsazure.com/ and sign in using your administrator account of Microsoft Azure.

2. Click [Active Directory] in the left panel.

3. In [DIRECTORY] tab, select the name of your organization.

4. In [APPLICATIONS] tab, click [ADD] at the bottom.

5. Click [Add an application my organization is developing].

6. Enter the name of your application. (e.g. MyAppOnlyWebApp)

7. Select [WEB APPLICATION AND/OR WEB API] and go to the next page.

8. Enter the sing-in URL of your application in [SIGN-ON URL] box. (e.g. http://localhost/MyAppOnlyWebApp <- Whatever is fine.)

9. Enter the APP ID URI of your application in [APP ID URI] box (e.g. http://contoso.onmicrosoft.com/MyAppOnlyWebApp <- Whatever is fine.)

10. Click [Complete].

11. In [CONFIGURE] tab, Go to [permissions to other applications] section and add the necessary permissions for your application.
If your application has to read the messages in users mailbox using Office 365 API (outlook.office.com), you have to add the [Read mail in all mailboxes] permission under [Application Permissions] of [Office 365 Exchange Online].
If your application has to read the messages in users mailbox using Graph API (graph.microsoft.com), you have to add the [Read mail in all mailboxes] permission under [Application Permissions] of [Microsoft Graph].

13. Click [SAVE] at the bottom.

15. Run the Command Prompt as an administrator and run the following command. The MakeCert tool is included in the Microsoft Windows Software Development Kit.

  makecert -r -pe -n "CN=<Subject Name>" -b <Start date> -e <End date> -ss my 2048
  
  e.g. makecert -r -pe -n "CN=MyAppOnlyWebApp" -b 09/22/2016 -e 09/22/2018 -ss my 2048

16. Run certmgr.msc and open the certificate management console.

17. The issued certificate is save to the [Personal] folder. Right click the certificate and click [All Tasks] - [Export].

18. Click [Next].

19. Select [No, do not export the private key] and click [Next].

20. Select [DER encoded binary X.509 (.CER)] and click [Next].

21. Enter the file name (e.g. c:\temp\MyAppOnlyWebApp.cer) and click [Next].

22. Click [Finish].

23. Right click the certificate and click [All Tasks] - [Export] again.

18. Click [Next].

19. Select [Yes, export the private key] and click [Next].

20. Select [Personal Infomation Exchange - PKCS #122 (.PFX)], [Include all certificates in the certificate path if possible] and click [Next].

21. Select [Password], enter the password for the exported certificate and click [Next].

21. Enter the file name (e.g. c:\temp\MyAppOnlyWebApp.pfx) and click [Next].

22. Click [Finish].

23. Run the notepad, paste following text and save it as "GetCert.ps1".

  ~~~powershell
$certPath = Read-Host "Enter certificate path (.cer)"
$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate
$cert.Import($certPath)
$rawCert = $cert.GetRawCertData()
$base64Cert = [System.Convert]::ToBase64String($rawCert)
$rawCertHash = $cert.GetCertHash()
$base64certHash = [System.Convert]::ToBase64String($rawCertHash)
$KeyId = [System.Guid]::NewGuid().ToString()
Write-Host "Base64Cert:"
Write-Host $base64Cert
Write-Host "Base64CertHash:"
Write-Host $base64certHash
Write-Host "KeyId:"
Write-Host $KeyId
  ~~~

24. Run the GetCert.ps1 and get Base64Cert, Base64CertHash and KeyId.

25. Go back to [CONFIGURE] tab in the portal and click [MANAGE MANIFEST] - [Download Manifest] at the bottom.

26. Click [Download manifest].

27. Open the downloaded JSON file and update [keyCredentials] section in the following format.

  ~~~json
  "keyCredentials": [
    {
      "customKeyIdentifier": "<$base64certHash FROM PS>",
      "keyId": "<$KeyId FROM PS>",
      "type": "AsymmetricX509Cert",
      "usage": "Verify",
      "value": "<$base64Cert FROM PS>"
    }
  ],
  ~~~

  e.g.

  ~~~json
  "keyCredentials": [
    {
      "customKeyIdentifier": "bXp8XlCY5OTE0tosNqaCnPlxQog=",
      "keyId": "da4733d7-4aba-48ee-8237-5a4eca7e16e2",
      "type": "AsymmetricX509Cert",
      "usage": "Verify",
      "value": "MIIDBTCCAfGgAwIBAgIQhyVVTt+XZ6VL8rmTp3Xj6jAJBgUrDgMCHQUAMBoxGDAWBgNVBAMTD015QXBwT25seVdlYkFwcDAeFw0xNjA5MjExNTAwMDBaFw0x
ODA5MjExNTAwMDBaMBoxGDAWBgNVBAMTD015QXBwT25seVdlYkFwcDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAL+bLrvdIK1MSWbY31RIiNw0
YMryaiFxquDSXUn8zHtmCpuqPESQJm1HeON6c8QcXrId0Ajzq0OGUcxTXUx/YnPjmCSJebbgF0z+bzLuHY3LGD8i38T9lBmPcIJVSKFwv7Y4cY+PYwEUQ7h6
2p/1JpY8vDl+rKL/i9PjoJcS0Efu6xvr1oabv1GbswhErIFpgv/GlDfQET1J9ZzhW4HEu9ZfWAhIfcGj1VveHvXyojNat80SrJ81FX0RcKbQ7kz7dv0PJIei
hGQc4gs5f95/D9nghJFoD/+of3WNWnqTyYgw7QQnGyOgsv80/DWDhge99jMu76t44yexQAnYPXDGzk0CAwEAAaNPME0wSwYDVR0BBEQwQoAQf0PRCHlTDjVG
Y74p2az/jKEcMBoxGDAWBgNVBAMTD015QXBwT25seVdlYkFwcIIQhyVVTt+XZ6VL8rmTp3Xj6jAJBgUrDgMCHQUAA4IBAQAejRz2w1GecmDlVFFMgj/Sg4tB
dBlJNFwC1rG04B2GIwwQsKm9FkzWMyKdsEAxBWd24NqtDJLk+ONsWeksWborwTKbLjN26dc6YBP0AWCaUWYVASoLRW2NZ0MbvFfYaaxOgfRh75UCOGk0kr5p
E6f9CrOzGtf3wPEXcHEwuDwYAee/gNTBbfgmTNfld3Sh0H0E2CzwGjYTxkoooT0SJ3C/CgMV4GS8DaegeARKRFctGOP5MqHK1NhLr4GkjVKoRg3RhoKxKQWl
uZrkfRhX5Z+s2xhLjjrhaU8fptEvUWyD8l6QUI934DZ9VnOds7AeBiH0MYPmPKZuVmm/yE4WxFxR"
    }
  ],
  ~~~

28. Save the JSON file.

29. Go back to [CONFIGURE] tab in the portal and click [MANAGE MANIFEST] - [Upload Manifest] at the bottom.

30. Select the updated JSON file and click [OK].

14. Check the following values and use them in Office365APIEditor.

  | Value in Azure portal | Textbox in Office365APIEditor |  
  |:----------------------|-------------------------------|  
  |CLIENT ID              |Client ID                      |  

15. In Office365APIEditor, enter the file path of the exported PFX file in the [Certificate Path] box. And enter the password of the certificate in the [Password for cert] box.