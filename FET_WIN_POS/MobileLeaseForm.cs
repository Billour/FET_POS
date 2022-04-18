using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FetPos
{
    public partial class MobileLeaseForm : Form
    {
        public MobileLeaseForm()
        {
            InitializeComponent();
        }

        private void MobileLeaseForm_Load(object sender, EventArgs e)
        {

        }

        private void daysTextBox_TextChanged(object sender, EventArgs e)
        {
            errorProvider.SetError(daysTextBox, null);

            int days;
            if (int.TryParse(daysTextBox.Text, out days))
            {
                totalAmountTextBox.Text = (days * 100).ToString();
            }
            else
            {
                errorProvider.SetError(daysTextBox, "請填寫數字");
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
