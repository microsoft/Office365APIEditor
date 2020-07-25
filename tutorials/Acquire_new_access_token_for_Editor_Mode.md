# Acquire new access token for Editor Mode

Office365APIEditor Editor Mode supports a variety of access token acquisition scenarios.

If your application is registered with Azure Active Directory and you want to use the configured permissions, select [v1.0 Endpoint]. In this scenario, Office365APIEditor uses the Azure Active Directory v1.0 endpoint to acquire an access token. Your application can be a web app or a public client. And your application can use the delegated permissions and the application permissions. If your application uses the application permissions, you can use a certificate or a client secret to acquire an access token.

If your application is registered with Azure Active Directory and you want to dynamically acquire an access token, select [v2.0 Endpoint]. In this scenario, Office365APIEditor uses the Microsoft Identity Platform v2.0 endpoint to acquire an access token. Your application can be a web app or a public client. And your application can use the delegated permissions of a variety of APIs and the application permissions of Microsoft Graph. If your application uses the application permissions of Microsoft Graph, you can use a client secret to acquire an access token.

If you have not registered the application with Azure Active Directory, select [I have not registered the application]. In this scenario, Office365APIEditor will use the built-in application or basic auth.

If your application is registered with SharePoint Online, select [I have registered the SharePoint Online App-Only REST API].