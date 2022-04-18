using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_ACL_RoleMaintain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    /// <summary>
    /// 依查詢條件取得Role資料，並將它存於ViewState
    /// </summary>
    /// <param name="RoleCode"></param>
    /// <param name="RoleName"></param>
    /// <param name="Message"></param>
    private void bindRoleData(string RoleCode, string RoleName, out string Message)
    {
        Message = string.Empty;
        DataTable dtRoleData = new DataTable();
        //取得資料
        dtRoleData = genRoleDataTemp();
        //存入ViewState
        ViewState["grdRoleData"] = dtRoleData;
        bindRoleData();
    }

    /// <summary>
    /// 將存在ViewState的資料重Bind至GridView
    /// </summary>
    private void bindRoleData()
    {
        DataTable dtRoleData = new DataTable();
        dtRoleData = ViewState["grdRoleData"] as DataTable ?? null;
        grdRoleData.DataSource = dtRoleData;
        grdRoleData.DataBind();
    }

    /// <summary>
    /// 建立假資料 (暫時)
    /// </summary>
    /// <returns></returns>
    private DataTable genRoleDataTemp()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ROLENO", typeof(string));
        dt.Columns.Add("ROLECODE", typeof(string));
        dt.Columns.Add("ROLENAME", typeof(string));
        dt.Columns.Add("ROLEMEMO", typeof(string));

        //建10筆資料
        for (int ii = 0; ii < 10; ii++)
        {
            DataRow row = dt.NewRow();
            row["ROLENO"] = ii;
            row["ROLECODE"] = "角色代碼" + ii;
            row["ROLENAME"] = "角色名稱" + ii;
            row["ROLEMEMO"] = "說明" + ii;
            dt.Rows.Add(row);
        }
        return dt;
    }



    #region 查詢/新增/清除 按鍵 觸發事件
    /// <summary>
    /// 查詢按鍵
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string RoleCode = string.Empty;
        string RoleName = string.Empty;
        string Message = string.Empty;

        RoleCode = tbRoleCode.Text.Trim();
        RoleName = tbRoleName.Text.Trim();

        #region 驗証資料
        //驗証
        #endregion

        //取得資料
        bindRoleData(RoleCode, RoleName, out Message);


        divNewBlock.Style["display"] = "none";
        divDataBlock.Style["display"] = "";


    }

    /// <summary>
    /// 清除按鍵
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        tbRoleCode.Text = "";
        tbRoleName.Text = "";
    }

    /// <summary>
    /// 新增按鍵
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        divNewBlock.Style["display"] = "";
        divDataBlock.Style["display"] = "none";
    }
    #endregion


    #region 新增 確定/取消鍵 觸發事件
    /// <summary>
    /// 確定新增按鍵
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewOK_Click(object sender, EventArgs e)
    {
        string RoleCode = string.Empty;
        string RoleName = string.Empty;
        string RoleMemo = string.Empty;
        string Message = string.Empty;

        RoleCode = tbNewRoleCode.Text.Trim();
        RoleName = tbNewRoleName.Text.Trim();
        RoleMemo = tbNewRoleMemo.Text.Trim();

        #region 驗証資料
        #endregion

        //新增至資料庫

        //將畫面回復至查詢狀態
        divNewBlock.Style["display"] = "none";
        divDataBlock.Style["display"] = "";
        grdRoleData.DataSource = null;
        grdRoleData.DataBind();
    }

    /// <summary>
    /// 新增下的取消按鍵
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewCancel_Click(object sender, EventArgs e)
    {
        //將畫面回復至查詢狀態
        divNewBlock.Style["display"] = "none";
        divDataBlock.Style["display"] = "";
        grdRoleData.DataSource = null;
        grdRoleData.DataBind();
    }
    #endregion



    #region GridView相關 觸發事件
    /// <summary>
    /// 編輯
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdRoleData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        //設定編輯欄位
        gridview.EditIndex = e.NewEditIndex;

        //Bind原查詢資料
        bindRoleData();

    }

    /// <summary>
    /// 刪除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdRoleData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //取得資料

        //更新資料庫

        //Bind資料
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdRoleData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView gridview = sender as GridView;

        //取得資料

        //更新資料庫

        //取消編輯狀態
        gridview.EditIndex = -1;

        //Bind查詢條件資料(重取資料)
        string RoleCode = string.Empty;
        string RoleName = string.Empty;
        string Message = string.Empty;
        RoleCode = tbRoleCode.Text.Trim();
        RoleName = tbRoleName.Text.Trim();
        bindRoleData(RoleCode, RoleName, out Message);
    }

    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdRoleData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gridview = sender as GridView;
        gridview.EditIndex = -1;
        //Bind原查詢資料
        bindRoleData();
    }
    #endregion
}