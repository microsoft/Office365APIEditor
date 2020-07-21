// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        public ViewerRequestHelper()
        {
            // TODO : Implement logging feature
            // Maybe in Util, not here.
        }

        public static async Task<string> SendGetRequestAsync(Uri URL)
        {
            try
            {
                var accessToken = await Global.GetAccessTokenAsync();
                return await Util.SendGetRequestAsync(URL, accessToken, Global.currentUser.Username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> SendPostRequestAsync(Uri URL, string PostData)
        {
            try
            {
                var accessToken = await Global.GetAccessTokenAsync();
                return await Util.SendPostRequestAsync(URL, accessToken, Global.currentUser.Username, PostData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> SendPatchRequestAsync(Uri URL, string PostData)
        {
            try
            {
                var accessToken = await Global.GetAccessTokenAsync();
                return await Util.SendPatchRequestAsync(URL, accessToken, Global.currentUser.Username, PostData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> SendDeleteRequestAsync(Uri URL)
        {
            try
            {
                var accessToken = await Global.GetAccessTokenAsync();
                return await Util.SendDeleteRequestAsync(URL, accessToken, Global.currentUser.Username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTimeOffset? ConvertDateTimeToDateTimeOffset(DateTime dateTimeValue)
        {
            try
            {
                if (dateTimeValue == null)
                {
                    return null;
                }
                else
                {
                    if (dateTimeValue.Kind == DateTimeKind.Utc || dateTimeValue.Kind == DateTimeKind.Local)
                    {
                        return new DateTimeOffset(dateTimeValue);
                    }
                    else
                    {
                        return new DateTimeOffset(dateTimeValue, new TimeSpan(0));
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}