# Start new Mailbox Viewer Session

This tutorial introduces how to start new Mailbox Viewer Session.

## Option 1 : Use built-in app

1. Launch Office365APIEditor.
2. Click [File] - [New session].
3. Click [Sign in].
4. Sign in with your Office 365 account.
5. You can browse your mailbox. Mail folders and contact folders will be shown under MsgFolderRoom folder. Calendar folders and task folders will be shown under each dummy root folders.

## Option 2 : Use your owan app

1. Go to https://portal.azure.com/ and sign in using your Office 365 account.
2. Click [Azure Active Directory] in the left panel.
3. Click [App registration].
4. Click [New registration].
5. Enter the name of your application. (e.g. MyOffice365APIEditorApp)
6. Select [Accounts in any organizational directory (Any Azure AD directory - Multitenant) and personal Microsoft accounts (e.g. Skype, Xbox)].
7. Select [Public client/native (mobile & desktop)], and enter [https&#58;<span></span>//login.microsoftonline.com/common/oauth2/nativeclient] in [Redirect URI].
8. Click [Register].
9. Copy the value of [Overview] - [Application (client) ID].
10. Launch Office365APIEditor.
11. Click [File] - [New session].
12. Paste your application ID in [Application ID].
13. Click [Use my application].
14. Sign in with your Office 365 account.
15. You can browse your mailbox. Mail folders and contact folders will be shown under MsgFolderRoom folder. Calendar folders and task folders will be shown under each dummy root folders.