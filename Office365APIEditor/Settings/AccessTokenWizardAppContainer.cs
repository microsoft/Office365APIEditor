// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System.ComponentModel;
using System.Configuration;

namespace Office365APIEditor.Settings
{
    [TypeConverter(typeof(AccessTokenWizardAppContainerConverter))]
    [SettingsSerializeAs(SettingsSerializeAs.String)]
    public class AccessTokenWizardAppContainer
    {
        // List of the last used apps.
        public AccessTokenWizardAppSettingCollection LastApps { get; set; }

        // List of apps saved by user.
        public AccessTokenWizardAppSettingCollection SavedApps { get; set; }
    }
}