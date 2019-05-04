// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper.Data
{
    public class DateTimeOffset
    {
        public string DateTime;
        public string TimeZone;

        public override string ToString()
        {
            return $"{DateTime} ({TimeZone})";
        }
    }
}