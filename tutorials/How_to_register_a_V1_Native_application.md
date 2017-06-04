# How to register a V1 Native application

This tutorial introduces how to register the application using Microsoft Azure portal.
Once you registered an application by following this tutorial, you can use it as a "Native application" in Office365APIEditor.

1. Go to https://portal.azure.com/ and sign in using your administrator account of Microsoft Azure.

2. Click [Azure Active Directory] in the left panel.

3. Click [App registration] tab.

4. Click [New application registration].

5. Enter the name of your application. (e.g. MyNativeApp)

6. Select [Native].

7. Enter the Redirect URI of your application in [Redirect URI] box. (e.g. https&#58;<span></span>//localhost/MyNativeApp <- Whatever is fine.)

8. Click [Create].

9. Click the application which you created.

10. Click [Required permissions] and add the necessary permissions for your application.
If your application has to read the messages in users mailbox using Office 365 API (outlook.office.com), you have to add the [Read user mail] permission under [DELEGATED PERMISSIONS] of [Office 365 Exchange Online (Microsoft.Exchange)].
If your application has to read the messages in users mailbox using Graph API (graph.microsoft.com), you have to add the [Read user mail] permission under [DELEGATED PERMISSIONS] of [Microsoft Graph].

11. Check the following values and use them in Office365APIEditor.

  | Value in Azure portal | Textbox in Office365APIEditor |  
  |:----------------------|-------------------------------|  
  |Application ID         |Application ID                 |  
  |Redirect URI           |Redirect URI                   |  
