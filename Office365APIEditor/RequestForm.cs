using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class RequestForm : Form
    {
        bool useBasicAuth = false;
        string _accessToken = "";

        public RequestForm()
        {
            InitializeComponent();
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            StartForm startForm = new StartForm();
            if (startForm.ShowDialog(out _accessToken) == DialogResult.OK)
            {
                if (_accessToken.StartsWith("USEBASICBASIC"))
                {
                    useBasicAuth = true;
                    textBox_BasicAuthSMTPAddress.Enabled = true;
                    textBox_BasicAuthPassword.Enabled = true;
                }
                else
                {
                    useBasicAuth = false;
                    textBox_BasicAuthSMTPAddress.Enabled = false;
                    textBox_BasicAuthSMTPAddress.Text = "OAuth";
                    textBox_BasicAuthPassword.Enabled = false;
                    textBox_BasicAuthPassword.Text = "OAuth";
                    textBox_BasicAuthPassword.UseSystemPasswordChar = false;
                }
            }
            else
            {
                this.Close();
            }
        }

        private void button_Run_Click(object sender, EventArgs e)
        {
            //文字コードを指定する
            Encoding enc = Encoding.Default;

            System.Net.WebRequest req = System.Net.WebRequest.Create(textBox_Request.Text);

            if (useBasicAuth == true)
            {
                // Basic authentication
                string cred = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(textBox_BasicAuthSMTPAddress.Text + ":" + textBox_BasicAuthPassword.Text));
                req.Headers.Add("Authorization:Basic " + cred);
            }
            else
            {
                // OAuth authentication
                req.Headers.Add("Authorization:Bearer " + _accessToken);
            }

            if (radioButton_Get.Checked)
            {
                // Request is GET.
                req.Method = "GET";
                req.ContentType = "application/json";
            }
            else
            {
                // Request is POST.
                req.Method = "POST";
                req.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    string json = textBox_RequestBody.Text;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            
            try
            {
                // Change cursor.
                this.Cursor = Cursors.WaitCursor;

                //サーバーからの応答を受信するためのWebResponseを取得
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)req.GetResponse();
                //応答データを受信するためのStreamを取得
                string jsonResponse = "";
                using (System.IO.Stream resStream = res.GetResponseStream())
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(resStream, enc);
                    jsonResponse = sr.ReadToEnd();
                }

                // Display the results.
                textBox_Result.Text = "StatusCode : " + res.StatusCode.ToString() + "\r\n\r\n";
                textBox_Result.Text += "Response Header : \r\n" + res.Headers.ToString() + "\r\n\r\n";

                // JSON をパースして読みやすくする。
                dynamic parsedJson = JsonConvert.DeserializeObject(jsonResponse);
                textBox_Result.Text += "Response Body : \r\n" + JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);

                // Save application setting.
                Properties.Settings.Default.Save();
            }
            catch (System.Net.WebException ex)
            {
                textBox_Result.Text = ex.Message + "\r\n\r\nResponse Headers : \r\n" + ex.Response.Headers.ToString();
            }
            catch (Exception ex)
            {
                textBox_Result.Text = ex.Message;
            }
            finally
            {
                // Change cursor.
                this.Cursor = Cursors.Default;
            }
        }

        private void textBox_Request_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_Request.SelectAll();
            }
        }

        private void textBox_RequestBody_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_RequestBody.SelectAll();
            }
        }

        private void textBox_Result_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox_Result.SelectAll();
            }
        }
    }
}
