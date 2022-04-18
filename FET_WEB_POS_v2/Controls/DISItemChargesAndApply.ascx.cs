using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class DISItemChargesAndApply : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            if (Session["RatePlan"] != null)
            {
                DataTable dt = Session["RatePlan"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (Control cl1 in this.Controls)
                    {
                        string strClType = cl1.GetType().Name;
                        if (strClType == "CheckBoxList")
                        {
                            CheckBoxList cbl1 = (CheckBoxList)cl1;

                            foreach (ListItem lt1 in cbl1.Items)
                            {
                                if (dr["M_TYPE"].ToString() == lt1.Value)
                                {
                                    lt1.Selected = (dr["VALUE"].ToString() == "Y" ? true : false);
                                }
                            }//end-foreach (ListItem lt1 in cbl1.Items)S
                        }//end-if (strClType == "CheckBoxList")
                    }//end-foreach (Control cl1 in this.Controls)
                }//end-foreach (DataRow dr in dt.Rows)
            }//end-if (Session["RatePlan"] != null)
        }
    }

    public void Clear()
    {
        foreach (Control cl1 in this.Controls)
        {
            string strClType = cl1.GetType().Name;
            if (strClType == "CheckBoxList")
            {
                CheckBoxList cbl1 = (CheckBoxList)cl1;
                cbl1.ClearSelection();

            }
            if (strClType == "ASPxCheckBox")
            {
                ASPxCheckBox cbl1 = (ASPxCheckBox)cl1;
                cbl1.Checked = false;
            }
        }

        //CheckBoxList[] chkList = new CheckBoxList[] { cbRate, cbGAG1, cbGAG2, cbLoyalty, cbLoyalty, cb223G1, cb223G2, cbMNP };

        //foreach (CheckBoxList list in chkList)
        //{
        //    foreach (ListItem item in list.Items)
        //    {
        //        item.Selected = false;
        //    }
        //}

        //this.cb223All.Checked = false;
        //this.cbGAAll.Checked = false;
    }

    public bool Enabled
    {
        set {
            foreach (Control cl1 in this.Controls)
            {
                string strClType = cl1.GetType().Name;
                if (strClType == "CheckBoxList")
                {
                    CheckBoxList cbl1 = (CheckBoxList)cl1;

                    foreach (ListItem lt1 in cbl1.Items)
                    {
                        lt1.Enabled = value;
                    }//end-foreach (ListItem lt1 in cbl1.Items)
                }//end-if (strClType == "CheckBoxList")
            }//end-foreach (Control cl1 in this.Controls)

            this.cb223All.Enabled = value;
            this.cbGAAll.Enabled = value;

        }
    }
}
