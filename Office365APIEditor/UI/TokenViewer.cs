// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class TokenViewer : Form
    {
        bool genericMode = false;
        
        string rawIdToken = "";
        string rawAccessToken = "";
        string rawRefreshToken = "";

        public TokenViewer()
        {
            InitializeComponent();

            genericMode = true;
        }

        public TokenViewer(TokenResponse Token)
        {
            InitializeComponent();

            genericMode = false;

            rawIdToken = Token.id_token;
            rawAccessToken = Token.access_token;
            rawRefreshToken = Token.refresh_token;
        }

        private void TokenViewer_Load(object sender, EventArgs e)
        {
            textBox_IdToken.Text = rawIdToken;
            textBox_AccessToken.Text = rawAccessToken;
            textBox_RefreshToken.Text = rawRefreshToken;

            if (rawAccessToken == "" && rawIdToken == "")
            {
                textBox_IdToken.ReadOnly = false;
                textBox_IdToken.Text = "Enter the ID Token and click [Detail]";

                textBox_AccessToken.ReadOnly = false;
                textBox_AccessToken.Text = "Enter the Access Token and click [Detail]";
            }

            button_Import.Enabled = genericMode;

            Location = new System.Drawing.Point(
                Owner.Location.X + (Owner.Width - this.Width) / 2,
                Owner.Location.Y + (Owner.Height - this.Height) / 2);
        }

        private void button_AccessTokenDetail_Click(object sender, EventArgs e)
        {
            // 1. Try to decode the ID Token and the Access Token.
            // 2. Select a token and open a detail window.

            string idToken = (rawIdToken != "" && rawIdToken != null) ? rawIdToken : textBox_IdToken.Text;
            string accessToken = (rawAccessToken != "" && rawAccessToken != null) ? rawAccessToken : textBox_AccessToken.Text;

            Tuple<bool, string, string, string> decodedIdToken = TryDecodeToken(idToken);
            Tuple<bool, string, string, string> decodedAccessToken = TryDecodeToken(accessToken);

            if (decodedIdToken.Item1 == true)
            {
                if (decodedAccessToken.Item1 == true)
                {
                    // IdToken : Valid
                    // AccessToken : Valid

                    DialogResult dialogResult = MessageBox.Show("Do you want to see the detail of Access Token?" + Environment.NewLine + "If you want to see the detail of ID Token, click [no].", "Office365APIEditor", MessageBoxButtons.YesNoCancel);

                    if (dialogResult == DialogResult.Yes)
                    {
                        DetailedTokenViewer viewer = new DetailedTokenViewer(decodedAccessToken);

                        viewer.ShowDialog();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        DetailedTokenViewer viewer = new DetailedTokenViewer(decodedIdToken);

                        viewer.ShowDialog();
                    }
                }
                else
                {
                    // IdToken : Valid
                    // AccessToken : Invalid

                    DialogResult dialogResult = MessageBox.Show("You can see the detail of ID Token.", "Office365APIEditor", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.OK)
                    {
                        DetailedTokenViewer viewer = new DetailedTokenViewer(decodedIdToken);

                        viewer.ShowDialog();
                    }
                }
            }
            else
            {
                if (decodedAccessToken.Item1 == true)
                {
                    // IdToken : Invalid
                    // AccessToken : Valid

                    DialogResult dialogResult = MessageBox.Show("You can see the detail of Access Token.", "Office365APIEditor", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.OK)
                    {
                        DetailedTokenViewer viewer = new DetailedTokenViewer(decodedAccessToken);

                        viewer.ShowDialog();
                    }
                }
                else
                {
                    // IdToken : Invalid
                    // AccessToken : Invalid

                     MessageBox.Show("Decoding of both ID Token and Access Token failed.", "Office365APIEditor");
                }
            }

            DialogResult = DialogResult.None;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox_AccessToken_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_AccessToken.SelectAll();
            }
        }

        private void textBox_RefreshToken_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_RefreshToken.SelectAll();
            }
        }

        private void textBox_IdToken_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_IdToken.SelectAll();
            }
        }

        private Tuple<bool, string, string, string> TryDecodeToken(string Token)
        {
            string decodedHeader = "";
            string decodedClaim = "";
            string decodedSignature = "";
            bool result = false;

            try
            {
                string[] tokenParts = Token.Split('.');

                if (tokenParts.Length < 2)
                {
                    return new Tuple<bool, string, string, string>(false, "", "", "");
                }

                decodedHeader = RequestForm.parseJsonResponse(Encoding.UTF8.GetString(ConvertFromBase64String(tokenParts[0])));
                decodedClaim = RequestForm.parseJsonResponse(Encoding.UTF8.GetString(ConvertFromBase64String(tokenParts[1])));
                decodedSignature = BitConverter.ToString(ConvertFromBase64String(tokenParts[2]));
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return new Tuple<bool, string, string, string>(result, decodedHeader, decodedClaim, decodedSignature);
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

        private void button_Export_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string idToken = (rawIdToken != "" && rawIdToken != null) ? rawIdToken : textBox_IdToken.Text;
            string accessToken = (rawAccessToken != "" && rawAccessToken != null) ? rawAccessToken : textBox_AccessToken.Text;

            Tuple<bool, string, string, string> decodedIdToken = TryDecodeToken(idToken);
            Tuple<bool, string, string, string> decodedAccessToken = TryDecodeToken(accessToken);

            RawAndDecodedTokenInfo exportData = new RawAndDecodedTokenInfo
            {
                RawIdToken = rawIdToken,
                RawAccessToken = rawAccessToken,
                RawRefreshToken = rawRefreshToken,
                DecodedIdTokenHeader = decodedIdToken.Item2,
                DecodedIdTokenClaim = decodedIdToken.Item3,
                DecodedIdTokenSignature = decodedIdToken.Item4,
                DecodedAccessTokenHeader = decodedAccessToken.Item2,
                DecodedAccessTokenClaim = decodedAccessToken.Item3,
                DecodedAccessTokenSignature = decodedAccessToken.Item4
            };

            var runspace = RunspaceFactory.CreateRunspace();
            var pipeline = runspace.CreatePipeline();

            try
            {
                pipeline.Commands.Add(
                    new Command("Export-CliXml")
                    {
                        Parameters = {
                            new CommandParameter("InputObject", exportData),
                            new CommandParameter("Path", saveFileDialog1.FileName)
                        }
                    }
                );

                runspace.Open();
                var psObjects = pipeline.Invoke();
                runspace.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                runspace.Dispose();
            }
        }

        private void button_Import_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }


            var runspace = RunspaceFactory.CreateRunspace();
            var pipeline = runspace.CreatePipeline();

            try
            {
                pipeline.Commands.Add(
                    new Command("Import-CliXml")
                    {
                        Parameters = {
                            new CommandParameter("Path", openFileDialog1.FileName)
                        }
                    }
                );

                runspace.Open();
                var psObjects = pipeline.Invoke();
                runspace.Close();

                if (psObjects == null || psObjects.Count != 1)
                {
                    MessageBox.Show("Invalid XML Format.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        dynamic importData = psObjects[0];
                        textBox_IdToken.Text = importData.RawIdToken;
                        textBox_AccessToken.Text = importData.RawAccessToken;
                        textBox_RefreshToken.Text = importData.RawRefreshToken;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                runspace.Dispose();
            }
        }

        internal struct RawAndDecodedTokenInfo
        {
            public string RawIdToken;
            public string RawAccessToken;
            public string RawRefreshToken;
            public string DecodedIdTokenHeader;
            public string DecodedIdTokenClaim;
            public string DecodedIdTokenSignature;
            public string DecodedAccessTokenHeader;
            public string DecodedAccessTokenClaim;
            public string DecodedAccessTokenSignature;
        }
    }
}
