# How to register applications for OAuth

This tutorial introduces how to register the application using Microsoft Azure portal. You can use [Office 365 App Registration Tool](http://dev.office.com/app-registration) and [Application Registration Portal](https://apps.dev.microsoft.com) as well.
Once you registered an application by following this tutorial, you can use it as a "Web application" in Office365APIEditor.

1. Go to https://manage.windowsazure.com/ and sign in using your administrator account of Microsoft Azure.

2. Click [Active Directory] in the left pane.

3. In [DIRECTORY] tab, Click the name of your organization.

4. In [APPLICATIONS] tab, click [ADD] at the bottom.

5. Click [Add an application my organization is developing].

6. Enter the name of your application. (e.g. MyWebApp)

7. Select [WEB APPLICATION AND/OR WEB API] and go to the next page.

8. Enter the sing-in URL of your application in [SIGN-ON URL] box. (e.g. http&#58;<span></span>//localhost/MyWebApp <- this is really something you can use)

9. Enter the APP ID URI of your application in [APP ID URI] box (e.g. http&#58;<span></span>//contoso.onmicrosoft.com/MyWebApp <- this is really something you can use)

10. Click [Complete].

11. In [COFIGURE] tab, go to [Keys] section and select the duration of the key. (e.g. 2 years)

12. Go to [permissions to other applications] section and add the necessary permissions for your application.
If your application has to read the messages in users mailbox using Office 365 API (outlook.office.com), you have to add the [Read user mail] permission under [Delegated Permissions] of [Office 365 Exchange Online].
If your application has to read the messages in users mailbox using Graph API (graph.microsoft.com), you have to add the [Read user mail] permission under [Delegated Permissions] of [Microsoft Graph].

13. Click [SAVE] at the bottom.

14. Check the following values and use them in Office365APIEditor.

| Value in Azure portal | Textbox in Office365APIEditor |  
|:----------------------|-------------------------------|  
|SIGN-ON URL            |Redirect URL                   |  
|CLIENT ID              |Client ID                      |  
|key in [Keys] section  |Client Secret                  |  
