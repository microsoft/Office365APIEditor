# Office365APIEditor

You can test Office 365 API, Microsoft Graph and Office 365 Management Activity API easily.

## Download options

Download Office365APIEditor from [releases](https://github.com/Microsoft/Office365APIEditor/releases) page.

## Features

### Mailbox Viewer mode
You can browse folders, items, and so on in your mailbox using Outlook REST API.

### Editor mode
- You can test Office 365 API, Microsoft Graph and Office 365 Management Activity API. We support following endpoints.
	- graph.microsoft.com (Microsoft Graph)
	- outlook.office.com (Office 365 API - Exchange Online)
	- manage.office.com (Office 365 Management Activity API)

- In addition to the built-in application ID, you can also use the application ID that you have registered in the Azure Active Directory to acquire an access token.

## Prerequisites

You need the following to work with Office365APIEditor:
- An Office 365 account or a Microsoft Account
- Microsoft .NET Framework 4.5 or later

You don't have to register your application in the Azure Active Directory or [Application Registration Portal](https://apps.dev.microsoft.com/) if you want to use the built-in application ID.

## Command-line switches

| Switch     | Description                                 |
|:-----------|:--------------------------------------------|
| /NoSetting | Remove all user settings.                   |
| /NoHistory | Remove all request and response history.    |
| /CustomDefinedScopes | Remove all custom defined scopes. |
| /SystemLogging | Enable system logging.                  |

## Feedback

If you have any feedback, please post on the [Issues](https://github.com/Microsoft/Office365APIEditor/issues) list.

---
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
