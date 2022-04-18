<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintTest.aspx.cs" Inherits="PrintTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
        }
    </style>
      <script type="text/javascript">

          function Call_RecDetectPrinterName() {
              //ctl00$MainContentPlaceHolder$PrintName
              //debugger;
              var PrinterNAME = window.document.getElementById("RECPrintName").value;

              var oBarcodePrint = new ActiveXObject("detctPrinter.detect");
              var result = oBarcodePrint.detectP(PrinterNAME);
              if (result == "") {

                  alert("印表機名稱有誤!請重新確認");
                  // e.processOnServer = false;
              }
              else {
                  window.document.getElementById("RECPrintName").value = result;
                  //this.PrintName.SetText = result;

              }
              alert(window.document.getElementById("RECPrintName").value);
          }
            
          function Call_INVDetectPrinterName() {
              //ctl00$MainContentPlaceHolder$PrintName
              //debugger;
              var PrinterNAME = window.document.getElementById("INVPrintName").value;

              var oBarcodePrint = new ActiveXObject("detctPrinter.detect");
              var result = oBarcodePrint.detectP(PrinterNAME);
              if (result == "") {

                  alert("印表機名稱有誤!請重新確認");
                  // e.processOnServer = false;
              }
              else {
                  window.document.getElementById("INVPrintName").value = result;
                  //this.PrintName.SetText = result;

              }
              alert(window.document.getElementById("INVPrintName").value);
          }
          function Call_CreDetectPrinterName() {
              //ctl00$MainContentPlaceHolder$PrintName
              //debugger;
              var PrinterNAME = window.document.getElementById("CrePrintName").value;

              var oBarcodePrint = new ActiveXObject("detctPrinter.detect");
              var result = oBarcodePrint.detectP(PrinterNAME);
              if (result == "") {

                  alert("印表機名稱有誤!請重新確認");
                  // e.processOnServer = false;
              }
              else {
                  window.document.getElementById("CrePrintName").value = result;
                  //this.PrintName.SetText = result;

              }
              alert(window.document.getElementById("CrePrintName").value);
          }
          function Call_DetectPrinterName() {
              //ctl00$MainContentPlaceHolder$PrintName
              //debugger;
              var PrinterNAME = window.document.getElementById("INVPrintName").value;

              var oBarcodePrint = new ActiveXObject("detctPrinter.detect");
              var result = oBarcodePrint.detectP(PrinterNAME);
              if (result == "") {

                  alert("印表機名稱有誤!請重新確認");
                  // e.processOnServer = false;
              }
              else {
                  window.document.getElementById("INVPrintName").value = result;
                  //this.PrintName.SetText = result;

              }
              alert(window.document.getElementById("INVPrintName").value);
          }
          
          function Call_BarcodePrintFile(vorderno, vbarcodestr) {
              //debugger;
              var oBarcodePrint = new ActiveXObject("ProjBarcodePrint.BarcodePrint");
              var result = oBarcodePrint.BarcodePrintFile(vorderno, vbarcodestr);

              alert(result);
          }
      </script>
</head>
<body>
<OBJECT ID="BarcodePrint"
CLASSID="CLSID:5BF61033-4F10-4492-84F4-2052AB55CFFF"
CODEBASE="BarcodePrint.CAB#version=1,0,0,0">
</OBJECT>
<OBJECT ID="detect"
CLASSID="CLSID:7BACE1A5-F435-45DB-BFB3-23B5AE9069F1"
CODEBASE="detctPrinter.CAB#version=1,0,0,1">
</OBJECT>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="~/ClientUtility/shortcut.js" />
        </Scripts>
    </asp:ToolkitScriptManager>
    <div align="center" class="style1">
            印表機測試
    </div>
      <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
            <ContentTemplate>
              <asp:HiddenField ID="INVPrintName" runat="server" />
              <asp:HiddenField ID="RECPrintName" runat="server" />
              <asp:HiddenField ID="CrePrintName" runat="server" />
              <asp:HiddenField ID="BarcodePrintName" runat="server" />
    ID:<dx:ASPxTextBox ID="INVOICE_ID" runat="server" Width="200"></dx:ASPxTextBox>          
    <dx:ASPxButton ID="InvoiceTest" runat="server"   Text="發票測試"  onclick="InvoiceTest_Click" ClientSideEvents-Click="function(s,e){ Call_INVDetectPrinterName(); }">
       
    </dx:ASPxButton>
         
      <br />
    ID:<dx:ASPxTextBox ID="RECEIPT_ID" runat="server" Width="200"></dx:ASPxTextBox>  
    <dx:ASPxButton ID="Receipt" runat="server"   Text="收據測試" 
        ClientSideEvents-Click="function(s,e){ Call_RecDetectPrinterName(); }" 
                    onclick="Receipt_Click"></dx:ASPxButton><br />
    ID:<dx:ASPxTextBox ID="CREDIT_ID" runat="server" Width="200"></dx:ASPxTextBox>
    <dx:ASPxButton ID="Credit" runat="server"   Text="折讓單測試" onclick="Credit_Click" ClientSideEvents-Click="function(s,e){ Call_CreDetectPrinterName(); }"></dx:ASPxButton><br />
    <dx:ASPxButton ID="General" runat="server"   Text="一般測試" 
        onclick="General_Click"></dx:ASPxButton><br />
        
    ORDER_NO:<dx:ASPxTextBox ID="ORDER_NO" runat="server" Width="200"></dx:ASPxTextBox>    
    驗收單號:<dx:ASPxTextBox ID="Rec_NO" runat="server" Width="200"></dx:ASPxTextBox>
    <dx:ASPxButton ID="Barcode_Print" runat="server" Text="條碼測試" onclick="Barcode_Print_Click"></dx:ASPxButton>   
    <br />
    
    </ContentTemplate>
        </asp:UpdatePanel>
      <iframe id="fDownload" style="display:none" src="" runat="server"></iframe>
    </form>
</body>
</html>
