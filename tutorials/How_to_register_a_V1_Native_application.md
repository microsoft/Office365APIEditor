# How to register a V1 Native application

This tutorial introduces how to register the application using Microsoft Azure portal.
Once you registered an application by following this tutorial, you can use it as a "Native application" in Office365APIEditor.

1. Go to https://manage.windowsazure.com/ and sign in using your administrator account of Microsoft Azure.

2. Click [Active Directory] in the left panel.

3. In [DIRECTORY] tab, select the name of your organization.

4. In [APPLICATIONS] tab, click [ADD] at the bottom.

5. Click [Add an application my organization is developing].

6. Enter the name of your application. (e.g. MyNativeApp)

7. Select [NATIVE CLIENT APPLICATION] and go to the next page.

8. Enter the redirect URI of your application in [REDIRECT URI] box. (e.g. http://localhost/MyNativeApp <- Whatever is fine.)

9. Click [Complete].

10. In [CONFIGURE] tab, Go to [permissions to other applications] section and add the necessary permissions for your application.
If your application has to read the messages in users mailbox using Office 365 API (outlook.office.com), you have to add the [Read user mail] permission under [Delegated Permissions] of [Office 365 Exchange Online].
If your application has to read the messages in users mailbox using Graph API (graph.microsoft.com), you have to add the [Read user mail] permission under [Delegated Permissions] of [Microsoft Graph].

11. Click [SAVE] at the bottom.

13. Check the following values and use them in Office365APIEditor.

  | Value in Azure portal | Textbox in Office365APIEditor |  
  |:----------------------|-------------------------------|  
  |CLIENT ID              |Client ID                      |  
  |REDIRECT URI            |Redirect URL                   |  
