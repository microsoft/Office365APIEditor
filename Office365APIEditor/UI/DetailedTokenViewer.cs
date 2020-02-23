// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Codeplex.Data;
using System;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class DetailedTokenViewer : Form
    {        
        string decodedHeader = "";
        string decodedClaim = "";
        string decodedSignature = "";

        public DetailedTokenViewer(Tuple<bool, string, string, string> DecodedToken)
        {
            InitializeComponent();

            decodedHeader = DecodedToken.Item2;
            decodedClaim = DecodedToken.Item3;
            decodedSignature = DecodedToken.Item4;
        }

        private void DetailedTokenViewer_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            // Disable advanced view.
            textBox_Header.Visible = true;
            dataGridView_Header.Visible = false;
            textBox_Claim.Visible = true;
            dataGridView_Claim.Visible = false;

            textBox_Header.Text = decodedHeader;
            PrepareDataGridView(dataGridView_Header, textBox_Header.Text);

            textBox_Claim.Text = decodedClaim;
            PrepareDataGridView(dataGridView_Claim, textBox_Claim.Text);

            textBox_Signature.Text = decodedSignature; ;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox_Header_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_Header.SelectAll();
            }
        }

        private void textBox_Claim_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_Claim.SelectAll();
            }
        }

        private void textBox_Signature_KeyDown(object sender, KeyEventArgs e)
        {
            // Enable 'Ctrl + A'
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_Signature.SelectAll();
            }
        }

        private void checkBox_AdvancedView_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Header.Visible = !checkBox_AdvancedView.Checked;
            dataGridView_Header.Visible = checkBox_AdvancedView.Checked;
            textBox_Claim.Visible = !checkBox_AdvancedView.Checked;
            dataGridView_Claim.Visible = checkBox_AdvancedView.Checked;
        }

        private void PrepareDataGridView(DataGridView dataGridView, string jsonString)
        {
            var obj = DynamicJson.Parse(jsonString);

            foreach (System.Collections.Generic.KeyValuePair<string, object> item in obj)
            {
                string description = "";
                string value = "";

                switch (item.Key)
                {
                    case "aud":
                        description = "Audience of the token";
                        value = item.Value.ToString();
                        break;
                    case "iss":
                        description = "Identifies the token issuer";
                        value = item.Value.ToString();
                        break;
                    case "iat":
                        description = "Issued at time";
                        value = CalculateDate(item.Value.ToString());
                        break;
                    case "nbf":
                        description = "Not before time";
                        value = CalculateDate(item.Value.ToString());
                        break;
                    case "exp":
                        description = "Expiration time";
                        value = CalculateDate(item.Value.ToString());
                        break;
                    case "tid":
                        description = "Tenant identifier (ID) of the Azure AD tenant that issued the token.";
                        value = item.Value.ToString();
                        break;
                    case "ver":
                        description = "Version";
                        value = item.Value.ToString();
                        break;
                    case "oid":
                        description = "Object identifier (ID) of the user object in Azure AD";
                        value = item.Value.ToString();
                        break;
                    case "sub":
                        description = "Token subject identifier";
                        value = item.Value.ToString();
                        break;
                    case "upn":
                        description = "User principal name of the user";
                        value = item.Value.ToString();
                        break;
                    case "typ":
                        description = "Type";
                        value = item.Value.ToString();
                        break;
                    case "alg":
                        description = "Algorithm";
                        value = item.Value.ToString();
                        break;
                    case "x5t":
                        description = "X.509 certificate SHA-1 Thumbprint";
                        value = item.Value.ToString();
                        break;
                    case "kid":
                        description = "Key ID";
                        value = item.Value.ToString();
                        break;
                    default:
                        description = "";
                        value = item.Value.ToString();
                        break;
                }

                dataGridView.Rows.Add(item.Key, description, value);

                dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private string CalculateDate(string Seconds)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            if (long.TryParse(Seconds, out long secondsAfterBase))
            {
                return baseDate.AddSeconds(secondsAfterBase).ToString("yyyy/MM/dd HH:mm:ss") + " (UTC)";
            }
            else
            {
                return Seconds;
            }
        }
    }
}
