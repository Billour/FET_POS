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
    public partial class CallDetailRecordForm : Form
    {
        public CallDetailRecordForm()
        {
            InitializeComponent();
        }

        private void CallDetailRecordForm_Load(object sender, EventArgs e)
        {
            CalculateDateDifference();
        }

        private void CalculateDateDifference()
        {
            daysTextBox.Text = endDateTimePicker.Value.Subtract(startDateTimePicker.Value).Days.ToString();
        }

        private void CalculateTotalAmount()
        {
           
            TimeSpan diff = endDateTimePicker.Value.Subtract(startDateTimePicker.Value);
            totalAmountTextBox.Text = (60 + 60 * int.Parse(daysTextBox.Text)).ToString();
        }

        private void startDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            CalculateDateDifference();
        }

        private void endDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            CalculateDateDifference();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void daysTextBox_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmount();
        }
    }
}
