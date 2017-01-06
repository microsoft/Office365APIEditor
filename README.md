# Office365APIEditor

You can test Office 365 API, Microsoft Graph and Office 365 Management Activity API easily.

## Download options

Download Office365APIEditor from [releases](https://github.com/Microsoft/Office365APIEditor/releases) page.

## Features

You can test Office 365 API, Microsoft Graph and Office 365 Management Activity API. We support following endpoints.
- outlook.office.com and outlook.office365.com (Office 365 API - Exchange Online)
- graph.microsoft.com (Microsoft Graph)
- manage.office.com (Office 365 Management Activity API)

You can use OAuth or Basic authentication as authentication mechanisms. (Only outlook.office365.com endpoint supports Basic authentication)

You can display items in your mailbox which are hosted by Exchange Online or Outlook.com.

## Prerequisites

You need the following to work with Office365APIEditor:
- A subscription to Office 365 and an user account, or a Microsoft Account.
- Version of the .NET Framework. Minimal requirement is .NET Framework 4.5.

And you need the following to use OAuth with V1 endpoint. You can also use the App Only Token.

- A subscriotion to Microsoft Azure which manages Azure Active Directory of your Office 365 tennant.
- An application which was registered in Azure Active Directory ('Web Application' or 'Native Application').

You need the following to use OAuth with V2 endpoint.

- An application which was registered in the [Application Registration Portal](https://apps.dev.microsoft.com/).

If you have not registered your application, follow the [tutorial](/tutorials/How_to_register_a_V1_Web_application.md).

## Command-line switches

| Switch     | Description                              |
|:-----------|:-----------------------------------------|
| /NoSetting | Remove all user settings.                |
| /NoHistory | Remove all request and response history. |

## Feedback

If you have any feedback, please post on the [Issues](https://github.com/Microsoft/Office365APIEditor/issues) list.

---
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
