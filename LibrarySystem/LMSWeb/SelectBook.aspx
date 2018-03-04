<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SelectBook.aspx.vb" Inherits="LMSWeb.SelectBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style1
        {
            width: 164px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;"> <%--align="center"--%>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <img alt="" src="images/title.png" style="text-align: center" /></td>
                <td>
                    <asp:Label ID="WelcomeMessage" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label>
                    <br />
                    <asp:LinkButton ID="SignOut" runat="server" Font-Size="Small">Sign out</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Menu ID="Menu1" runat="server" BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#990000" StaticSubMenuIndent="10px">
                        <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <DynamicMenuStyle BackColor="#FFFBD6" />
                        <DynamicSelectedStyle BackColor="#FFCC66" />
                        <Items>
                            <asp:MenuItem Text="Workspace" Value="Workspace" NavigateUrl="~/StudentWorkspace.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Change Password" Value="Change Password" NavigateUrl="~/ChangePassword.aspx"></asp:MenuItem>
                            <asp:MenuItem Selected="True" Text="Search Books" Value="Search Books"></asp:MenuItem>
                        </Items>
                        <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticSelectedStyle BackColor="#FFCC66" />
                    </asp:Menu>
                </td>
                <td>
                    <asp:Label ID="ErrorMSG" runat="server" BorderColor="#FF3300" ForeColor="Red" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="DSID" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="BookTitle" HeaderText="Title" SortExpression="BookTitle" />
                            <asp:BoundField DataField="BookAuthor" HeaderText="Author" SortExpression="BookAuthor" />
                            <asp:BoundField DataField="PublicationYear" HeaderText="Year" SortExpression="PublicationYear" />
                            <asp:BoundField DataField="Press" HeaderText="Press" SortExpression="Press" />
                            <asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject" />
                            <asp:BoundField DataField="ShelfNo" HeaderText="Shelf" SortExpression="ShelfNo" />
                            <asp:BoundField DataField="Barcode" HeaderText="Barcode" SortExpression="Barcode" />
                            <asp:ButtonField ButtonType="Button" CommandName="request" Text="Request" />
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="DSID" runat="server" ConnectionString="<%$ ConnectionStrings:LMSConnectionString %>" SelectCommand="SELECT [BookID], [BookTitle], [BookAuthor], [PublicationYear], [Press], [Subject], [ShelfNo], [Barcode] FROM [Books]"></asp:SqlDataSource>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
