using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class TokenViewer : Form
    {
        string rawAccessToken = "";
        string rawRefreshToken = "";

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
        }

        private void button_AccessTokenDetail_Click(object sender, EventArgs e)
        {
            DetailedTokenViewer viewer = new DetailedTokenViewer(rawAccessToken);

            viewer.ShowDialog();

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
