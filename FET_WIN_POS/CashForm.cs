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
    public partial class CashForm : Form
    {
        public CashForm()
        {
            InitializeComponent();
        }

        public int Amount
        {
            get
            {
                return int.Parse(amountTextBox.Text);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(amountTextBox, null);
            int amount;
            if (!int.TryParse(amountTextBox.Text, out amount))
            {
                errorProvider.SetError(amountTextBox, "請輸入數字");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
