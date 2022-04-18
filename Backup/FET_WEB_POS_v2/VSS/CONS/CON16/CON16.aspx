<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON16.aspx.cs" Inherits="VSS_CON_CON16" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function CalcuDiffStkQty(s, e) {
           
            var fName = "6_txtStkchkQty";
            txtStkQty = getClientInstance('Label', s.name.replace(fName, "5_txtStkQty"));
            lblDiffStkQty = getClientInstance('TxtBox', s.name.replace(fName, "7_lblDiffStkQty"));
            
            var StkchkQty = s.GetValue();
            var iStkchkQty = 0;
            if (StkchkQty == null || StkchkQty == "") {
                iStkchkQty = 0; 
            }
            else {
                iStkchkQty = Number(StkchkQty);
                if (isNaN(iStkchkQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串不符合數字格式，請重新輸入';
                    return false;
                }
                else if (iStkchkQty < 0) {
                    e.isValid = false;
                    e.errorText = '門市盤點量不允許小於0，請重新輸入';
                    return false;
                }
            }

            var iStkQty = txtStkQty.GetValue();
            var Diff = Number(iStkQty) - Number(StkchkQty);
            lblDiffStkQty.SetValue(Diff);
        }

        function checkStkQty(s, e) {
          
            if (s.isValid) {
                var fName = "6_txtStkchkQty";
                txtStkQty = getClientInstance('Label', s.name.replace(fName, "5_txtStkQty"));
                lblDiffStkQty = getClientInstance('TxtBox', s.name.replace(fName, "7_lblDiffStkQty"));

                var StkchkQty = s.GetValue();
                var iStkchkQty = 0;
                if (StkchkQty == null || StkchkQty == "") {
                    iStkchkQty = 0;
                }

                var iStkQty = txtStkQty.GetValue();
                var Diff = Number(iStkQty) - Number(StkchkQty);
                lblDiffStkQty.SetValue(Diff);
                if (Diff != 0 || StkchkQty == null)
                   alert("數量不符，請確認");
            }
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
            document.body.removeChild(bgObj);
            document.body.removeChild(msgObj);
            if (str != "")
                alert(str);


        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--寄銷商品盤點作業-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductSockTaking %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" 
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='CON16_1.aspx'; }" />
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
                    <asp:RadioButtonList ID="rbActivityType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
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
                    <asp:RadioButtonList ID="rbStkChkType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">重盤</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">全盤</asp:ListItem>
                        <asp:ListItem Value="3">關帳日盤點</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="tdtxt">&nbsp;</td>
                <td class="tdval">&nbsp;</td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--盤點單號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblStkchkNo" runat="server"></dx:ASPxLabel>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--盤點人員-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CountedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblStkchkUserNo" runat="server" ></dx:ASPxLabel>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--更新日期-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxLabel ID="lblModiDTM" runat="server" ></dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--盤點日期-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblStkchkDate" runat="server" ></dx:ASPxLabel>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--門市名稱-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table border="0" cellpadding="0" cellspacing="0" align="left">
                        <tr>
                            <td width="5px"><dx:ASPxLabel ID="lblStoreNo" runat="server"></dx:ASPxLabel></td>
                            <td width="5px">&nbsp;</td>
                            <td><dx:ASPxLabel ID="lblStoreName" runat="server"></dx:ASPxLabel></td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt" nowrap="nowrap">
                    <!--更新人員-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <dx:ASPxLabel ID="lblModiUser" runat="server" ></dx:ASPxLabel>
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
   
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
        KeyFieldName="STKCHK_D_ID" Width="100%" 
        onpageindexchanged="gvMaster_PageIndexChanged" 
        onhtmlrowcreated="gvMaster_HtmlRowCreated">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                <DataItemTemplate>
                    <%#Container.ItemIndex + 1%>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>            
            <dx:GridViewDataTextColumn FieldName="SUPP_NO" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="SUPP_NAME" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="STKQTY" runat="server" Caption="<%$ Resources:WebResources, BookInventory %>">
                <DataItemTemplate>
                    <dx:ASPxTextBox ID="txtStkQty" Width="90px" runat="server" Text = '<%# Bind("STKQTY") %>' ReadOnly="true" Enabled="false" ></dx:ASPxTextBox>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="STKCHKQTY" runat="server" Caption="<%$ Resources:WebResources, PhysicalInventory %>">
                <DataItemTemplate>
                    <dx:ASPxTextBox ID="txtStkchkQty" runat="server" Text='<%# Bind("STKCHKQTY") %>' Width="100px" MaxLength="9" HorizontalAlign="Right">
                        <ValidationSettings SetFocusOnError="true">
                        </ValidationSettings>
                        <ClientSideEvents Validation="function(s,e){ CalcuDiffStkQty(s, e); }"  TextChanged="function(s,e){ checkStkQty(s,e); }"/>
                    </dx:ASPxTextBox>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DIFF_STKQTY" runat="server" Caption="<%$ Resources:WebResources, DifferenceQuantity %>">
                <DataItemTemplate>
                    <dx:ASPxTextBox ID="lblDiffStkQty" runat="server" Text = '<%# Eval("DIFF_STKQTY") %>'
                        ReadOnly="true" Border-BorderStyle="None" Width="100%" HorizontalAlign="Right"></dx:ASPxTextBox>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
    </cc:ASPxGridView>
    
    <div class="seperate"></div>
    
    <div id="divBTN" runat="server" class="btnPosition" visible="false">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" 
                        OnClick="btnSave_Click" >
                        <ClientSideEvents Click="function(s,e){sSave('存檔中...');}" />
                     </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                        OnClick="btnCancel_Click" CausesValidation="false"/>
                </td>
                 <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                        OnClick="btnDelete_Click" CausesValidation="false" >
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
    <iframe id="fDownload" style="display:none" src="" runat="server" width="100%" height="100%"></iframe>
</asp:Content>
