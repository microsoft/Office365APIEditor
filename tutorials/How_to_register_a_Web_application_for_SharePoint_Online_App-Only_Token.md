# How to register a Web application for SharePoint Online App-Only Token

This tutorial introduces how to register the application using SharePoint Online Portal.
Once you registered an application by following this tutorial, you can use it as a "SharePoint Online App-Only REST API" in Office365APIEditor.

1. Go to https://<tenantName>.sharepoint.com/_layouts/15/appregnew.aspx

2. Create an application registration

3. Assign permissions, go to http://<SharePointWebsite>/_layouts/15/AppInv.aspx


Sample XML
```
<AppPermissionRequests AllowAppOnlyPolicy="true">
<AppPermissionRequest Scope="http://sharepoint/content/tenant" Right="Read"/>
</AppPermissionRequests>
```

```
<AppPermissionRequests AllowAppOnlyPolicy="true">
<AppPermissionRequest Scope="http://sharepoint/content/sitecollection" Right="Read"/>
</AppPermissionRequests>
```

To look up registration information for an add-in that you have registered, go to http://<SharePointWebsite>/_layouts/15/AppInv.aspx.
To see a list of registered add-in principals, go to: http://<SharePointWebsite>/_layouts/15/AppPrincipals.aspx.



## Links
https://docs.microsoft.com/en-us/sharepoint/dev/solution-guidance/security-apponly-azureacs
https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/register-sharepoint-add-ins
https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/get-to-know-the-sharepoint-rest-service
https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/add-in-permissions-in-sharepoint

