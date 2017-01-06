// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;

namespace Office365APIEditor
{
    public class RunInformation
    {
        [System.Xml.Serialization.XmlAttribute("ExecutionID")]
        public String ExecutionID { get; set; }

        [System.Xml.Serialization.XmlElement("RequestUrl")]
        public String RequestUrl { get; set; }

        [System.Xml.Serialization.XmlElement("RequestMethod")]
        public String RequestMethod { get; set; }

        [System.Xml.Serialization.XmlElement("RequestHeader")]
        public String RequestHeader { get; set; }

        [System.Xml.Serialization.XmlElement("RequestCompleteHeader")]
        public String RequestCompleteHeader { get; set; }

        [System.Xml.Serialization.XmlElement("RequestBody")]
        public String RequestBody { get; set; }

        [System.Xml.Serialization.XmlElement("ResponseStatusCode")]
        public String ResponseStatusCode { get; set; }

        [System.Xml.Serialization.XmlElement("ResponseHeader")]
        public String ResponseHeader { get; set; }

        [System.Xml.Serialization.XmlElement("ResponseBody")]
        public String ResponseBody { get; set; }
    }
}
