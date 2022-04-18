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
    public partial class CollectionForm : Form
    {
        public CollectionForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void accountTextBox_TextChanged(object sender, EventArgs e)
        {
            mobileNumberLabel.Enabled = true;
            mobileNumberTextBox.Enabled = true;

            if (accountTextBox.Text.Length == 14)
            {
                captionLabel.Text = "遠傳電信";
                amountDueTextBox.Text = "568";
            }            
            else if (accountTextBox.Text.Length == 12)
            {
                captionLabel.Text = "和信電信";
                mobileNumberLabel.Enabled = false;
                mobileNumberTextBox.Enabled = false;
                amountDueTextBox.Text = "950";
            }
            else if (accountTextBox.Text.EndsWith("6F2", StringComparison.CurrentCultureIgnoreCase))
            {
                captionLabel.Text = "遠通電信";
                amountDueTextBox.Text = "289";
            }
            else if (accountTextBox.Text.EndsWith("021", StringComparison.CurrentCultureIgnoreCase))
            {
                captionLabel.Text = "速博電信";
                amountDueTextBox.Text = "219";
            }
            else if (accountTextBox.Text.EndsWith("010", StringComparison.CurrentCultureIgnoreCase))
            {
                captionLabel.Text = "SeedNet電信";
                amountDueTextBox.Text = "512";

            }
        }       

    }
}
