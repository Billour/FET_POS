<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV11.aspx.cs" Inherits="VSS_INV_INV11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <script type="text/javascript" language="javascript">
     function Call_DetectPrinterName()
     {
         //ctl00$MainContentPlaceHolder$PrintName

         var PrinterNAME = window.document.getElementById("ctl00_MainContentPlaceHolder_PrintName").value;
        // var ReceiptPrinterNAME = window.document.getElementById("ctl00_MainContentPlaceHolder_ReceiptPrintName").value;

         var oBarcodePrint = new ActiveXObject("detctPrinter.detect");
         var result = oBarcodePrint.detectP(PrinterNAME);
         if (result == "")
         {

             alert("印表機名稱有誤!請重新確認");
             // e.processOnServer = false;
         }
         else
         {
             window.document.getElementById("ctl00_MainContentPlaceHolder_PrintName").value = result;
             //this.PrintName.SetText = result;

         }
        

         //    alert(window.document.getElementById("ctl00_MainContentPlaceHolder_PrintName").value);
     }
    
    
    </script>
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function CalcuDiffStkQty(s, e) {
            var fName = "7_txtStkchkQty";
            var txtStkQty = getClientInstance('TextBox', s.name.replace(fName, "6_lbStkQty"));
            var lblDiffStkQty = getClientInstance('TextBox', s.name.replace(fName, "8_lblDiffStkQty"));
            
            //txtStkQty = clbStkQty;
            //lblDiffStkQty = clblDiffStkQty;

            var StkchkQty = s.GetText();
            var iStkchkQty = 0;
//            if (StkchkQty == null || StkchkQty == "") {
//                iStkchkQty = 0;
//                e.isValid = false;
//                e.errorText = '請輸入盤點數量';
//                //alert('請輸入盤點數量');
//                return false;
//            }

            iStkchkQty = Number(StkchkQty);
            if (isNaN(iStkchkQty)) {
                e.isValid = false;
                e.errorText = '輸入字串不符合數字格式，請重新輸入';
                //alert('輸入字串不符合數字格式，請重新輸入');
                return false;
            }
            else if (iStkchkQty < 0) {
                e.isValid = false;
                e.errorText = '門市盤點量不允許小於0，請重新輸入';
                //alert('門市盤點量不允許小於0，請重新輸入');
                return false;
            }

            var iStkQty = txtStkQty.GetText();
            var Diff = Number(iStkQty) - Number(StkchkQty);
            if (StkchkQty != "") {
                lblDiffStkQty.SetText(Diff);
            }
            e.isValid = true;
            return true;
        }

        function checkStkQty(s, e) {

            if (!CalcuDiffStkQty(s, e)) return false;

            var fName = "7_txtStkchkQty";
            var txtStkQty = getClientInstance('TextBox', s.name.replace(fName, "6_lbStkQty"));
            var lblDiffStkQty = getClientInstance('TextBox', s.name.replace(fName, "8_lblDiffStkQty"));

            //txtStkQty = clbStkQty;
            //lblDiffStkQty = clblDiffStkQty;

            var StkchkQty = s.GetValue();
            var iStkchkQty = 0;
            if (StkchkQty == null || StkchkQty == "") {
                iStkchkQty = 0;
            }

            var iStkQty = txtStkQty.GetValue();
            var Diff = Number(iStkQty) - Number(StkchkQty);

            if (Diff != 0 || StkchkQty == null) {
                //alert("數量不符，請確認");
                var rtn = confirm("盤點數量與庫存數量不符，是否確定儲存？");
                if (!rtn) {
                    var lbStkchkQty = getClientInstance('TextBox', s.name.replace(fName, "7_lbStkchkQty"));
                    s.SetText(lbStkchkQty.GetText());
                    Diff = Number(iStkQty) - Number(StkchkQty);
                }

                return rtn;
            }
            lblDiffStkQty.SetText(Diff);
            return true;
        }

        function Import(s, e) {
            var rtn = confirm('請確認是否要刪除?');
            if (!rtn) {
                e.processOnServer = false;
            }
        }

        function sSave(str) {
            var msgw, msgh, bordercolor;
            msgw = 100; //提示窗口的寬度
            msgh = 50; //提示窗口的高度
            titleheight = 25 //提示窗口標題高度
            bordercolor = "#336699"; //提示窗口的邊框顏色
            titlecolor = "#99CCFF"; //提示窗口的標題顏色

            var sWidth, sHeight;
            sWidth = document.body.offsetWidth;
            sHeight = screen.height;

            var bgObj = document.createElement("div");
            bgObj.setAttribute('id', 'bgDiv');
            bgObj.style.position = "absolute";
            bgObj.style.top = "0";
            bgObj.style.background = "#777";
            bgObj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";
            bgObj.style.opacity = "0.6";
            bgObj.style.left = "0";
            bgObj.style.width = sWidth + "px";
            bgObj.style.height = sHeight + "px";
            bgObj.style.zIndex = "10000";
            document.body.appendChild(bgObj);

            var msgObj = document.createElement("div")
            msgObj.setAttribute("id", "msgDiv");
            msgObj.setAttribute("align", "center");
            msgObj.style.background = "white";
            msgObj.style.border = "1px solid " + bordercolor;
            msgObj.style.position = "absolute";
            msgObj.style.left = "50%";
            msgObj.style.top = "50%";
            msgObj.style.font = "12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif";
            msgObj.style.marginLeft = "-225px";
            msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";
            msgObj.style.width = msgw + "px";
            msgObj.style.height = msgh + "px";
            msgObj.style.textAlign = "center";
            msgObj.style.lineHeight = "25px";
            msgObj.style.zIndex = "10001";

            document.body.appendChild(msgObj);
            var txt = document.createElement("p");
            txt.style.margin = "1em 0"
            txt.setAttribute("id", "msgTxt");
            txt.innerHTML = str;
            document.getElementById("msgDiv").appendChild(txt);
        }
        
        function eSave(str) {
            var bgObj = document.getElementById("bgDiv")
            var msgObj = document.getElementById("msgDiv")
            if (bgObj != null) {
                document.body.removeChild(bgObj);
            }
            if (msgObj != null) {
                document.body.removeChild(msgObj);
            }
            if (str != "")
                alert(str);
        }

        function onRowSave(s, e) {      
           // if (ASPxClientEdit.ValidateEditorsInContainer(null)) {
                //if (checkStkQty(s, e)) {
                if (CalcuDiffStkQty(s, e)) {
                    //**2011/03/29 Tina：不逐筆儲存資料，改成"換頁"時才儲存資料
                    //gvMaster.UpdateEdit();

                    //1. 已經是當頁的最後一筆，就跳到下一頁的第一筆進行編輯
                    //2. 當頁還有下一筆，則下一筆資料直接進入編輯模式
                    var Index = s.name.split("cell")[1].split("_")[0] - (gvMaster.pageIndex * gvMaster.pageRowSize); //編輯Row的Index
                    var PageLastIndex = gvMaster.pageRowCount - 1;     //當頁最後一筆Index
                    if (Index == PageLastIndex) {
                        if (ASPxClientEdit.ValidateEditorsInContainer(null)) {
                            aspxGVPagerOnClick(gvMaster.name, 'PBN');
                        }
                        //else {
                        //    aspxGVPagerOnClick(gvMaster.name, 'PN' + (gvMaster.pageIndex + 1));
                        //}
                    }
                    else {
                        var CurrentIndex = s.name.split("cell")[1].split("_")[0]
                        var txtStkQty = getClientInstance('TextBox', s.name.replace("cell" + CurrentIndex, "cell" + (Number(CurrentIndex) + 1)));
                        txtStkQty.SetFocus(true);
                    }
                }
                else {
                    s.SetFocus(true);
                }
           // }
        }


        function ontxtStkchkQty_KeyDown(s, e) {

            if (e.htmlEvent.keyCode == 13) {      //按下[Enter]，執行LostFocus event
                //s.SetFocus(false);
                onRowSave(s, e)
            }
            else if (e.htmlEvent.keyCode == 27) { //按下[Esc]，執行還原原本數量
                var fName = "7_txtStkchkQty";
                var lbStkchkQty = getClientInstance('TextBox', s.name.replace(fName, "7_lbStkchkQty"));
                s.SetText(lbStkchkQty.GetText());
                s.SetFocus(false);
                //gvMaster.CancelEdit();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<OBJECT ID="detect"
CLASSID="CLSID:7BACE1A5-F435-45DB-BFB3-23B5AE9069F1"
CODEBASE="detctPrinter.CAB#version=1,0,0,1">
</OBJECT>
    <asp:HiddenField ID="PrintName" runat="server" />
    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--門市盤點作業-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreStockTaking %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='INV10.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--作業類型-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ActivityType %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="2">
                            <asp:RadioButtonList ID="rbActivityType" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="Print">列印(空白盤點單)</asp:ListItem>
                                <asp:ListItem Value="KeyIn" Selected="True">盤點輸入</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點型態-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockTakingMethod %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:RadioButtonList ID="rbStkChkType" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1">重盤</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">全盤</asp:ListItem>
                                <asp:ListItem Value="3">關帳日盤點</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點單號-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblStkchkNo" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點人員-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CountedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblStkchkUserNo" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lblModiDTM" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--盤點日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblStkchkDate" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--門市名稱-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <table border="0" cellpadding="0" cellspacing="0" align="left">
                                <tr>
                                    <td width="5px">
                                        <dx:ASPxLabel ID="lblStoreNo" runat="server">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td width="5px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="lblStoreName" runat="server">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lblModiUser" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                OnClick="btnOK_Click" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="STKCHK_D_ID"
                Width="100%" AutoGenerateColumns="False" IsClearStatus="true" 
                OnPageIndexChanged="gvMaster_PageIndexChanged" 
                onhtmlrowcreated="gvMaster_HtmlRowCreated">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" FieldName="項次" ReadOnly="True" VisibleIndex="1" Width="30px">
                        <DataItemTemplate>
                            <%#Container.ItemIndex + 1%>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <%#Container.ItemIndex + 1%>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STOCK_NAME" runat="server" Caption="<%$ Resources:WebResources, Warehouse %>"
                        ReadOnly="True" Width="40px">
                        <PropertiesTextEdit EnableDefaultAppearance="False">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODTYPENAME" runat="server" Caption="<%$ Resources:WebResources, ProductCategory %>"
                        ReadOnly="True" Width="80px">
                        <PropertiesTextEdit EnableDefaultAppearance="False">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                        ReadOnly="True" Width="50px">
                        <PropertiesTextEdit EnableDefaultAppearance="False">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"
                        ReadOnly="True">
                        <PropertiesTextEdit EnableDefaultAppearance="False">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="UNIT" runat="server" Caption="<%$ Resources:WebResources, Unit %>"
                        ReadOnly="True" Width="50px">
                        <PropertiesTextEdit EnableDefaultAppearance="False">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STKQTY" runat="server" Caption="<%$ Resources:WebResources, BookInventory %>"
                        ReadOnly="True" Width="30px">
                        <DataItemTemplate>
                            <%--<dx:ASPxLabel ID="lbStkQty" runat="server" Text='<%# Eval("STKQTY") %>'></dx:ASPxLabel>--%>
                            <dx:ASPxTextBox ID="lbStkQty" runat="server" Text='<%# Eval("STKQTY") %>' ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                        </DataItemTemplate>
                        <%--<EditItemTemplate>
                            <dx:ASPxLabel ID="lbStkQty" ClientInstanceName="clbStkQty" runat="server" Text='<%# Eval("STKQTY") %>'>
                            </dx:ASPxLabel>
                        </EditItemTemplate>--%>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STKCHKQTY" runat="server" Caption="<%$ Resources:WebResources, PhysicalInventory %>"
                        Width="50px">
                        <DataItemTemplate>
                            <%--<dx:ASPxLabel ID="lbStkchkQty" runat="server" Text='<%# Bind("STKCHKQTY") %>'></dx:ASPxLabel>--%>
                            <dx:ASPxTextBox ID="lbStkchkQty" runat="server" Text='<%# Bind("STKCHKQTY") %>' ReadOnly="true" ClientVisible="false"></dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="txtStkchkQty" runat="server"
                                Text='<%# Bind("STKCHKQTY") %>' Width="70px" MaxLength="9" HorizontalAlign="Right">
                                <ValidationSettings SetFocusOnError="true"></ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e){ checkStkQty(s, e);  }" Validation="function(s,e){ CalcuDiffStkQty(s, e); }" 
                                                  KeyDown="function(s,e){ ontxtStkchkQty_KeyDown(s,e); }" />
                            </dx:ASPxTextBox>
                        </DataItemTemplate>
                        <%--<EditItemTemplate>
                            <dx:ASPxTextBox ID="txtStkchkQty" ClientInstanceName="ctxtStkchkQty" runat="server"
                                Text='<%# Bind("STKCHKQTY") %>' Width="70px" MaxLength="9" HorizontalAlign="Right">
                                <ValidationSettings SetFocusOnError="true"></ValidationSettings>
                                <ClientSideEvents LostFocus="function(s,e){ onRowSave(s, e); }" 
                                                    KeyDown="function(s,e){  
                                                                            if(e.htmlEvent.keyCode == 13){ ctxtStkchkQty.SetFocus(false); } 
                                                                       else if(e.htmlEvent.keyCode == 27){ gvMaster.CancelEdit(); }
                                                            }" />
                            </dx:ASPxTextBox>
                        </EditItemTemplate>--%>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DIFF_STKQTY" runat="server" Caption="<%$ Resources:WebResources, DifferenceQuantity %>"
                        ReadOnly="True" Width="30px">
                        <DataItemTemplate>
                            <%--<dx:ASPxLabel ID="lblDiffStkQty" runat="server" Text='<%# Eval("DIFF_STKQTY") %>'></dx:ASPxLabel>--%>
                            <dx:ASPxTextBox ID="lblDiffStkQty" runat="server" Text='<%# Eval("DIFF_STKQTY") %>' ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                        </DataItemTemplate>
                       <%-- <EditItemTemplate>
                            <dx:ASPxLabel ID="lblDiffStkQty" ClientInstanceName="clblDiffStkQty" runat="server"
                                Text='<%# Eval("DIFF_STKQTY") %>'>
                            </dx:ASPxLabel>
                        </EditItemTemplate>--%>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager PageSize="10"> </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
<%--                <ClientSideEvents RowDblClick="function(s, e) { if (!gvMaster.IsEditing()) { gvMaster.StartEditRow(e.visibleIndex); } }" />
--%>            </cc:ASPxGridView>
            <div class="seperate">
                <dx:ASPxLabel ID="lbHidWorkDay" runat="server" Visible="False"> </dx:ASPxLabel>
            </div>
            <div id="divBTN" runat="server" class="btnPosition" visible="false">
                <table align="center" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" 
                                OnClick="btnSave_Click">
                             </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                OnClick="btnDelete_Click" CausesValidation="false">
                                <ClientSideEvents Click="function(s, e) {
                                            Import(s, e);
                                        }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <iframe id="fDownload" style="display: none" src="" runat="server" width="100%" height="100%">
    </iframe>
</asp:Content>
