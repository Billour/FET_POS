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
    public partial class ECardTopUpForm : Form
    {
        public ECardTopUpForm()
        {
            InitializeComponent();
        }

        private void eCardTopUpForm_Load(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void amountTextBox_TextChanged(object sender, EventArgs e)
        {
            int amount;

            errorProvider.SetError(amountTextBox, null);

            if (!int.TryParse(amountTextBox.Text, out amount))
            {
                errorProvider.SetError(amountTextBox, "請輸入數字");
            }
            else if (amount < 300)
            {
                errorProvider.SetError(amountTextBox, "儲值金額最低300元");
            }
            else if (amount > 10000)
            {
                errorProvider.SetError(amountTextBox, "儲值金額最高1000元");
            }
        }
    }
}
