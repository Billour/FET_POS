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
    public partial class DiscountForm : Form
    {       
        public DiscountForm()
        {
            InitializeComponent();
        }

        private void discountAmountTextBox_TextChanged(object sender, EventArgs e)
        {            
            discountRateTextBox.Enabled = (discountAmountTextBox.Text == string.Empty);
            discountAmountTextBox.Enabled = !discountRateTextBox.Enabled;

            errorProvider.SetError(discountAmountTextBox, null);
            if (discountAmountTextBox.Text != "")
            {
                int amount;
                if (!int.TryParse(discountAmountTextBox.Text, out amount))
                {
                    errorProvider.SetError(discountAmountTextBox, "請輸入數字");
                }
            }
        }

        private void discountRateTextBox_TextChanged(object sender, EventArgs e)
        {            
            discountAmountTextBox.Enabled = (discountRateTextBox.Text == string.Empty);
            discountRateTextBox.Enabled = !discountAmountTextBox.Enabled;

            errorProvider.SetError(discountRateTextBox, null);
            if (discountRateTextBox.Text != "")
            {
                int number;
                if (!int.TryParse(discountRateTextBox.Text, out number))
                {
                    errorProvider.SetError(discountRateTextBox, "請輸入數字");
                }
            }
        }
        
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reasonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reasonComboBox.SelectedIndex > 0)
            {
                otherTextBox.Enabled = true;
            }
            else
            {
                otherTextBox.Enabled = false;
            }
        }

        private void DiscountForm_Load(object sender, EventArgs e)
        {
            reasonComboBox.SelectedIndex = 0;
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(passwordTextBox, null);
            if (passwordTextBox.Text == string.Empty)
            {
                errorProvider.SetError(passwordTextBox, "請輸入密碼");
            }
            else
            {
                passwordTextBox.ReadOnly = true;
                okButton.Enabled = true;
            }
        }
    }
}
