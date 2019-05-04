// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Identity.Client;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper
{
    partial class ViewerRequestHelper
    {
        private PublicClientApplication pca;
        private IAccount currentUser;

        public ViewerRequestHelper(PublicClientApplication PCA, IAccount CurrentUser)
        {
            pca = PCA;
            currentUser = CurrentUser;

            // TODO : Implement logging feature
            // Maybe in Util, not here.
            //client.Context.BuildingRequest += new EventHandler<BuildingRequestEventArgs>(
            //    (eventSender, eventArgs) => RequestLogger(eventSender, eventArgs));
            //client.Context.ReceivingResponse += new EventHandler<ReceivingResponseEventArgs>(
            //    (eventSender, eventArgs) => RequestLogger(eventSender, eventArgs));
        }

        private void RequestLogger(object eventSender, ReceivingResponseEventArgs eventArgs)
        {
            // TODO : Implement logging feature
            // eventArgs.ResponseMessage.ToString();
        }

        private void RequestLogger(object eventSender, BuildingRequestEventArgs eventArgs)
        {
            // TODO : Implement logging feature
            // eventArgs.ToString();
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

        private MailAddressCollection ConvertRecipientIListToMailAddressCollection(IList<Data.Recipient> recipients)
        {
            MailAddressCollection result = new MailAddressCollection();

            foreach (var recipient in recipients)
            {
                result.Add(new MailAddress(recipient.EmailAddress.Address, recipient.EmailAddress.Name));
            }

            return result;
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