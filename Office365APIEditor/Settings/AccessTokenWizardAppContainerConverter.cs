// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Office365APIEditor.Settings
{
    public class AccessTokenWizardAppContainerConverter : TypeConverter
    {
        // AccessTokenWizardAppContainerConverter is used when saving AccessTokenWizardAppContainer.

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // AccessTokenWizardAppContainer is saved as base64 encoded json string.
            // To read setting, we need to decoded and deserialize it.

            if (value is string)
            {
                AccessTokenWizardAppContainer result = new AccessTokenWizardAppContainer();

                string jsonStyleValue = DecodeFromBase64((string)value);
                
                dynamic dynamicaccessTokenWizardAppContainer = JObject.Parse(jsonStyleValue);
                
                if (dynamicaccessTokenWizardAppContainer.LastApps != null)
                {
                    JArray jArrayLastApps = (JArray)dynamicaccessTokenWizardAppContainer.LastApps;
                    result.LastApps = JsonConvert.DeserializeObject<AccessTokenWizardAppSettingCollection>(jArrayLastApps.ToString());
                }

                if (dynamicaccessTokenWizardAppContainer.SavedApps != null)
                {
                    JArray jArraySavedApps = (JArray)dynamicaccessTokenWizardAppContainer.SavedApps;
                    result.SavedApps = JsonConvert.DeserializeObject<AccessTokenWizardAppSettingCollection>(jArraySavedApps.ToString());
                }

                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            // AccessTokenWizardAppContainer is saved as base64 encoded json string.
            // To save setting, we need to serialize and encode it.

            if (destinationType == typeof(string))
            {
                string result = "";

                if (value == null)
                {
                    result = "{\"LastApps\":[],\"SavedApps\":[]}";
                }
                else
                {
                    AccessTokenWizardAppContainer accessTokenWizardAppContainer = value as AccessTokenWizardAppContainer;

                    result += "{\"LastApps\":" + ConvertAccessTokenWizardAppCollectionToString(accessTokenWizardAppContainer.LastApps) + ",\"SavedApps\":" + ConvertAccessTokenWizardAppCollectionToString(accessTokenWizardAppContainer.SavedApps) + "}";
                }

                return EncodeToBase64(result);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        private string ConvertAccessTokenWizardAppCollectionToString(AccessTokenWizardAppSettingCollection appSettings)
        {
            StringBuilder result = new StringBuilder();

            List<string> stringAppSettingList = new List<string>();

            result.Append('[');

            foreach (var appSetting in appSettings)
            {
                stringAppSettingList.Add(JsonConvert.SerializeObject(appSetting));
            }

            result.Append(string.Join(",", stringAppSettingList.ToArray()));
            result.Append(']');

            return result.ToString();
        }

        private string DecodeFromBase64(string Base64EncodedString)
        {
            var base64EncodedBytes = Convert.FromBase64String(Base64EncodedString);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private string EncodeToBase64(string Base64DecodedString)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(Base64DecodedString);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}