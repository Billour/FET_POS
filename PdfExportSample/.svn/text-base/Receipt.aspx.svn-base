<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receipt.aspx.cs" Inherits="Receipt" EnableEventValidation = "false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>電子發票</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <h2>電子發票</h2>
        <a href="Default.aspx">首頁</a><br/>
        <asp:Button ID="uxExport" runat="server" Text="Export to PDF" OnClick="uxExport_Click" />        
        <asp:ListView ID="uxMainListView" runat="server" DataSourceID="OrderHeaderDataSource">
            <LayoutTemplate>                                                               
                <table id="itemPlaceholder" runat="server"></table>   
            </LayoutTemplate>
            <ItemTemplate>                
                <table style="width:800px">
                    <caption style="color:#FF4300; font-size:larger">ＸＸ股份有限公司</caption>        
                    <tr>
                        <td>
                            <div align="center"><font color="#FF4300">電 子 計 算 機 統 一 發 票</font></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div align="center">
                                <font color="#FF4300">中華民國</font>&nbsp;&nbsp;99&nbsp;&nbsp;<font color="#FF4300">年</font>&nbsp;&nbsp;08&nbsp;&nbsp;<font 
                                    color="#FF4300">月</font>&nbsp;&nbsp;05&nbsp;&nbsp;<font color="#FF4300">日</font></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width:100%">
                                <tr>
                                    <td width="12%"><font color="#FF4300">發 票 號 碼：</font></td>
                                    <td width="12%">NW33508783</td>
                                    <td width="33%"></td>
                                    <td width="12%"></td>
                                    <td width="30%"></td>
                                </tr>
                                <tr>
                                    <td><font color="#FF4300">買&nbsp;&nbsp;&nbsp;受&nbsp;&nbsp;&nbsp;人：</font></td>
                                    <td colspan="4"><%# Eval("Name")%></td>        
                                </tr>
                                   <tr>
                                    <td><font color="#FF4300">統 一 編 號：</font></td>        
                                    <td colspan="4"></td>        
                                </tr>
                                <tr>
                                    <td><font color="#FF4300">地&nbsp;&nbsp;&nbsp;址：</font></td>
                                    <td colspan="2"><%# Eval("Address")%></td>
                                    <td><font color="#FF4300">訂單編號：</font></td>
                                    <td><%# Eval("OrderNumber")%><asp:HiddenField ID="uxOrderID" runat="server" Value='<%# Eval("OrderID") %>' /></td>
                                </tr>
                            </table>        
                        </td>        
                    </tr>     
                    <tr>
                        <td>    
                            <asp:ListView ID="uxNestedListView" runat="server" DataSourceID="OrderDetailDataSource" 
                            OnItemDataBound="uxNestedListView_ItemDataBound" OnDataBound="uxNestedListView_DataBound">
                                <LayoutTemplate>
                                    <table style="width:100%; border-collapse:collapse;" cellspacing="0" border="1">                                                                       
                                        <tr align="center">
                                            <td style="width:40%">品名</td>
                                            <td style="width:9%">數量</td>
                                            <td style="width:12%">單價</td>
                                            <td style="width:12%">金額</td>
                                            <td style="width:27%">備註</td>
                                        </tr>                                                                                                             
                                        <tr id="itemPlaceholder" runat="server"></tr>   
                                        <tr>
                                            <td colspan="3" align="center"><font color="#FF4300">銷　　售　　額　　合　　計</font></td>
                                            <td align="right"><asp:Label runat="server" ID="uxAmountTotal" Text="0"></asp:Label></td>
                                        </tr>       
                                        <tr>
                                            <td colspan="3" rowspan="2" style="padding:0">
                                                <table style="width:100%; border-collapse:collapse" cellspacing="0" border="1">
                                                    <tr><td rowspan="2" align="center">營　業　稅</td><td align="center">應　　稅</td><td align="center">零　稅　率</td><td align="center">免　　稅</td></tr>
                                                    <tr><td align="center">V</td><td></td><td></td></tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>  
                                        <tr>
                                            <td align="right">0</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center"><font color="#FF4300">總　　　　　　　　　　計</font></td>
                                            <td align="right">0</td>
                                        </tr>  
                                        <tr>
                                            <td colspan="4">
                                                <table border="0" style="width:100%">
                                                    <tr>
                                                        <td>總計新台幣</td>
                                                        <td></td>
                                                        <td>拾萬</td>
                                                        <td></td>
                                                        <td>萬</td>
                                                        <td></td>
                                                        <td>仟</td>
                                                        <td></td>
                                                        <td>佰</td>
                                                        <td></td>
                                                        <td>拾</td>
                                                        <td></td>
                                                        <td>元整</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>                             
                                    </table>                                                                
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr id="uxTr" runat="server">
                                        <td><%# Eval("Description") %></td>
                                        <td align="center"><%# Eval("Quantity") %></td>
                                        <td align="right"><%# Eval("Price") %></td>
                                        <td align="right"><%# Eval("Subtotal") %></td>
                                        <td></td>
                                    </tr>
                                </ItemTemplate>                                                                                           
                            </asp:ListView>
                            <asp:AccessDataSource ID="OrderDetailDataSource" runat="server"
                            DataFile="~/App_Data/Sales.mdb"   
                            SelectCommand="SELECT * FROM vwOrderDetail WHERE OrderID=@OrderID">
                                <SelectParameters>
                                    <asp:ControlParameter Name="OrderID" ControlID="uxOrderID" PropertyName="Value" />
                                </SelectParameters>
                            </asp:AccessDataSource>                                                                     
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <font color="#005700">※<span class="Apple-converted-space">&nbsp;</span></font><font 
                                            color="#FE5624">應稅、零稅、免稅之銷售額應分別開立統一發票，並應於各該欄位打 「 V 」。</font></td>
                                </tr>
                                <tr>
                                    <td>
                                        <font color="#005700">※<span class="Apple-converted-space">&nbsp;</span></font><font 
                                            color="#FE5624">本發票依財政部台北市國稅局中正稽徵所 93年 10 月 29 日財北國稅中正營業字第0930025449號函核准使用。</font></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>                
            </ItemTemplate>
            <ItemSeparatorTemplate>
                <hr style="color: #fff; background-color: #fff; border: 1px dotted #ff0000; border-style: none none dotted;"  />
            </ItemSeparatorTemplate>
        </asp:ListView>
        <asp:AccessDataSource ID="OrderHeaderDataSource" runat="server"
        DataFile="~/App_Data/Sales.mdb"   
        SelectCommand="SELECT * FROM vwOrderHeader"></asp:AccessDataSource>
    </div>
    </form>
</body>
</html>
