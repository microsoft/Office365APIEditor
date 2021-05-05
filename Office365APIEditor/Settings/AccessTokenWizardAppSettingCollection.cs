// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.ObjectModel;

namespace Office365APIEditor.Settings
{
    public class AccessTokenWizardAppSettingCollection : Collection<AccessTokenWizardAppSetting>
    {
        readonly string defaultNewAppDisplayName = "NewAppSetting";

        [Obsolete("Add method is not implemented. Use UpdateAppSetting method instead.")]
        public new void Add(AccessTokenWizardAppSetting appSetting)
        {
            throw new NotImplementedException("Add method is not implemented. Use UpdateAppSetting method instead.");
        }

        public void AddForce(AccessTokenWizardAppSetting appSetting)
        {
            Items.Add(appSetting);
        }

        public void Update(AppSettingType appSettingType, AccessTokenWizardAppSetting appSetting) {
            // Check if the same AppSettingType is already used.

            int index = -1;

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].DisplayName == appSettingType.ToString())
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                // Remove existing app.
                Items.RemoveAt(index);
            }

            Items.Add(appSetting);
        }

        public AccessTokenWizardAppSetting Find(AppSettingType appSettingType)
        {
            // Search apps by AppSettingType and return the first one.

            return Find(appSettingType.ToString());
        }

        public AccessTokenWizardAppSetting Find(string displayName)
        {
            // Search apps by DisplayName and return the first one.

            foreach (var app in Items)
            {
                if (app.DisplayName == displayName)
                {
                    return app;
                }
            }

            return null;
        }

        public string FindNewAppDisplayName()
        {
            for (int i = 1; i < 100; i++)
            {
                string tempName = defaultNewAppDisplayName + i.ToString();

                if (Find(tempName) == null)
                {
                    return tempName;
                }
            }

            return (new Guid()).ToString();
        }
    }
}