// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.SampleRequest
{
    public class SampleRequestDefinitionRoot
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public Firstlevelcategory[] FirstLevelCategory { get; set; }
    }

    public class Firstlevelcategory
    {
        public string DisplayName { get; set; }
        public Secondlevelcategory[] SecondLevelCategory { get; set; }
    }

    public class Secondlevelcategory
    {
        public string DisplayName { get; set; }
        public Thirdlevelcategory[] ThirdLevelCategory { get; set; }
    }

    public class Thirdlevelcategory
    {
        public string DisplayName { get; set; }
        public SampleRequest[] SampleRequest { get; set; }
    }

    public class SampleRequest
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Method { get; set; }
        public string URI { get; set; }
        public Header[] Header { get; set; }
        public string Body { get; set; }
    }

    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}