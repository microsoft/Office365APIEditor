// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

namespace Office365APIEditor.ViewerHelper.SampleRequest
{
    public class SampleRequestDefinitionRoot
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public FirstlevelCategory[] FirstLevelCategory { get; set; }
    }

    public class FirstlevelCategory
    {
        public string DisplayName { get; set; }
        public SecondlevelCategory[] SecondLevelCategory { get; set; }
    }

    public class SecondlevelCategory
    {
        public string DisplayName { get; set; }
        public ThirdlevelCategory[] ThirdLevelCategory { get; set; }
    }

    public class ThirdlevelCategory
    {
        public string DisplayName { get; set; }

        public FourthLevelCategory[] FourthLevelCategory { get; set; }

        public SampleRequest[] SampleRequest { get; set; }
    }

    public class FourthLevelCategory {
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