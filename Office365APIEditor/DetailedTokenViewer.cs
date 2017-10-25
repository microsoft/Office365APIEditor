// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Codeplex.Data;
using System;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class DetailedTokenViewer : Form
    {
        string _token = "";

        public DetailedTokenViewer(string Token)
        {
            InitializeComponent();

            _token = Token;
        }

        private void DetailedTokenViewer_Load(object sender, EventArgs e)
        {
            // Disable advanced view.
            textBox_Header.Visible = true;
            dataGridView_Header.Visible = false;
            textBox_Claim.Visible = true;
            dataGridView_Claim.Visible = false;
            
            string[] tokenParts = _token.Split('.');

            // Decode headers.
            try
            {
                textBox_Header.Text = RequestForm.parseJsonResponse(Encoding.UTF8.GetString(ConvertFromBase64String(tokenParts[0])));
                PrepareDataGridView(dataGridView_Header, textBox_Header.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("Enter a valid Access Token. Or you are using Token ID.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }            

            if (tokenParts.Length == 1)
            {
                MessageBox.Show("Enter a valid Access Token. Or you are using Token ID.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            else
            {
                // Decode claims.
                textBox_Claim.Text = RequestForm.parseJsonResponse(Encoding.UTF8.GetString(ConvertFromBase64String(tokenParts[1])));
                PrepareDataGridView(dataGridView_Claim, textBox_Claim.Text);

                // Decode signature.
                textBox_Signature.Text = BitConverter.ToString(ConvertFromBase64String(tokenParts[2]));
            }
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
            long secondsAfterBase;

            if (long.TryParse(Seconds, out secondsAfterBase))
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
