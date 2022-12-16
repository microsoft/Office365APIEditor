# Office365APIEditor

You can test Microsoft Graph, Outlook REST API, Office 365 Management Activity API and Azure Resource Manager API easily.

## Download options

Download Office365APIEditor from [releases](https://github.com/Microsoft/Office365APIEditor/releases) page.

## Features

Click on the image below to see the introduction video.  
[![Watch a video on YouTube](https://img.youtube.com/vi/Tm3APCwng6Y/maxresdefault.jpg)](http://www.youtube.com/watch?v=Tm3APCwng6Y "Introducing Office365APIEditor")

### Mailbox Viewer mode
You can browse your mailbox contents such as folders, items, and mailbox properties using Microsoft Graph.

### Editor mode
- You can send requests to APIs such as Microsoft Graph. We support following endpoints.
	- graph.microsoft.com (Microsoft Graph)
	- outlook.office.com (Outlook REST API)
	- manage.office.com (Office 365 Management Activity API)
	- management.azure.com (Azure Resource Manager API)

- In addition to the built-in application ID, you can also use the application ID that you have registered in the Azure Active Directory to acquire an access token.

## Prerequisites

You need the following to work with Office365APIEditor:
- An Office 365 account or a Microsoft Account
- Microsoft .NET Framework 4.8 or later
- A minimum screen resolution of 1024 x 768

You don't have to register your application in the Azure Active Directory if you want to use the built-in application ID.

You can use both managed accounts and federated accounts as Office 365 accounts. But it is recommended to use managed accounts to simplify the authentication flow.
If you are using AD FS in your organization, you might need to be enabled Forms Authentication to use federated accounts, because Office365APIEditor always send prompt=login and you need to type the user name on the authentication form.

## Command-line switches

| Switch                    | Description                              |
|:--------------------------|:-----------------------------------------|
| /ResetSettings            | Reset all user settings.                 |
| /RemoveHistory            | Remove all request and response history. |
| /ResetCustomDefinedScopes | Reset all custom defined scopes.         |
| /EnableSystemLogging      | Enable system logging.                   |

## Feedback

If you have any feedback, please post on the [Issues](https://github.com/Microsoft/Office365APIEditor/issues) list.

---
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
