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
    public partial class CreditCardForm : Form
    {
        public CreditCardForm()
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

        private void wizardPage1_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            
        }

        private void wizardPage2_ShowFromNext(object sender, EventArgs e)
        {
            errorProvider.SetError(amountTextBox, null);
            int amount;
            if (!int.TryParse(amountTextBox.Text, out amount))
            {
                errorProvider.SetError(amountTextBox, "請輸入數字");
                wizard1.BackTo(wizardPage1);
                return;
            }
        }

        private void wizardPage3_CloseFromNext(object sender, Gui.Wizard.PageEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
