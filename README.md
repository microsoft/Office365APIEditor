# Office365APIEditor

You can test Office 365 API easily.

## Download options

Download Office365APIEditor from [releases](https://github.com/Microsoft/Office365APIEditor/releases) page.

## Features

You can test Office 365 API. We support following endpoints.
- outlook.office.com and outlook.office365.com (Exchange Online)
- graph.microsoft.com (Microsoft Graph)

You can use OAuth or Basic authentication as authentication mechanisms.

You can display items in your mailbox which hosted by Exchange Online or Outlook.com.

## Prerequisites

You need the following to work with Office365APIEditor:
- A subscription of Office 365 and a user account, or a Microsoft Account.
- Version of the .NET Framework starting with the .NET Framework 4.5.

And you need the following to use OAuth with V1 endpoint. You can test the App Only Token as well.

- A subscriotion of Microsoft Azure which manages Azure Active Directory of your Office 365 tennant.
- An application which was registered in Azure Active Directory ('Web Application' of 'Native Application').

You need the following to use OAuth with V2 endpoint.

- An application which was registered in the [Application Registration Portal](https://apps.dev.microsoft.com/).

If you have not registered your application, follow the [tutorial](/tutorial1.md).

## Feedback

If you have any feedback, please post to the [Issues](https://github.com/Microsoft/Office365APIEditor/issues) list.

---
This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.