# How to register a V2 Web application

This tutorial introduces how to register the application using Microsoft Azure portal. Once you have registered an application by following this tutorial, you can use it as a "Web app" in Office365APIEditor.

1. Go to https://portal.azure.com/ and sign in using your Office 365 account.
2. Click [Azure Active Directory] in the left panel.
3. Click [App registration].
4. Click [New registration].
5. Enter the name of your application. (e.g. MyWebApp)
6. It doesn't matter what you select for [Supported account types].
7. Select [Web], and enter the redirect URI of your application in [Redirect URI]. (e.g. https&#58;<span></span>//localhost/MyWebApp <- Whatever is fine.)
8. Click [Register].
9. Copy the value of [Overview] - [Application (client) ID].
10. Click [Certificates & secrets].
11. Click [New client secret].
12. Enter the name of your key. (e.g. Key01)
13. Select the duration of the key. (e.g. in 2 years)
14. Click [Add].
15. Copy the [Value] to the safety place.
16. Check the following values and use them in Office365APIEditor.

  | Value in Azure portal  | Textbox in Office365APIEditor |  
  |:-----------------------|-------------------------------|  
  |Application (client) ID |Application ID                 |  
  |Redirect URI            |Redirect URI                   |  
  |Value of Client secret  |Client secret                  |  

  [Tenant name] is your Office 365 tenant name. (e.g. contoso.onmicrosoft.com)