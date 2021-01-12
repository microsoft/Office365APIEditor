// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

using System;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor.UI.AccessTokenWizard
{
    public partial class PreAcquiredAccessTokenSettingPage : UserControl, IAccessTokenWizardPage
    {
        public PreAcquiredAccessTokenSettingPage()
        {
            InitializeComponent();
        }

        public void NextButtonAction()
        {
            if (ValidateAccessTokenParam())
            {
                TokenResponse tokenResponse = new TokenResponse();
                tokenResponse.access_token = textBox_AccessToken.Text.Trim();

                AccessTokenWizardForm wizard = (AccessTokenWizardForm)Parent;
                wizard.CloseWizard(new ClientInformation(tokenResponse, AuthEndpoints.PreAcquired, Resources.None, "", "", "", ""));
            }
        }

        private bool ValidateAccessTokenParam()
        {
            string token = textBox_AccessToken.Text.Trim();

            if (token == "")
            {
                MessageBox.Show("Enter the access token.", "Office365APIEditor");
                return false;
            }
            else
            {
                string[] tokenParts = token.Split('.');

                if (tokenParts.Length < 2)
                {
                    MessageBox.Show("Access token format is invalid.", "Office365APIEditor");
                    return false;
                }
                else
                {
                    try
                    {
                        string decodedHeader = RequestForm.parseJsonResponse(Encoding.UTF8.GetString(ConvertFromBase64String(tokenParts[0])));
                        string decodedClaim = RequestForm.parseJsonResponse(Encoding.UTF8.GetString(ConvertFromBase64String(tokenParts[1])));
                        string decodedSignature = BitConverter.ToString(ConvertFromBase64String(tokenParts[2]));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Access token format is invalid.", "Office365APIEditor");
                        return false;
                    }
                }
            }

            return true;
        }

        private byte[] ConvertFromBase64String(string Data)
        {
            string temp = Data.Replace('-', '+').Replace('_', '/');

            switch (temp.Length % 4)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    temp += "==";
                    break;
                case 3:
                    temp += "=";
                    break;
                default:
                    throw new Exception();
            }

            return Convert.FromBase64String(temp);
        }
    }
}