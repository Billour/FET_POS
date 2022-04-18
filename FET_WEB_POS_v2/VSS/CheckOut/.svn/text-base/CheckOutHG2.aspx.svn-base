<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutHG2.aspx.cs" Inherits="VSS_CheckOut_CheckOutHG2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target ="_self"/>

    <title>HappyGo資料輸入</title>

     <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">

        _UsePoint = 0;    //已兌換的點數
        _UseAmount = 0;   //已兌換的金額
       
        //刷卡
        function Call_HappyGo_Card_Sale() {
            var oECR = new ActiveXObject("ProjECR.ECRAPI");
            var retStr = oECR.Inquiry_Loyalty_Point("");
            return retStr;
            
            //舊的程式
            //var oECR = new ActiveXObject("ProjECR.ECRAPI");
            //var STORENO = $get("hiddenSTORENO").value;
            //return oECR.Online_Redeem_Transaction(txtAMOUNT.GetText(), STORENO, "0", "");
            //return '0000,1111-2222-3333-4444,700';
        }

        function CheckQuantity(s, e) {

            fName = "2_txtQuantity";
            OldQuantity = getClientInstance('TxtBox', s.name.replace(fName, "2_OldQuantity"));  //TextChanged前的數量
            lblPoint = getClientInstance('Label', s.name.replace(fName, "1_lblPoint"));       //兌換點數
            lblAmount = getClientInstance('Label', s.name.replace(fName, "1_lblAmount"));     //兌換金額

            var IsNumber = Number(s.GetText());
            if (isNaN(IsNumber) || Number(s.GetText()) < 0) {
                s.SetText(OldQuantity.GetText());
                alert('輸入字串非數字格式且不允許小於0，請重新輸入!');
            }
            else {
                if (!isInteger(s.GetText())) {
                    s.SetText(OldQuantity.GetText());
                    alert('輸入字串不為整數，請重新輸入!');
                }
                else {
                    var fName = "";
                    var lblPoint;
                    var lblAmount;
                    var OldQuantity;

                    if (_UsePoint == 0) { _UsePoint = Number(hdSumPoint.GetText()); }
                    if (_UseAmount == 0) { _UseAmount = Number(hdSumAmount.GetText()); }

                    //輸入數量後的總點數和總金額，不能大於剩餘點數或折抵金額上限
                    var calPoint = _UsePoint;
                    var calAmount = _UseAmount;
                    calPoint -= Number(lblPoint.GetText()) * Number(OldQuantity.GetText());   //減掉 原本的兌換點數
                    calAmount -= Number(lblAmount.GetText()) * Number(OldQuantity.GetText()); //減掉 原本的兌換金額

                    calPoint += Number(lblPoint.GetText()) * Number(s.GetText());   //加上 現在的兌換點數
                    calAmount += Number(lblAmount.GetText()) * Number(s.GetText()); //加上 現在的兌換金額

                    var PRODTOTALAMOUNT = $get("TOTAL_AMOUNT").value;  //QueryString("TOTAL_AMOUNT");  //應收總金額
                    if (calPoint <= Number(OriginalHG_LEFT_POINT.GetText()) && calAmount <= Number(PRODTOTALAMOUNT)) {
                        _UsePoint = calPoint;
                        _UseAmount = calAmount;
                        OldQuantity.SetText(s.GetText())
                        hdSumPoint.SetText(_UsePoint);
                        hdSumAmount.SetText(_UseAmount);
                        HG_REDEEM_POINT.SetText(_UseAmount + "元(" + _UsePoint + "點)");
                        HG_LEFT_POINT.SetText(OriginalHG_LEFT_POINT.GetText() - _UsePoint);
                    } 
                    else if (calPoint > Number(OriginalHG_LEFT_POINT.GetText())) {
                        s.SetText(OldQuantity.GetText());
                        alert("兌換點數不能超過剩餘點數!");
                    } else {
                        s.SetText(OldQuantity.GetText());
                        alert("兌點金額不能超過應收總金額!");
                    }
                }
            }

        }
        
        //刷卡扣點
        function Call_HappyGo_Card_Full_Redeem() {
            var oECR = new ActiveXObject("ProjECR.ECRAPI");
            var storeNo = hidStoreNo.GetText();
            var HGCardNo = lblFinHG_CAR_NO.GetText();
            var HGRedeemPoint = hdFinSumPoint.GetText();
            var retStr = oECR.Full_Redeem(storeNo,HGCardNo,HGRedeemPoint);
            var retArr = retStr.split(',');
            if (retArr[0] == "0000") 
                return true;
            else 
                return false;
            //return '0000,1111-2222-3333-4444,7000';
        }
        function ReturnValue(s, e) {
            if (_UsePoint == 0) { _UsePoint = Number(hdFinSumPoint.GetText()); }
            if (_UseAmount == 0) { _UseAmount = Number(hdFinSumAmount.GetText()); }

            if (Number(hdFinSumAmount.GetText()) <= 0) {
                alert("請輸入兌換數量!");
            }
            else {
                if (Call_HappyGo_Card_Full_Redeem()) {
                    returnValue = 'HGPAID,' + _UseAmount + ',' + lblFinHG_CAR_NO.GetText() + ',' + _UsePoint + ',' + lblFinHG_LEFT_POINT.GetText() + ",";
                } else {
                    alert('HG 扣點失敗!');
                    returnValue = '';
                }                
                window.close();
            }
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="seperate">
    </div>
    <div class="checkOutDiv">
        <div>
            <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="false" Width="100%" 
                OnNextButtonClick="Wizard1_NextButtonClick" OnPreviousButtonClick="Wizard1_PreviousButtonClick">
                <StartNavigationTemplate>
                    <dx:ASPxButton ID="btnStart" runat="server" Text="<%$ Resources:WebResources, Ok %>" CommandName="MoveNext" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){ HGCard(s,e);}" />
                    </dx:ASPxButton>
                    <dx:ASPxTextBox ID="hdHG_CAR_NO" ClientInstanceName="hdHG_CAR_NO" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hdHG_LEFT_POINT" ClientInstanceName="hdHG_LEFT_POINT" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <script type="text/javascript">
                        function HGCard(s, e) {
//跳過刷卡程式步驟，自訂卡號、剩餘點數
//                              hdHG_CAR_NO.SetText("test-1234567890");       //HG卡號
//                              hdHG_LEFT_POINT.SetText("9999");   //剩餘點數
//                              e.processOnServer = true;  
//以下為刷卡程式，commit必須使用以下程式               
                            var r = Call_HappyGo_Card_Sale();
                            //TEST等刷卡程式用好來就可以WORK了
                            var ra = r.split(',');
                            if (ra[0] == '0000') //刷卡成功
                            {
                                hdHG_CAR_NO.SetText(ra[1]);       //HG卡號
                                hdHG_LEFT_POINT.SetText(ra[2]);   //剩餘點數
                                e.processOnServer = true;
                            }
                            else //失敗
                            {
                                alert("授權失敗!!");
                                e.processOnServer = false;
                            }
                        }
                    </script>
                </StartNavigationTemplate>
                <StepNavigationTemplate>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnNext" runat="server" Text="下一步" CommandName="MoveNext">
                                </dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){ window.close(); }" />
                                </dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </StepNavigationTemplate>
                <NavigationStyle HorizontalAlign="Center" />
                <WizardSteps>
                    <asp:WizardStep ID="Step1" runat="server" StepType="Start">
                        <table align="center">
                            <tr>
                                <td style="width: 200px; height: 200px; background-color: Silver" align="center" valign="middle">
                                    請過卡
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep ID="Step2" runat="server" StepType="Step">
                        <table>
                            <tr>
                                <td class="tdtxt">
                                    HG卡號：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxLabel ID="lblHG_CAR_NO" ClientInstanceName="HG_CAR_NO" runat="server"></dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">
                                    剩餘點數：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxLabel ID="lblOriginalHG_LEFT_POINT" ClientInstanceName="OriginalHG_LEFT_POINT" runat="server" ClientVisible="false"></dx:ASPxLabel>
                                    <dx:ASPxLabel ID="lblHG_LEFT_POINT" ClientInstanceName="HG_LEFT_POINT" runat="server"></dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">
                                    欲兌點數：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxTextBox ID="hdSumPoint" ClientInstanceName="hdSumPoint" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hdSumAmount" ClientInstanceName="hdSumAmount" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="lblHG_REDEEM_POINT" ClientInstanceName="HG_REDEEM_POINT" runat="server" ForeColor="Red" Border-BorderStyle="None" ReadOnly="true"></dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="seperate"></div>
                        <div>
                            <dx:ASPxGridView ID="gvMaster" runat="server" Width="100%" KeyFieldName="項次" >
                                 <Columns>
                                    <dx:GridViewDataColumn FieldName="項次" Caption="項次">
                                        <DataItemTemplate>
                                            <%#Container.ItemIndex + 1%>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="項目名稱" Caption="項目名稱" CellStyle-HorizontalAlign="Left">
                                        <DataItemTemplate>
                                            <dx:ASPxLabel ID="lblPoint" runat="server" Text='<%# Bind("[兌換點數]") %>' ClientVisible="false"></dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblAmount" runat="server" Text='<%# Bind("[兌換金額]") %>' ClientVisible="false" ></dx:ASPxLabel>
                                            <dx:ASPxLabel ID="lblItemName" runat="server" Text='<%# Bind("[項目名稱]") %>' ></dx:ASPxLabel>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="數量">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="OldQuantity" runat="server" Text='<%# Eval("[數量]") %>' ClientVisible="false"></dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="txtQuantity" runat="server" Text='<%# Bind("[數量]") %>' Width="50px" MaxLength="8">
                                                <ValidationSettings>
                                                    <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                                                </ValidationSettings>
                                                <ClientSideEvents TextChanged="function(s,e) { CheckQuantity(s,e); } " />
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <asp:Label ID="Label7" runat="server" Text="一般兌點通則"></asp:Label>
                                    </TitlePanel>
                                </Templates>
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                                <Settings ShowTitlePanel="true" />
                                <SettingsBehavior AllowSort="false" />
                            </dx:ASPxGridView>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="Step3" runat="server" StepType="Finish">
                        <table>
                            <tr>
                                <td class="tdtxt">
                                    HG卡號：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxLabel ID="lblFinHG_CAR_NO" ClientInstanceName="lblFinHG_CAR_NO" runat="server"></dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">
                                    剩餘點數：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxLabel ID="lblFinHG_LEFT_POINT"  ClientInstanceName="lblFinHG_LEFT_POINT" runat="server"></dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">
                                    欲兌金額/點數：
                                </td>
                                <td class="tdval">
                                    <dx:ASPxTextBox ID="hdFinSumPoint" ClientInstanceName="hdFinSumPoint" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hdFinSumAmount" ClientInstanceName="hdFinSumAmount" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxLabel ID="hidStoreNo" ClientInstanceName="hidStoreNo" runat="server" ClientVisible="false"></dx:ASPxLabel>
                                    <dx:ASPxLabel ID="lblFinHG_REDEEM_POINT" ClientInstanceName="lblFinHG_REDEEM_POINT" runat="server"></dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 160px; background-color: Silver" align="center" valign="middle">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="請過卡兌點" Font-Size="Larger"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                </WizardSteps>
                <FinishNavigationTemplate>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnPre" runat="server" Text="上一步" CommandName="MovePrevious" ></dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxButton ID="btnFinOK" runat="server" Text="<%$ Resources:WebResources, OK %>" CommandName="MoveComplete" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){ ReturnValue(s, e); }" />
                                </dx:ASPxButton>
                            </td>
                            
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxButton ID="btnFinClose" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false" >
                                    <ClientSideEvents Click="function(s,e){window.close();}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </FinishNavigationTemplate>                
            </asp:Wizard>
        </div>
    </div>
    </form>
</body>
</html>
