<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL11_Transaction.aspx.cs" Inherits="VSS_SAL_SAL11_Transaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>交易補登選項</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript" />

    <script type="text/javascript" language="javascript">
        $(function() {
            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
            $("input:radio").attr("name", "SameRadio");
        });
    </script>

    <script type="text/javascript" language="javascript">
        $(function() {
            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
            $("input:radio").attr("name", "SameRadio");
        });
    </script>

    <script type="text/javascript">
          function OnInit(s, e) {
              s.GetInputElement().name = "radioChoose";
          }
    </script>

</head>
<body>
    <form id="form1" runat="server">    
    <div class="func">
        <div>
            <%--<div class="criteria">--%>
                <table cellpadding="0" cellspacing="0" border="0" align="center"> 
                    <tr>
                        <td align="left">
                            <dx:ASPxRadioButton ID="Radio1" ClientInstanceName="radio1" runat="server" GroupName="radioChoose" Text="由交易未結清單選取資料" Checked="true" >
                            </dx:ASPxRadioButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <dx:ASPxRadioButton ID="Radio2" ClientInstanceName="radio2" runat="server" GroupName="radioChoose" Text="手動輸入交易資料">                            
                            </dx:ASPxRadioButton>
                        </td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td align="center">
                            <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e) {
                                if (radio1.GetChecked()) {
                                    parent.redirect('SAL05.aspx?s=1');
                                }
                                hidePopupWindow();
                            }" /> </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            <%--</div>--%>

            <div class="seperate"></div>
            <div class="seperate"></div>

            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>