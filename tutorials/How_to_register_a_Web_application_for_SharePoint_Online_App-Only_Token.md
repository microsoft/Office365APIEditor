# How to register a Web application for SharePoint Online App-Only Token

This tutorial introduces how to register the application using SharePoint Online Portal.
Once you registered an application by following this tutorial, you can use it as a "SharePoint Online App-Only REST API" in Office365APIEditor.

1. Go to https://\<tenantName\>.sharepoint.com/_layouts/15/appregnew.aspx

2. Create an application registration

3. Go to https://\<tenantName\>.sharepoint.com/_layouts/15/AppInv.aspx and assign permissions


Sample XML
```xml
<AppPermissionRequests AllowAppOnlyPolicy="true">
<AppPermissionRequest Scope="http://sharepoint/content/tenant" Right="Read"/>
</AppPermissionRequests>
```

```xml
<AppPermissionRequests AllowAppOnlyPolicy="true">
<AppPermissionRequest Scope="http://sharepoint/content/sitecollection" Right="Read"/>
</AppPermissionRequests>
```

To look up registration information for an add-in that you have registered, go to:  
https://\<tenantName\>.sharepoint.com/_layouts/15/AppInv.aspx

To see a list of registered add-in principals, go to:  
https://\<tenantName\>.sharepoint.com/_layouts/15/AppPrincipals.aspx

## Use the application in Office365APIEditor

|Value in SharePoint Online|Textbox in Office365APIEditor|
|:--|:--|
|Client ID|Client ID|
|Client Secret|Client Secret|

In [Tenant Name] box, input your onmicrosoft.com tenant name. (e.g. contoso.onmicrosoft.com)

## Links
https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azureacs  
https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/register-sharepoint-add-ins  
https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/get-to-know-the-sharepoint-rest-service  
https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/add-in-permissions-in-sharepoint  

