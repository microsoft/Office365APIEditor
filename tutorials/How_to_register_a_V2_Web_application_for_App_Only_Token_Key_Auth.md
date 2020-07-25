# How to register a V2 Web application and client secret for App Only Token

This tutorial introduces how to register the application using Microsoft Azure portal. Once you have registered an application by following this tutorial, you can use it as a "Web app (Use App Only Token by client secret for Microsoft Graph)" in Office365APIEditor.

1. Go to https://portal.azure.com/ and sign in using your tenant administrator account.
2. Click [Azure Active Directory] in the left panel.
3. Click [App registration].
4. Click [New registration].
5. Enter the name of your application. (e.g. MyAppOnlyWebApp)
6. It doesn't matter what you select for [Supported account types].
7. Select [Web], and enter the redirect URI of your application in [Redirect URI]. (e.g. https&#58;<span></span>//localhost/MyAppOnlyWebApp <- Whatever is fine.)
8. Click [Register].
9. Copy the value of [Overview] - [Application (client) ID].
10. Click [Certificates & secrets].
11. Click [New client secret].
12. Enter the name of your key. (e.g. Key01)
13. Select the duration of the key. (e.g. in 2 years)
14. Click [Add].
15. Copy the [Value] to the safety place.
16. Click [API permissions] and add the necessary permissions for your application. If your application has to read the messages in the users' mailbox using Microsoft Graph, you have to add the [Mail.Read] permission under [Application permissions] of [Microsoft Graph].
17. Click [Grant admin consent for <Your tenant name>].
18. Click [Yes].
19. Check the following values and use them in Office365APIEditor.

  | Value in Azure portal  | Textbox in Office365APIEditor |  
  |:-----------------------|-------------------------------|  
  |Application (Client) ID |Application ID                 |  
  |Value of Client secret  |Client secret                  |  

  [Tenant name] is your Office 365 tenant name. (e.g. contoso.onmicrosoft.com)  