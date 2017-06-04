// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

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
            string[] tokenParts = _token.Split('.');

            // Decode headers.
            try
            {
                textBox_Header.Text = RequestForm.parseJsonResponse(Encoding.UTF8.GetString(ConvertFromBase64String(tokenParts[0])));
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
    }
}
