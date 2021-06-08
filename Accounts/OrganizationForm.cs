using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Accounts
{
    public partial class OrganizationForm : Form
    {
        public OrganizationForm()
        {
            InitializeComponent();
        }

        public string OrganizationName
        {
            get => nameTextBox.Text;
            set => nameTextBox.Text = value;
        }
    }
}
