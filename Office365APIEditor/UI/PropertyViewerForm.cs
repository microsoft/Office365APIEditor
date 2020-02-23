// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Office365APIEditor
{
    public partial class PropertyViewerForm : Form
    {
        private string propertyName;
        private string propertyValue;

        public PropertyViewerForm(string PropertyName, string PropertyValue)
        {
            propertyName = PropertyName;
            propertyValue = PropertyValue;

            InitializeComponent();
        }

        private void PropertyViewerForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.DefaultIcon;

            Location = new Point(
            Owner.Location.X + (Owner.Width - Width) / 2,
            Owner.Location.Y + (Owner.Height - Height) / 2);

            label_PropertyName.Text = propertyName;
            textBox_PropertyValue.Text = propertyValue;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton_Original_CheckedChanged(object sender, EventArgs e)
        {
            // If "Decoded JSON" is selected, parse the value as JSON string.
            textBox_PropertyValue.Text = (radioButton_Original.Checked) ? propertyValue : RequestForm.DecodeJsonResponse(RequestForm.parseJsonResponse(propertyValue));
        }
    }
}
