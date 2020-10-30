// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Office365APIEditor.UI
{
    public partial class ErrorForm : Form
    {
        Exception exception;
        
        public ErrorForm(Exception Exception, Form OwnerWindow)
        {
            exception = Exception;
            Owner = OwnerWindow;

            StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            // Load error icon
            pictureBox_ErrorIcon.Image = SystemIcons.Error.ToBitmap();

            // Show exception message and stack trace
            textBox_Message.Text =
                exception.GetType().FullName +
                Environment.NewLine +
                Environment.NewLine +
                exception.Message +
                Environment.NewLine +
                Environment.NewLine +
                exception.StackTrace; ;

            if (exception is WebException webException)
            {
                if (webException.Response != null)
                {
                    // Show HTTP header and body

                    string jsonResponse = "";
                    using (Stream responseStream = webException.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        jsonResponse = reader.ReadToEnd();
                    }

                    HttpWebResponse response = (HttpWebResponse)webException.Response;

                    textBox_HttpResponse.Text =
                        response.Headers.ToString() +
                        jsonResponse;
                }
            }
        }
    }
}