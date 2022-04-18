using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Advtek.Utility;

namespace EncConnectionString
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string key = this.textBox3.Text;
            string iv = this.textBox4.Text;
            string HyperText = this.textBox1.Text;
            //string test1 = DESCryptography.EncryptDES(test, key,iv);
            //string test2 = DESCryptography.DecryptDES(test1, key, iv);
            this.textBox2.Text = DESCryptography.EncryptDES(HyperText, key, iv);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string key = this.textBox3.Text;
            string iv = this.textBox4.Text;
            string HyperText = this.textBox2.Text;
            this.textBox1.Text = DESCryptography.DecryptDES(HyperText, key, iv);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";
        }
    }
}
