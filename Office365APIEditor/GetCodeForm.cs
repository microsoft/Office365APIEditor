using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class GetCodeForm : Form
    {
        private string redirectUrl;
        private string AuthenticationUrl;

        private string acquiredCode = "";

        public GetCodeForm(string ClientID, string RedirectUri, string ResourceUri)
        {
            InitializeComponent();

            redirectUrl = RedirectUri;
            AuthenticationUrl = "https://login.windows.net/common/oauth2/authorize?response_type=code&client_id=" + ClientID + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(RedirectUri) + "&resource=" + System.Web.HttpUtility.UrlEncode(ResourceUri) + "&prompt=login";

            webBrowser1.DocumentTitleChanged += new EventHandler(webBrowser1_DocumentTitleChanged);
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser1.Document.Title;
        }

        private void GetCodeForm_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(AuthenticationUrl);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if ((e.Url.AbsoluteUri.StartsWith(redirectUrl)) && (e.Url.AbsoluteUri.Contains("code")))
            {
                var queryString = e.Url.AbsoluteUri.Substring(e.Url.AbsoluteUri.IndexOf("?"));
                NameValueCollection temp = System.Web.HttpUtility.ParseQueryString(queryString);
                acquiredCode = temp["code"];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public DialogResult ShowDialog(out string code)
        {
            DialogResult result = this.ShowDialog();

            code = acquiredCode;
            return result;
        }
    }
}
