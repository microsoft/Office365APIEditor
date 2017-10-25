// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class TokenViewer : Form
    {
        string rawAccessToken = "";
        string rawRefreshToken = "";

        public TokenViewer()
        {
            InitializeComponent();
        }

        public TokenViewer(TokenResponse Token)
        {
            InitializeComponent();

            rawAccessToken = Token.access_token;
            rawRefreshToken = Token.refresh_token;
        }

        private void TokenViewer_Load(object sender, EventArgs e)
        {
            textBox_AccessToken.Text = rawAccessToken;
            textBox_RefreshToken.Text = rawRefreshToken;

            if (rawAccessToken == "")
            {
                textBox_AccessToken.ReadOnly = false;
                textBox_AccessToken.Text = "Enter Access Token and click [Detail]";
            }

            Location = new System.Drawing.Point(
                Owner.Location.X + (Owner.Width - this.Width) / 2,
                Owner.Location.Y + (Owner.Height - this.Height) / 2);
        }

        private void button_AccessTokenDetail_Click(object sender, EventArgs e)
        {
            if (rawAccessToken == "")
            {
                try
                {
                    DetailedTokenViewer viewer = new DetailedTokenViewer(textBox_AccessToken.Text);

                    viewer.ShowDialog();
                }
                catch (Exception)
                {
                    MessageBox.Show("Enter a valid Access Token.", "Office365APIEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DetailedTokenViewer viewer = new DetailedTokenViewer(rawAccessToken);

                viewer.ShowDialog();
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
    }
}
