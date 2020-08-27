// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data
{
    [DataContract]
    public class MicrosoftGraphBaseObject
    {
        private string id;

        protected MicrosoftGraphBaseObject()
        {
        }

        public MicrosoftGraphBaseObject(string JsonData)
        {
            RawJson = JsonData;
        }

        public string RawJson { get; private set; }

        public string Id
        {
            get
            {
                if (string.IsNullOrEmpty(id))
                {
                    id = LoadPropertyFromRawJson<string>("id", null);
                }

                return id;
            }
            
            protected set
            {
                id = value;
            }
        }

        protected T LoadPropertyFromRawJson<T>(string PropertyName, T DefaultValue)
        {
            if (string.IsNullOrEmpty(RawJson))
            {
                return DefaultValue;
            }

            try
            {
                var jsonData = (JObject)JsonConvert.DeserializeObject(RawJson);

                foreach (var item in jsonData)
                {
                    if (item.Key.ToLowerInvariant() == PropertyName.ToLowerInvariant())
                    {
                        try
                        {
                            return item.Value.Value<T>();
                        }
                        catch
                        {
                            return item.Value.ToObject<T>();
                        }
                    }
                }

                return DefaultValue;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, string> GetRawProperties()
        {
            // Convert raw json to dictionary.

            Dictionary<string, string> result = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(RawJson))
            {
                return result;
            }

            try
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(RawJson);

                foreach (var property in jsonObject)
                {
                    if (property.Value.Type == JTokenType.Object || property.Value.Type == JTokenType.Array)
                    {
                        // Value is not basic type like string or bool.
                        // Convert to JSON formatted string.
                        result.Add(property.Key, JsonConvert.SerializeObject(property.Value));
                    }
                    else
                    {
                        result.Add(property.Key, property.Value.Value<string>());
                    }
                }

                return result;
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }
    }
}