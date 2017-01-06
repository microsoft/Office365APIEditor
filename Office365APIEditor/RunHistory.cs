// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor
{
    [System.Xml.Serialization.XmlRoot("RunHistory")]
    public class RunHistory
    {
        [System.Xml.Serialization.XmlElement("RunInfo")]
        public System.Collections.Generic.List<RunInformation> RunInfo { get; set; }
    }
}
