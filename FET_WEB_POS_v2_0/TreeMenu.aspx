<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreeMenu.aspx.cs" Inherits="TreeMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="ClientUtility/shortcut.js"></script>
    <script type="text/javascript">
        shortcut.add('Ctrl+Shift+X', function() {
            alert('Hi there!');
        });
    </script>
    <script type="text/javascript" language="javascript">
        function redirect(url) {
            parent.frames["Working"].location = url;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="myTreeMenu" runat="server" ExpandDepth="0">
            <Nodes>
                <asp:TreeNode Text="促銷商品價格查詢 " Value="促銷商品價格查詢" NavigateUrl="~/VSS/SAL/SAL07.aspx"
                    Target="Working" />
                <asp:TreeNode Text="銷售作業" Value="銷售作業" NavigateUrl="~/VSS/SAL/SAL01.aspx" Target="Working" />
                <asp:TreeNode Text="銷售交易查詢" Value="銷售交易查詢" NavigateUrl="~/VSS/SAL/SAL02.aspx" Target="Working" />
                <%--CHK--%>
                <asp:TreeNode Text="CHK" Value="CHK-" SelectAction="Expand">
                    <asp:TreeNode Text="門市日結作業" Value="門市日結作業" NavigateUrl="~/VSS/CHK/CHK01.aspx" Target="Working" />
                    <asp:TreeNode Text="機台讀帳作業" Value="機台讀帳作業" NavigateUrl="~/VSS/CHK/CHK02.aspx" Target="Working" />
                    <asp:TreeNode Text="保全收款作業" Value="保全收款作業" NavigateUrl="~/VSS/CHK/CHK03.aspx" Target="Working" />
                    <asp:TreeNode Text="找零金" Value="找零金" NavigateUrl="~/VSS/CHK/CHK04.aspx" Target="Working" />
                    <asp:TreeNode Text="繳大鈔" Value="繳大鈔" NavigateUrl="~/VSS/CHK/CHK05.aspx" Target="Working" />
                </asp:TreeNode>               
                <%--CONS--%>
                 <asp:TreeNode Text="CON" Value="CONS" SelectAction="Expand">                    
                    <asp:TreeNode Text="外部廠商維護作業(總部)" Value="外部廠商維護作業(總部)" NavigateUrl="~/VSS/CONS/CON02.aspx" Target="Working" />
                    <asp:TreeNode Text="外部廠商查詢作業(總部)" Value="外部廠商查詢作業(總部)" NavigateUrl="~/VSS/CONS/CON01.aspx" Target="Working" />
                    <asp:TreeNode Text="外部廠商查詢作業(門市)" Value="外部廠商查詢作業(門市)" NavigateUrl="~/VSS/CONS/CON01a.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品維護作業(總部)" Value="寄銷商品維護作業(總部)" NavigateUrl="~/VSS/CONS/CON04.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品查詢作業(總部)" Value="寄銷商品查詢作業(總部)" NavigateUrl="~/VSS/CONS/CON03.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品查詢作業(門市)" Value="寄銷商品查詢作業(門市)" NavigateUrl="~/VSS/CONS/CON03a.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品訂單查詢" Value="寄銷商品訂單查詢" NavigateUrl="~/VSS/CONS/CON05.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品訂貨作業" Value="寄銷商品訂貨作業" NavigateUrl="~/VSS/CONS/CON06.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品主配作業" Value=" 寄銷商品主配作業" NavigateUrl="~/VSS/CONS/CON08.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品退倉設定查詢(總部)" Value="寄銷商品退倉設定查詢(總部)" NavigateUrl="~/VSS/CONS/CON09.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品退倉設定作業(總部)" Value="寄銷商品退倉設定作業(總部)" NavigateUrl="~/VSS/CONS/CON10.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品退倉設定查詢(門市)" Value="寄銷商品退倉設定查詢(門市)" NavigateUrl="~/VSS/CONS/CON11.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品退倉設定作業(門市)" Value="寄銷商品退倉設定作業(門市)" NavigateUrl="~/VSS/CONS/CON12.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品進貨驗收查詢" Value="寄銷商品進貨驗收查詢" NavigateUrl="~/VSS/CONS/CON13.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品進貨驗收作業" Value="寄銷商品進貨驗收作業" NavigateUrl="~/VSS/CONS/CON14.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品盤點查詢" Value="寄銷商品盤點查詢" NavigateUrl="~/VSS/CONS/CON15.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品盤點作業" Value="寄銷商品盤點作業" NavigateUrl="~/VSS/CONS/CON16.aspx" Target="Working" />
                    <asp:TreeNode Text="外部廠商月結作業" Value="外部廠商月結作業" NavigateUrl="~/VSS/CONS/CON17.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品移出作業" Value="寄銷商品移出作業" NavigateUrl="~/VSS/CONS/CON20.aspx" Target="Working" />
                    <asp:TreeNode Text="寄銷商品撥入作業" Value="寄銷商品撥入作業" NavigateUrl="~/VSS/CONS/CON21.aspx" Target="Working" />
                </asp:TreeNode> 
                <%--DIS--%>
                <asp:TreeNode Text="DIS" Value="DIS" SelectAction="Expand">
                    <asp:TreeNode Text="折扣設定查詢" Value="折扣設定查詢" NavigateUrl="~/VSS/DIS/DIS02.aspx" Target="Working" />
                    <asp:TreeNode Text="群組關聯性設定查詢" Value="群組關聯性設定查詢" NavigateUrl="~/VSS/DIS/DIS04.aspx"
                        Target="Working" />
                    <asp:TreeNode Text="組合促銷設定查詢" Value="組合促銷設定查詢" NavigateUrl="~/VSS/DIS/DIS05.aspx"
                        Target="Working" />
                    <asp:TreeNode Text="補貼金額查詢" Value="補貼金額查詢" NavigateUrl="~/VSS/DIS/DIS07.aspx" Target="Working" />
                    <asp:TreeNode Text="組合促銷轉換值查詢 - 商品編號" Value="組合促銷轉換值查詢 - 商品編號" NavigateUrl="~/VSS/DIS/DIS08.aspx"
                        Target="Working" />
                    <asp:TreeNode Text="組合促銷轉換值查詢 - 商品類別/屬性" Value="組合促銷轉換值查詢 - 商品類別/屬性" NavigateUrl="~/VSS/DIS/DIS09.aspx"
                        Target="Working" />
                    <asp:TreeNode Text=" ERP與類別屬性對應查詢" Value=" ERP與類別屬性對應查詢" NavigateUrl="~/VSS/DIS/DIS10.aspx"
                        Target="Working" />
                    <asp:TreeNode Text="屬性主檔查詢" Value="屬性主檔查詢" NavigateUrl="~/VSS/DIS//DIS11.aspx" Target="Working" />
                    <asp:TreeNode Text="類別主檔查詢" Value="類別主檔查詢" NavigateUrl="~/VSS/DIS/DIS12.aspx" Target="Working" />
                </asp:TreeNode>
                <%-- INV--%>
                <asp:TreeNode Text="INV-" Value="INV-" SelectAction="Expand">
                    <asp:TreeNode Text="總部移撥查詢作業" Value="總部移撥查詢作業" NavigateUrl="~/VSS/INV/INV01.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="移撥作業" Value="移撥作業" NavigateUrl="~/VSS/INV/INV02.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="庫存查詢作業" Value="庫存查詢作業" NavigateUrl="~/VSS/INV/INV03.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="退倉設定查詢作業" Value="退倉設定查詢作業" NavigateUrl="~/VSS/INV/INV04.aspx"
                        Target="Working"></asp:TreeNode>
                    <asp:TreeNode Text="退倉設定作業" Value="退倉設定作業" NavigateUrl="~/VSS/INV/INV05.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="退倉作業" Value="退倉作業" NavigateUrl="~/VSS/INV/INV06.aspx" Target="Working">
                    </asp:TreeNode>
                    <%-- <asp:TreeNode Text="退倉作業" Value="退倉作業" NavigateUrl="~/VSS/INV/INV07.aspx" Target="Working">
                    </asp:TreeNode>--%>
                    <asp:TreeNode Text="進貨驗收作業" Value="進貨驗收作業" NavigateUrl="~/VSS/INV/INV08.aspx"
                        Target="Working"></asp:TreeNode>
                    <%-- <asp:TreeNode Text="進貨驗收作業" Value="進貨驗收作業" NavigateUrl="~/VSS/INV/INV09.aspx" Target="Working">
                    </asp:TreeNode>--%>
                    <asp:TreeNode Text="盤點查詢作業" Value="盤點查詢作業" NavigateUrl="~/VSS/INV/INV10.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="門市盤點作業" Value="門市盤點作業" NavigateUrl="~/VSS/INV/INV11.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="關帳日設定" Value="關帳日設定" NavigateUrl="~/VSS/INV/INV17.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="庫存調整查詢作業" Value="庫存調整查詢作業" NavigateUrl="~/VSS/INV/INV18.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="庫存調整作業" Value="庫存調整作業" NavigateUrl="~/VSS/INV/INV18_1.aspx" Target="Working">
                    </asp:TreeNode>                    
                    <asp:TreeNode Text="倉別設定作業" Value="倉別設定作業" NavigateUrl="~/VSS/INV/INV23.aspx" Target="Working" />                    
                    <asp:TreeNode Text="移出查詢作業" Value="移出查詢作業" NavigateUrl="~/VSS/INV/INV24.aspx" Target="Working">
                    </asp:TreeNode>  
                    <asp:TreeNode Text="移出作業" Value="移出作業" NavigateUrl="~/VSS/INV/INV25.aspx" Target="Working">
                    </asp:TreeNode>  
                    <asp:TreeNode Text="撥入作業" Value="撥入作業" NavigateUrl="~/VSS/INV/INV26.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="總部拆封商品設定" Value="總部拆封商品設定" NavigateUrl="~/VSS/INV/INV27.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="門市拆封IMEI設定" Value="門市拆封IMEI設定" NavigateUrl="~/VSS/INV/INV28.aspx" Target="Working">
                    </asp:TreeNode>
                    
                </asp:TreeNode>
                <%--LEA --%>
                <asp:TreeNode Text="LEA" Value="LEA-" SelectAction="Expand">
                    <asp:TreeNode Text="設備租賃設定作業" Value="設備租賃設定作業" NavigateUrl="~/VSS/LEA/LEA01.aspx" Target="Working" />
                    <asp:TreeNode Text="可租賃設備查詢" Value="可租賃設備查詢" NavigateUrl="~/VSS/LEA/LEA02.aspx"
                        Target="Working" />
                    <asp:TreeNode Text="已租賃設備查詢" Value="已租賃設備查詢" NavigateUrl="~/VSS/LEA/LEA03.aspx" Target="Working" />
                    <asp:TreeNode Text="可租賃設備預約/新增/領取/歸還" Value="可租賃設備預約/新增/領取/歸還" NavigateUrl="~/VSS/LEA/LEA04.aspx"
                        Target="Working" />
                    <asp:TreeNode Text="設備租賃作業(含租賃簽收單)" Value="設備租賃作業(含租賃簽收單)" NavigateUrl="~/VSS/LEA/LEA05.aspx" Target="Working" />
                    <asp:TreeNode Text="租賃收費明細表查詢" Value="租賃收費明細表查詢" NavigateUrl="~/VSS/LEA/LEA06.aspx" Target="Working" />
                    <asp:TreeNode Text="設備租賃設定查詢作業" Value="設備租賃設定查詢作業" NavigateUrl="~/VSS/LEA/LEA07.aspx" Target="Working" />    
                </asp:TreeNode>
                <%--LOG --%>
                <asp:TreeNode Text="LOG" Value="LOG" SelectAction="Expand">
                    <asp:TreeNode Text="系統參數設定" Value="系統參數設定" NavigateUrl="~/VSS/LOG/LOG04.aspx" Target="Working" />
                    <asp:TreeNode Text="功能清單設定" Value="功能清單設定" NavigateUrl="~/VSS/LOG/LOG05.aspx" Target="Working" />
                    <asp:TreeNode Text="角色功能對應作業" Value="角色功能對應作業" NavigateUrl="~/VSS/LOG/LOG03.aspx" Target="Working" />
                    <asp:TreeNode Text="使用者功能對應作業" Value="使用者功能對應作業" NavigateUrl="~/VSS/LOG/LOG03b.aspx" Target="Working" />  
                    <asp:TreeNode Text="修改密碼" Value="修改密碼" NavigateUrl="~/VSS/LOG/LOG07.aspx" Target="Working" />   
                    <%--<asp:TreeNode Text="使用者登入" Value="使用者登入" NavigateUrl="~/VSS/LOG/LOG01.aspx" Target="Working" />--%>
                    <%--<asp:TreeNode Text="角色查詢" Value="角色查詢" NavigateUrl="~/VSS/LOG/LOG02.aspx" Target="Working" />--%>                    
                    <%--<asp:TreeNode Text="角色使用者對應作業" Value="角色使用者對應作業" NavigateUrl="~/VSS/LOG/LOG03a.aspx" Target="Working" />--%>                                                                                                   
                </asp:TreeNode>
                <%--ORD--%>
                <asp:TreeNode Text="ORD-訂貨" Value="ORD-訂貨" SelectAction="Expand">
                    <asp:TreeNode Text="訂貨作業" Value="訂貨作業" NavigateUrl="~/VSS/ORD/ORD01.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="預訂貨作業" Value="預訂貨作業" NavigateUrl="~/VSS/ORD/ORD12.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="訂單查詢作業" Value="訂單查詢作業" NavigateUrl="~/VSS/ORD/ORD02.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="訂單查詢報表" Value="訂單查詢報表" NavigateUrl="~/VSS/ORD/ORD03.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="調整訂單作業" Value="調整訂單作業" NavigateUrl="~/VSS/ORD/ORD04.aspx" Target="Working">
                    </asp:TreeNode>
                    <%--<asp:TreeNode Text="一搭一查詢作業" Value="一搭一查詢作業" NavigateUrl="~/VSS/ORD/ORD05.aspx" Target="Working">
                    </asp:TreeNode>--%>
                    <asp:TreeNode Text="一搭一設定作業" Value="一搭一設定作業" NavigateUrl="~/VSS/ORD/ORD06.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Non-DropShipment主配查詢作業" Value="Non-DropShipment主配查詢作業" NavigateUrl="~/VSS/ORD/ORD07.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="Non-DropShipment主配作業" Value="Non-DropShipment主配作業" NavigateUrl="~/VSS/ORD/ORD08.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="DropShipment主配上傳" Value="DropShipment主配上傳" NavigateUrl="~/VSS/ORD/ORD09.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="權重佔比分配" Value="權重佔比分配" NavigateUrl="~/VSS/ORD/ORD10.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="商品建議訂購量設定" Value="商品建議訂購量設定" NavigateUrl="~/VSS/ORD/ORD11.aspx" Target="Working" />
                </asp:TreeNode>                
                <%--OPT--%>
                <asp:TreeNode Text="OPT" Value="OPT-" SelectAction="Expand">
                    <asp:TreeNode Text="支付方式設定作業" Value="支付方式設定作業" NavigateUrl="~/VSS/OPT/OPT01.aspx"
                        Target="Working"></asp:TreeNode>
                    <asp:TreeNode Text="信用卡手續費設定作業" Value="信用卡手續費設定作業" NavigateUrl="~/VSS/OPT/OPT02.aspx"
                        Target="Working"></asp:TreeNode>
                    <asp:TreeNode Text="信用卡分期設定作業" Value="信用卡分期設定作業" NavigateUrl="~/VSS/OPT/OPT03.aspx"
                        Target="Working"></asp:TreeNode>
                    <asp:TreeNode Text="禮券設定作業" Value="禮券設定作業" NavigateUrl="~/VSS/OPT/OPT04.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="總部發票設定作業" Value="總部發票設定作業" NavigateUrl="~/VSS/OPT/OPT05.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="發票設定作業" Value="發票設定作業" NavigateUrl="~/VSS/OPT/OPT05a.aspx" Target="Working">
                    </asp:TreeNode>
                    <asp:TreeNode Text="門市基本資料設定作業" Value="門市基本資料設定作業" NavigateUrl="~/VSS/OPT/OPT06.aspx"
                        Target="Working"></asp:TreeNode>
                    <asp:TreeNode Text="HG兌點維護作業" Value="HG兌點維護作業" NavigateUrl="~/VSS/OPT/OPT07.aspx"
                        Target="Working"></asp:TreeNode>
                    <%--<asp:TreeNode Text="員工基本資料維護作業" Value="員工基本資料維護作業" NavigateUrl="~/VSS/OPT/OPT08.aspx"
                        Target="Working"></asp:TreeNode>--%>
                    <asp:TreeNode Text="員工基本資料查詢" Value="員工基本資料查詢" NavigateUrl="~/VSS/OPT/OPT09.aspx" Target="Working" />
                    <asp:TreeNode Text="商品主檔設定" Value="商品主檔設定" NavigateUrl="~/VSS/OPT/OPT10.aspx" Target="Working" />
                    <asp:TreeNode Text="HG點數兌換設定" Value="HG點數兌換設定" NavigateUrl="~/VSS/OPT/OPT11.aspx" Target="Working" />
                    <asp:TreeNode Text="HG點數累點設定" Value="HG點數累點設定" NavigateUrl="~/VSS/OPT/OPT12.aspx" Target="Working" />
                    <asp:TreeNode Text="HG活動兌點限制" Value="HG活動兌點限制－單商品" NavigateUrl="~/VSS/OPT/OPT13.aspx" Target="Working" />
                     <asp:TreeNode Text="HG活動兌點限制" Value="HG活動兌點限制－促銷活動" NavigateUrl="~/VSS/OPT/OPT13_1.aspx" Target="Working" />
                    <%--<asp:TreeNode Text="HG點數兌換－來店禮" Value="HG點數兌換－來店禮" NavigateUrl="~/VSS/OPT/OPT14.aspx" Target="Working" />--%> 
                    <asp:TreeNode Text="HG點數兌換-來店禮" Value="HG點數兌換-來店禮" NavigateUrl="~/VSS/OPT/OPT15.aspx" Target="Working" />  
                    <asp:TreeNode Text="HG兌點名單上傳" Value="HG兌點名單上傳" NavigateUrl="~/VSS/OPT/OPT16.aspx" Target="Working" />
                    <asp:TreeNode Text="門市手開發票號碼設定" Value="門市手開發票號碼設定" NavigateUrl="~/VSS/OPT/OPT17.aspx" Target="Working" />
                    <asp:TreeNode Text="門市店長折扣設定" Value="門市店長折扣設定" NavigateUrl="~/VSS/OPT/OPT18.aspx" Target="Working" />
                </asp:TreeNode>
                <%--PRE--%>
                <asp:TreeNode Text="PRE" Value="PRE-" SelectAction="Expand">
                    <asp:TreeNode Text="預購作業" Value="預購作業" NavigateUrl="~/VSS/PRE/PRE01.ASPX" Target="Working" />
                    <asp:TreeNode Text="預購查詢" Value="預購查詢" NavigateUrl="~/VSS/PRE/PRE02.ASPX" Target="Working" />
                    <asp:TreeNode Text="預購活動設定作業" Value="預購活動設定作業" NavigateUrl="~/VSS/PRE/PRE03.ASPX"
                        Target="Working" />
                    <asp:TreeNode Text="預購活動查詢" Value="預購活動查詢" NavigateUrl="~/VSS/PRE/PRE04.ASPX" Target="Working" />
                </asp:TreeNode>
                <%--SAL--%>
                <asp:TreeNode Text="SAL" Value="SAL-" SelectAction="Expand">
                    <%--<asp:TreeNode Text="銷售交易查詢" Value="銷售交易查詢" NavigateUrl="~/VSS/SAL/SAL02.aspx" Target="Working" />--%>
                    <asp:TreeNode Text="換貨作業" Value="換貨作業" NavigateUrl="~/VSS/SAL/SAL031.aspx" Target="Working" />
                    <asp:TreeNode Text="銷售作廢作業" Value="銷售作廢作業" NavigateUrl="~/VSS/SAL/SAL041.aspx" Target="Working" />
                    <asp:TreeNode Text=" 交易未結清單" Value=" 交易未結清單" NavigateUrl="~/VSS/SAL/SAL05.aspx" Target="Working" />
                    <asp:TreeNode Text="交易暫存清單" Value="交易暫存清單" NavigateUrl="~/VSS/SAL/SAL06.aspx" Target="Working" />
                </asp:TreeNode>
                <%--<asp:TreeNode Text="查詢供應商資訊作業" Value="查詢供應商資訊作業" NavigateUrl="~/Login.aspx" Target="Working">
                </asp:TreeNode>
                <asp:TreeNode Text="供貨商資料維護作業" Value="供貨商資料維護作業" NavigateUrl="~/Login.aspx" Target="Working">
                </asp:TreeNode>--%>
            </Nodes>
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
