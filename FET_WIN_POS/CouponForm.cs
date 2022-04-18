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
    public partial class CouponForm : Form
    {
        public CouponForm()
        {
            InitializeComponent();
        }

        public string CouponCode
        {
            get
            {
                return couponCodeTextBox.Text;
            }
        }

        public int CouponAmount
        {
            get
            {
                return int.Parse(couponAmountTextBox.Text);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(couponAmountTextBox, null);
            int amount;
            if (!int.TryParse(couponAmountTextBox.Text, out amount)) 
            {
                errorProvider.SetError(couponAmountTextBox, "請輸入數字");
                return;
            }
           
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
