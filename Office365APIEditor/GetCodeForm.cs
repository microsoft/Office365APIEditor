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

        }

        private void GetCodeForm_Load(object sender, EventArgs e)
        {
            var ua = string.Format("User-Agent: {0}",
                "Mozilla/5.0 (Windows Phone 10.0;  Android 4.2.1; Nokia; Lumia 520) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Mobile Safari/537.36 Edge/12.0");
            webBrowser1.Navigate(AuthenticationUrl, null, null, ua);
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
