<%@ Page Title="Option Entry Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="option.aspx.cs" Inherits="option" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <center>
        <asp:UpdatePanel ID="access" runat="server">
            <ContentTemplate>
                <asp:Label ID="lst" runat="server" Text="Existing Options and Marks Associated" style="font-size: large" ForeColor="#3366CC"></asp:Label>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" OnPageIndexChanging="GridView1_PageIndexChanging" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="optio" HeaderText="Option" SortExpression="optio" />
                        <asp:BoundField DataField="marks" HeaderText="Marks Carrying"  />
                        <asp:BoundField DataField="low_limit" HeaderText="Lower Limit" />
                        <asp:BoundField DataField="up_limit" HeaderText="Upper Limit" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <br />
                <asp:Label ID="stat" runat="server" Text="" Visible="false"></asp:Label>
                <br /><br /><br /><br />
                <asp:Label runat="server" Text="Insertion Form of New Option" style="font-size: large" ForeColor="#006699"></asp:Label>
                <br /><br />
                <table>
                    <tr>
                        <td>New Option Name&nbsp; :&nbsp;&nbsp; </td>
                        <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Marks for Option&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; </td>
                        <td><asp:DropDownList ID="DropDownList1" runat="server" Width="110px">
                            <asp:ListItem>Select Marks</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Lower Limit&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; </td>
                        <td><asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>Select an item</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Upper Limit&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; </td>
                        <td><asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>Select an item</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="addOption" runat="server" Text="Add New Option" OnClick="addOption_Click" Width="120px" Style="cursor: pointer;"></asp:Button></td>
                    </tr>
                </table>
                <br />
                <asp:Label id="msg1" runat="server"></asp:Label>
                <br /><br /><br /><br />
                <asp:Label runat="server" Text="Deletion Form of Old Options" style="font-size: large" ForeColor="#CC6699"></asp:Label>
                <br /><br />
                <table>
                    <tr>
                        <td>Old Option Name&nbsp;:</td>
                        <td>
                            <asp:DropDownList ID="DropDownList4" runat="server">
                            <asp:ListItem>Select Option</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="remOption" runat="server" Text="Delete Old Option" OnClick="remOption_Click" Width="120px" Style="cursor: pointer;"></asp:Button></td>
                    </tr>
                </table>
                <br />
               <asp:Label id="msg2" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>

