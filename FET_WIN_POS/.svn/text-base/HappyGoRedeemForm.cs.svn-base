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
    public partial class HappyGoRedeemForm : Form
    {
        public HappyGoRedeemForm()
        {
            InitializeComponent();            
        }

        public void HideControls()
        {
            label5.Visible = false;
            label6.Visible = false;
            listViewEx1.Visible = false;
            listViewEx2.Visible = false;
        }

        private void HappyGoRedeemForm_Load(object sender, EventArgs e)
        {
            TextBox tb = new TextBox();
            tb.Text = "0";
            listViewEx1.AddEmbeddedControl(tb, 3, 0);

            TextBox tb1 = new TextBox();
            tb1.Text = "0";
            listViewEx2.AddEmbeddedControl(tb1, 3, 0);

            TextBox tb2 = new TextBox();
            tb2.Text = "0";
            listViewEx2.AddEmbeddedControl(tb2, 3, 1);


            TextBox tb3 = new TextBox();
            tb3.Text = "0";
            listViewEx3.AddEmbeddedControl(tb3, 2, 0);

            TextBox tb4 = new TextBox();
            tb4.Text = "0";
            listViewEx3.AddEmbeddedControl(tb4, 2, 1);

            TextBox tb5 = new TextBox();
            tb5.Text = "0";
            listViewEx3.AddEmbeddedControl(tb5, 2, 2);

            TextBox tb6 = new TextBox();
            tb6.Text = "0";
            listViewEx3.AddEmbeddedControl(tb6, 2, 3);
        }
    }
}
