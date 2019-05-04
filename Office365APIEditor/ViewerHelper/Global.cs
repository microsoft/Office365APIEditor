// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;

namespace Office365APIEditor.ViewerHelper
{
    public static class Global
    {
        public static PublicClientApplication pca;
        public static IAccount currentUser;
    }
}