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
    public partial class OfflineCreditCardForm : Form
    {
        public OfflineCreditCardForm()
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

        public string CreditCardNumber
        {
            get
            {
                return "1111-2222-3333-4444";
            }
        }

        public string AuthorizationCode
        {
            get
            {
                return "5055";
            }
        }

        private void okButton_Click(object sender, EventArgs e)
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OfflineCreditCardForm_Load(object sender, EventArgs e)
        {

        }
    }
}
