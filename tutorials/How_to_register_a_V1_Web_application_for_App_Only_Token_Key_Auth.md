# How to register a V1 Web application for App Only Token which uses the Key to acquire the Access Token

This tutorial introduces how to register the application using Microsoft Azure portal.
Once you registered an application by following this tutorial, you can use it as a "Web app / API (Use App Only Token by key)" in Office365APIEditor.

1. Go to https://portal.azure.com/ and sign in using your administrator account of Microsoft Azure.

2. Click [Azure Active Directory] in the left panel.

3. Click [App registration] tab.

4. Click [New application registration].

5. Enter the name of your application. (e.g. MyAppOnlyTokenApp)

6. Select [Web app / API].

7. Enter the sing-on URL of your application in [Sign-on URL] box. (e.g. https&#58;<span></span>//localhost/MyAppOnlyTokenApp <- Whatever is fine.)

8. Click [Create].

9. Click the application which you created.

10. Click [Keys].

11. Enter the name of your key. (e.g. Key01)

12. Select the duration of the key. (e.g. 2 years)

13. Click [Save].

14. Copy the [Value] to the safety place.

15. Click [Required permissions] and add the necessary permissions for your application.
If your application has to read the message in all mailboxes of all users using Graph API (graph.microsoft.com), you have to add the [Read mail in all mailboxes] permission under [APPLICATION PERMISSIONS] of [Microsoft Graph].

16. Check the following values and use them in Office365APIEditor.
If you have not completed ADMIN CONSENT, complete it before acquiring Access Token.

  | Value in Azure portal | Textbox in Office365APIEditor |  
  |:----------------------|-------------------------------|  
  |Application ID         |Application ID                 |  
  |Value of Key           |Key                            |  
