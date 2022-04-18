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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            TransactionNoTextBox.Text = rnd.Next(1000, 9999).ToString() + "-"
                + rnd.Next(100000, 999999).ToString()
                + rnd.Next(100000, 999999).ToString();
            uxTransactionDate.Text = DateTime.Now.ToString();
            statusLabel.Text = "00-未存檔";            
            /*
            // Create three items and three sets of subitems for each item.
            ListViewItem item1 = new ListViewItem();
            // Place a check mark next to the item.
            item1.SubItems.Add("促");
            item1.SubItems.Add("KJ489593");
            item1.SubItems.Add("哈拉900方案 -NOKIA 5800手機");
            item1.SubItems.Add("1");
            item1.SubItems.Add("6500");
            item1.SubItems.Add("6500");
            item1.SubItems.Add("YU1234567");
            ListViewItem item2 = new ListViewItem();
            item2.SubItems.Add("單");
            item2.SubItems.Add("UT3827494");
            item2.SubItems.Add("無線網卡");
            item2.SubItems.Add("1");
            item2.SubItems.Add("1290");
            */
            /*
            ListViewItem item3 = new ListViewItem();
            // Place a check mark next to the item.
            item3.Checked = true;
            item3.SubItems.Add("7");
            item3.SubItems.Add("8");
            item3.SubItems.Add("9");
            */
           // listView1.Items.AddRange(new ListViewItem []{ item1, item2 });


        }
        /// <summary>
        /// 組合銷售
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedSalesButton_Click(object sender, EventArgs e)
        {
            CombinedSalesForm form = new CombinedSalesForm();
            form.ShowDialog(this);
        }
        
        private void CollectionButton_Click(object sender, EventArgs e)
        {
            CollectionForm form = new CollectionForm();
            form.ShowDialog(this);
        }

        private void CashButton_Click(object sender, EventArgs e)
        {
            CashForm form = new CashForm();
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add("現金");
                li.SubItems.Add(form.Amount.ToString());
                li.SubItems.Add("");
                listView3.Items.Add(li);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (listView1.GetLastItem() != null && listView1.GetLastItem().SubItems[1].Text == string.Empty)
                return;

            ListViewItem item1 = new ListViewItem();            
            TextBox tb = new TextBox();
            tb.BorderStyle = BorderStyle.None;   
            tb.Width = listView1.Columns[2].Width - 10;    
            tb.KeyDown += new KeyEventHandler(tb_KeyDown);
            listView1.Items.Add(item1);
            listView1.AddEmbeddedControl(tb, 2, listView1.Items.Count-1);
            tb.Focus();
        }
        
        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                TextBox tb = sender as TextBox;
                if (tb.Text.Trim() == string.Empty)
                    return;

                ListViewItem li = listView1.Items[listView1.Items.Count - 1];
                li.SubItems.Add("促");
                li.SubItems.Add("KJ489593");
                li.SubItems.Add("哈拉900方案 -NOKIA 5800手機");
                li.SubItems.Add("1");
                li.SubItems.Add("6500");
                li.SubItems.Add("6500");
                li.SubItems.Add("YU1234567");                             
                tb.ReadOnly = true;
                addButton.Focus();

                int amountDue = int.Parse(amountDueTextBox.Text);
                amountDueTextBox.Text = (amountDue + 6500).ToString();
                 
            }
            else if (e.KeyData == Keys.Escape)
            {
                listView1.GetLastItem().Remove();                
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {           
            if (e.KeyData == Keys.F2)
            {                
                if (contextMenuStrip1.Items.Count == 0)
                    contextMenuStrip1.Items.AddRange(serviceItemMenu.DropDownItems.ToArray<ToolStripMenuItem>());
                contextMenuStrip1.Show((ClientRectangle.Width - contextMenuStrip1.Width) / 2, (ClientRectangle.Height - contextMenuStrip1.Height) / 2);
            }
            if (e.KeyData == Keys.F3) // 客戶服務
            {
                if (contextMenuStrip2.Items.Count == 0)
                contextMenuStrip2.Items.AddRange(customerServiceMenu.DropDownItems.ToArray<ToolStripMenuItem>());
                contextMenuStrip2.Show((ClientRectangle.Width - contextMenuStrip2.Width) / 2, (ClientRectangle.Height - contextMenuStrip2.Height) / 2);
            }
            if (e.KeyData == Keys.F4)
            {
                if (contextMenuStrip3.Items.Count == 0)
                contextMenuStrip3.Items.AddRange(serviceTransitionMenu.DropDownItems.ToArray<ToolStripMenuItem>());
                contextMenuStrip3.Show((ClientRectangle.Width - contextMenuStrip3.Width) / 2, (ClientRectangle.Height - contextMenuStrip3.Height) / 2);
            }
            if (e.KeyData == Keys.F5)
            {
                if (contextMenuStrip4.Items.Count == 0)
                contextMenuStrip4.Items.AddRange(leaseMenu.DropDownItems.ToArray<ToolStripMenuItem>());
                contextMenuStrip4.Show((ClientRectangle.Width - contextMenuStrip4.Width) / 2, (ClientRectangle.Height - contextMenuStrip4.Height) / 2);
            }
            else if (e.KeyData == Keys.Escape)
            {
                //contextMenuStrip4.Hide();
            }
        }

        private void checkOutButton_Click(object sender, EventArgs e)
        {

        }

        private void discountButton_Click(object sender, EventArgs e)
        {
            DiscountForm form = new DiscountForm();
            form.ShowDialog(this);
        }

        private void HappyGoRedeemButton_Click(object sender, EventArgs e)
        {
            HappyGoRedeemForm form = new HappyGoRedeemForm();
            form.ShowDialog(this);
        }

        private void happyGoButton_Click(object sender, EventArgs e)
        {
            HappyGoRedeemForm form = new HappyGoRedeemForm();
            form.HideControls();
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add("HappyGo");
                li.SubItems.Add("300");
                li.SubItems.Add("兌換點數：540點");
                listView3.Items.Add(li);
            }
        }

        private void couponButton_Click(object sender, EventArgs e)
        {
            CouponForm form = new CouponForm();
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem();                   
                li.SubItems.Add("禮券");
                li.SubItems.Add(form.CouponAmount.ToString());
                li.SubItems.Add("禮券號碼：" + form.CouponCode);
                listView3.Items.Add(li);       
            }
        }

        private void RemoveStatementLineItemButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listView3.Items)
            {
                if (li.Checked)
                {
                    li.Remove();
                }
            }
        }

        private void CreditCardButton_Click(object sender, EventArgs e)
        {
            CreditCardForm form = new CreditCardForm();
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add("信用卡");
                li.SubItems.Add(form.Amount.ToString());
                li.SubItems.Add("信用卡號碼：" + form.CreditCardNumber);
                listView3.Items.Add(li);   
            }
        }

        private void offlineCreditCardButton_Click(object sender, EventArgs e)
        {
            OfflineCreditCardForm form = new OfflineCreditCardForm();
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add("離線信用卡");
                li.SubItems.Add(form.Amount.ToString());
                li.SubItems.Add("信用卡號碼：" + form.CreditCardNumber);
                listView3.Items.Add(li);
            }
        }

        private void installmentButton_Click(object sender, EventArgs e)
        {
            InstallmentForm form = new InstallmentForm();
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add("分期付款");
                li.SubItems.Add(form.Amount.ToString());
                li.SubItems.Add("信用卡號碼：" + form.CreditCardNumber);
                listView3.Items.Add(li);
            }
        }

        private void bankCardButton_Click(object sender, EventArgs e)
        {
            BankCardForm form = new BankCardForm();
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add("金融卡");
                li.SubItems.Add(form.Amount.ToString());
                li.SubItems.Add("");
                listView3.Items.Add(li);
            }
        }

        private void RemoveSalesLineItemButton_Click(object sender, EventArgs e)
        {            
            foreach (ListViewItem li in listView1.Items)
            {
                if (li.Checked)
                {
                    li.Remove();
                }
            }

        }
        
    }
}
