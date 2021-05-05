// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.Settings
{
    // AccessTokenWizardAppSetting is used in AccessTokenWizardForm.
    // When adding new props to this class, we need to review the implementation of AccessTokenWizardAppCollectionConverter.
    // We might need to update AccessTokenWizardAppCollectionConverter as well.
    public class AccessTokenWizardAppSetting
    {
        public AccessTokenWizardAppSetting()
        {
            DisplayName = "";
            ApplicationId = "";
            RedirectUri = "";
            Resource = Resources.None;
            ClientSecret = "";
            TenantName = "";
            CertificatePath = "";
            CertificatePassword = "";
            Scopes = "";
            Note = "";
        }

        public string DisplayName { get; set; }

        public string ApplicationId { get; set; }

        public string RedirectUri { get; set; }

        public Resources Resource { get; set; }

        public string ClientSecret { get; set; }

        public string TenantName { get; set; }

        public string CertificatePath { get; set; }

        public string CertificatePassword { get; set; }

        public string Scopes { get; set; }
        
        public string Note { get; set; }
    }
}