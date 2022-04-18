<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutSM.aspx.cs" Inherits="VSS_CheckOut_CheckOutSM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <base target ="_self"/>
    <title>特殊抱怨折扣</title>

      <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
      <script src="../../ClientUtility/Common.js" type="text/javascript"></script>
    
      <script type= "text/javascript">

          function ReturnValue(s, e) {
              var Reson = '';
              var DISCOUNT_REASON = SH_DISCOUNT_REASON.GetText();
              
              if (DISCOUNT_REASON == "其它" && SH_DISCOUNT_DESC.GetText() == '') {
                  alert("輸入折抵原因!");
              }
              else if (SH_DISCOUNT_RATE.GetText() == '' && TOTAL_AMOUNT.GetText() == '') {
                  alert("金額和比率請擇一輸入值!");
              }
              else {
                  //**2011/05/03 Tina：折扣原因選擇其它，則折扣原因描述帶回使用者輸入的描述，否則帶回選取的原因。
                  if (DISCOUNT_REASON == "其它") {
                      Reson = SH_DISCOUNT_DESC.GetText();
                  }
                  else {
                      Reson = SH_DISCOUNT_REASON.GetText();
                  }

                  var TRAN_DATE = $('#hidTRAN_DATE').val();  //$get("TRAN_DATE").value; //QueryString("TRAN_DATE");
                  //**2010/05/03 Tina：折扣原因來源改從 DISCOUNT_MASTER 取得，因此折扣料號不須至SYS_PARAM取得，即折扣原因代號 = 折扣料號。
                  returnValue = 'STOREDISCOUNT,' + SH_DISCOUNT_REASON.GetValue() + ',' + Reson + ',' + Number(DIS_AMOUNT.GetText()).toFixed(0) + ',' + SH_DISCOUNT_RATE.GetText() + ',' + SH_DISCOUNT_REASON.GetValue() + ',' + Reson + ',' + window.document.getElementById("hidRoleType").value;
                  window.close();

                  
                  //PageMethods.getDISCOUNTInfo(TRAN_DATE, getISCOUNTInfo_OnOK);
              }
          }

