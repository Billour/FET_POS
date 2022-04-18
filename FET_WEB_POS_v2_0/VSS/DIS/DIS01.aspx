<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DIS01.aspx.cs" Inherits="VSS_DIS_DIS01" %>

<%@ Register src="DIS01UserControl/ItemPage.ascx" tagname="ItemPage" tagprefix="uc1" %>
<%@ Register src="DIS01UserControl/GridViewPanel.ascx" tagname="GridViewPanel" tagprefix="uc2" %>
<%@ Register src="DIS01UserControl/ucItem8.ascx" tagname="ucItem8" tagprefix="uc3" %>
<%@ Register src="DIS01UserControl/ucItem9.ascx" tagname="ucItem9" tagprefix="uc4" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
   


    <div class="titlef" align="left">
        <asp:Literal ID="Literal1" runat="server" Text="折扣設定維護作業"></asp:Literal>
    </div>
    
    <div class="criteria">
        <table>
            <tr>
                <td width="80px">折扣料號：</td>
                <td width="40px">
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
                <td align="left" width="5px">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="..." Width="2px"></dx:ASPxButton>
                </td>
                <td>&nbsp;</td>
                <td width="80px">折扣名稱：</td>
                <td align="left">
                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="btnPosition">
         <table align="Left" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"/></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"></dx:ASPxButton>
                    </td>
                     <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"></dx:ASPxButton>
                    </td>
                     <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"></dx:ASPxButton>
                    </td>
                </tr>
         </table>
    </div>
    
    <div class="seperate">
     <asp:UpdatePanel ID="upTab" runat="server">
        <ContentTemplate>
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" 
            AutoPostBack="True" onactivetabchanged="ASPxPageControl1_ActiveTabChanged" ActiveTabIndex="0">
        <TabPages>
        
              <dx:TabPage Text="Data資費">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                             <uc1:ItemPage ID="ItemPage1" runat="server"/>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
              <dx:TabPage Text="Voice資費">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                             <uc1:ItemPage ID="ItemPage2" runat="server"/>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
              <dx:TabPage Text="申裝類型">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                            <uc1:ItemPage ID="ItemPage3" runat="server"/>                           
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
              <dx:TabPage Text="指定商品">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                            <uc1:ItemPage ID="ItemPage4" runat="server"/>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
              <dx:TabPage Text="指定促銷">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                            <uc1:ItemPage ID="ItemPage5" runat="server"/>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
              <dx:TabPage Text="指定客戶等級">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                             <uc4:ucItem9 ID="ucItem9" runat="server" />
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
              <dx:TabPage Text="指定門市">
                <ContentCollection>
                    <dx:ContentControl>
                         <div>
                            <uc1:ItemPage ID="ItemPage7" runat="server"/>
                         </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
              <dx:TabPage Text="成本中心">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                            <uc1:ItemPage ID="ItemPage8" runat="server"/>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
              <dx:TabPage Text="折扣設定內容">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                            <uc3:ucItem8 ID="ucItem8" runat="server" />
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
         </TabPages>
       </dx:ASPxPageControl>
       </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    
</asp:Content>

