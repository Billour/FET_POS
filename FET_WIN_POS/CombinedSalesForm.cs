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
    public partial class CombinedSalesForm : Form
    {
        public CombinedSalesForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                        
            for (int i = 0; i < 3; i++)
            {
                RadioButton rb = new RadioButton();
                rb.Padding = new Padding(3);
                rb.AutoSize = true;      
                listViewEx1.AddEmbeddedControl(rb, 0, i);                
            }

            for (int i = 0; i < 6; i++)
            {
                ComboBox cb = new ComboBox();
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                cb.Items.AddRange(new string[] {"請選擇", "98896123", "98806553" });
                cb.SelectedIndex = 0;      
                listViewEx2.AddEmbeddedControl(cb, 1, i);                
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
    }
}