//          function getISCOUNTInfo_OnOK(returnData) {
//              if (returnData == '') {
//                  alert("取得特殊抱怨折扣的折扣料號、名稱失敗!");
//              }
//              else {
//                  returnValue = 'STOREDISCOUNT,' + returnData + ',' + Number(DIS_AMOUNT.GetText()).toFixed(0) + ',' + SH_DISCOUNT_RATE.GetText() + ',' + SH_DISCOUNT_REASON.GetValue() + ',' + SH_DISCOUNT_DESC.GetText() + ',' + window.document.getElementById("hidRoleType").value;
//                  window.close();
//              }
//          }

          function DisabledDISType(s, e) {
              //btnOK.SetEnabled(false);    //只要按了"輸入"button,【密碼輸入】欄位輸入值檢核正確,即btnOK.ClientEnabled = true;
              PRODTOTALAMOUNT = $('#hidTOTAL_AMOUNT').val();  //$get("TOTAL_AMOUNT").value; //QueryString("TOTAL_AMOUNT");
              if (s.name == TOTAL_AMOUNT.name) {
                  if (s.GetText() != '') {
                      SH_DISCOUNT_RATE.SetEnabled(false);
                      var IsNumber = Number(s.GetText());
                      if (isNaN(IsNumber) || Number(s.GetText()) <= 0) {
                          s.SetText(null);
                          EbanledDISTypeTextBox();
                          alert('輸入字串非數字格式且不允許小於等於0，請重新輸入!');
                      } else {
                          if (!isInteger(s.GetText())) {
                              s.SetText(null);
                              EbanledDISTypeTextBox();
                              alert('折抵金額不為整數，請重新輸入!');
                          } else {
                              DIS_AMOUNT.SetText(s.GetText());
                              if (Number(s.GetText()) > Number(RemainingDiscountAmount.GetText())) {
                                  s.SetText(null);
                                  EbanledDISTypeTextBox();
                                  if (window.document.getElementById("hidRoleType").value == "2") {
                                      if (confirm("已超過剩餘可折抵金額，是否使用店長權限?")) {
                                          btnEnter.SetVisible(true);
                                          txtPassword.SetEnabled(true);
                                          btnEnter.SetEnabled(true);
                                          RemainingDiscountAmount.SetVisible(false);
                                          RoleDisAmt.SetVisible(false);
                                          RoleDisRate.SetVisible(false);
                                          TOTAL_AMOUNT.SetText("");
                                          SH_DISCOUNT_RATE.SetText("");
                                          TOTAL_AMOUNT.SetEnabled(false);
                                          SH_DISCOUNT_RATE.SetEnabled(false);
                                          SH_DISCOUNT_REASON.SetEnabled(false);
                                      } 
                                  } else {
                                      alert('已超過剩餘可折抵金額，請重新輸入!');
                                  }
                              } else if (Number(s.GetText()) > Number(PRODTOTALAMOUNT)) {
                                  s.SetText(null);
                                  EbanledDISTypeTextBox();
                                  alert('折抵金額不可超過應收總金額，請重新輸入!');
                              } else if (Number(s.GetText()) > Number(window.document.getElementById("hidRoleDisAmt").value)) {
                                  s.SetText(null);
                                  EbanledDISTypeTextBox();
                                  if (window.document.getElementById("hidRoleType").value == "2") {
                                      if (confirm("已超過目前角色單次可折抵金額，是否使用店長權限?")) {
                                          btnEnter.SetVisible(true);
                                          txtPassword.SetEnabled(true);
                                          btnEnter.SetEnabled(true);
                                          RemainingDiscountAmount.SetVisible(false);
                                          RoleDisAmt.SetVisible(false);
                                          RoleDisRate.SetVisible(false);
                                          TOTAL_AMOUNT.SetText("");
                                          SH_DISCOUNT_RATE.SetText("");
                                          TOTAL_AMOUNT.SetEnabled(false);
                                          SH_DISCOUNT_RATE.SetEnabled(false);
                                          SH_DISCOUNT_REASON.SetEnabled(false);
                                      } 
                                  } else {
                                      alert('已超過目前角色單次可折抵金額，請重新輸入!');
                                  }
                              } else {
                                  btnOK.SetEnabled(true);
                              }
                          }
                      }
                  } else {
                      EbanledDISTypeTextBox();
                  }
              } else {  //輸入折扣比率
                  if (s.GetText() != '') {
                      TOTAL_AMOUNT.SetEnabled(false);
                      var IsNumber = Number(s.GetText());
                      if (isNaN(IsNumber) || Number(s.GetText()) <= 0) {
                          s.SetText(null);
                          EbanledDISTypeTextBox();
                          alert('輸入字串非數字格式且不允許小於等於0，請重新輸入!');
                      } else {
                          if (Number(s.GetText()) > Number(window.document.getElementById("hidRoleDisRateBound").value)) {
                              s.SetText(null);
                              EbanledDISTypeTextBox();
                              if (window.document.getElementById("hidRoleType").value == "2") {
                                  if (confirm("已超過目前角色單次可折抵比率，是否使用店長權限?")) {
                                      btnEnter.SetVisible(true);
                                      txtPassword.SetEnabled(true);
                                      btnEnter.SetEnabled(true);
                                      RemainingDiscountAmount.SetVisible(false);
                                      RoleDisAmt.SetVisible(false);
                                      RoleDisRate.SetVisible(false);
                                      TOTAL_AMOUNT.SetText("");
                                      SH_DISCOUNT_RATE.SetText("");
                                      TOTAL_AMOUNT.SetEnabled(false);
                                      SH_DISCOUNT_RATE.SetEnabled(false);
                                      SH_DISCOUNT_REASON.SetEnabled(false);
                                  } 
                              } else {
                                  alert('已超過目前角色單次可折抵比率，請重新輸入!');
                              }
                          } else {
                              var CalDIS_AMOUNT = Number(PRODTOTALAMOUNT) * Number(s.GetText()) / 100;
                              DIS_AMOUNT.SetText(CalDIS_AMOUNT);
                              if (Number(CalDIS_AMOUNT) > Number(RemainingDiscountAmount.GetText())) {
                                  s.SetText(null);
                                  EbanledDISTypeTextBox();
                                  if (window.document.getElementById("hidRoleType").value == "2") {
                                      if (confirm("已超過剩餘可折抵金額，是否使用店長權限?")) {
                                          btnEnter.SetVisible(true);
                                          txtPassword.SetEnabled(true);
                                          btnEnter.SetEnabled(true);
                                          RemainingDiscountAmount.SetVisible(false);
                                          RoleDisAmt.SetVisible(false);
                                          RoleDisRate.SetVisible(false);
                                          TOTAL_AMOUNT.SetText("");
                                          SH_DISCOUNT_RATE.SetText("");
                                          TOTAL_AMOUNT.SetEnabled(false);
                                          SH_DISCOUNT_RATE.SetEnabled(false);
                                          SH_DISCOUNT_REASON.SetEnabled(false);
                                      } 
                                  } else {
                                      alert('已超過剩餘可折抵金額，請重新輸入!');
                                  }
                              } else if (Number(CalDIS_AMOUNT) > Number(PRODTOTALAMOUNT)) {
                                  s.SetText(null);
                                  EbanledDISTypeTextBox();
                                  alert('折抵金額不可超過應收總金額，請重新輸入!');
                              } else if (Number(CalDIS_AMOUNT) > Number(window.document.getElementById("hidRoleDisAmt").value)) {
                                  s.SetText(null);
                                  EbanledDISTypeTextBox();
                                  if (window.document.getElementById("hidRoleType").value == "2") {
                                      if (confirm("已超過目前角色單次可折抵金額，是否使用店長權限?")) {
                                          btnEnter.SetVisible(true);
                                          txtPassword.SetEnabled(true);
                                          btnEnter.SetEnabled(true);
                                          RemainingDiscountAmount.SetVisible(false);
                                          RoleDisAmt.SetVisible(false);
                                          RoleDisRate.SetVisible(false);
                                          TOTAL_AMOUNT.SetText("");
                                          SH_DISCOUNT_RATE.SetText("");
                                          TOTAL_AMOUNT.SetEnabled(false);
                                          SH_DISCOUNT_RATE.SetEnabled(false);
                                          SH_DISCOUNT_REASON.SetEnabled(false);
                                      } 
                                  } else {
                                      alert('已超過目前角色單次可折抵金額，請重新輸入!');
                                  }
                              } else {
                                  btnOK.SetEnabled(true);
                              }
                          }
                      }
                  } else {
                      EbanledDISTypeTextBox();
                  }
              }
          }

          function EbanledDISTypeTextBox() {
              SH_DISCOUNT_RATE.SetEnabled(true);
              TOTAL_AMOUNT.SetEnabled(true);
          }
          
          function EnabledDISReason(s, e) {
              if (s.GetText() == "其它" || s.GetText() == "其他") {
                  SH_DISCOUNT_DESC.SetEnabled(true);
              }
              else {
                  SH_DISCOUNT_DESC.SetText('');
                  SH_DISCOUNT_DESC.SetEnabled(false);
              }
          }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"> </asp:ScriptManager>
    <asp:HiddenField ID="hidStoreDisAmtBound" runat="server" />
    <asp:HiddenField ID="hidRoleDisAmtBound" runat="server" />
    <asp:HiddenField ID="hidRoleDisRateBound" runat="server" />
    <asp:HiddenField ID="hidStoreUsedAmt" runat="server" />
    <asp:HiddenField ID="hidRoleDisAmt" runat="server" />
    <asp:HiddenField ID="hidRoleUsedAmt" runat="server" />   
    <asp:HiddenField ID="hidRoleType" runat="server" />  
    <asp:HiddenField ID="hidTOTAL_AMOUNT" runat="server" />  
    <asp:HiddenField ID="hidTRAN_DATE" runat="server" />  
    <div style="text-align: center">
        <dx:ASPxTextBox ID="hdDIS_AMOUNT" ClientInstanceName="DIS_AMOUNT" runat="server" Text="0" ClientVisible="false"></dx:ASPxTextBox>  
        <table>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <tr>
                    <td class="tdtxt">
                        密碼輸入：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtPassword" ClientInstanceName="txtPassword" runat="server" Width="100px" Password="True" 
                                            ValidationSettings-ValidationGroup="VPWD" ClientEnabled="false">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnEnter" ClientInstanceName="btnEnter" runat="server" Text="<%$ Resources:WebResources, Enter %>" OnClick="btnEnter_Click" 
                                                    AutoPostBack="true" ValidationGroup="VPWD" ClientVisible="false" ClientEnabled="false">
                                     </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        剩餘可折抵金額：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblRemainingDiscountAmount" ClientInstanceName="RemainingDiscountAmount" runat="server" Text="0"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        單次最高可折抵金額：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblRoleDisAmt" ClientInstanceName="RoleDisAmt" runat="server" Text="0"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        單次最高可折比率：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblRoleDisRate" ClientInstanceName="RoleDisRate" runat="server" Text="0"></dx:ASPxLabel>%
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        折抵金額：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtTOTAL_AMOUNT" ClientInstanceName="TOTAL_AMOUNT" runat="server" Width="100px" MaxLength="8" HorizontalAlign="Right" ClientEnabled="false">
                             <ValidationSettings>
                                  <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                             </ValidationSettings>
                            <ClientSideEvents TextChanged="function(s,e){ DisabledDISType(s, e); }" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        折抵比率：
                    </td>
                    <td class="tdval">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtSH_DISCOUNT_RATE" ClientInstanceName="SH_DISCOUNT_RATE" runat="server" 
                                        ClientEnabled="false" Width="100px" MaxLength="3" HorizontalAlign="Right">
                                         <ValidationSettings>
                                              <RegularExpression ValidationExpression="^[0-9]+\.{0,1}[0-9]{0,2}$" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                                         </ValidationSettings>
                                        <ClientSideEvents TextChanged="function(s,e){ DisabledDISType(s, e); }" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    %
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        折抵原因：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlSH_DISCOUNT_REASON" ClientInstanceName="SH_DISCOUNT_REASON" runat="server" ClientEnabled="false">
                            <ClientSideEvents ValueChanged="function(s, e) { EnabledDISReason(s, e); }" />
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txSH_DISCOUNT_DESC" ClientInstanceName="SH_DISCOUNT_DESC" runat="server" ClientEnabled="false" Width="100px" MaxLength="100">
                        </dx:ASPxTextBox>
                    </td>
                 </tr>
                </ContentTemplate>
            </asp:UpdatePanel>
        </table>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnOK" runat="server" ClientInstanceName="btnOK" ClientEnabled="false"
                            Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){ ReturnValue(s, e); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s,e){returnValue = ''; window.close();}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
