<%@ Page Title="Apply Subject Changes Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="db_generate.aspx.cs" Inherits="db_generate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="access" runat="server">
        <ContentTemplate>
        <center>
            <asp:Label runat="server" Text="Apply Subject Change to Overall System" style="font-size: large"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td>Stream&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="120px">
                            <asp:ListItem>Select Stream</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Semester :&nbsp;&nbsp; </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="120px">
                            <asp:ListItem>Select Semester</asp:ListItem>
                        <asp:ListItem>1st</asp:ListItem>
                        <asp:ListItem>2nd</asp:ListItem>
                        <asp:ListItem>3rd</asp:ListItem>
                        <asp:ListItem>4th</asp:ListItem>
                        <asp:ListItem>5th</asp:ListItem>
                        <asp:ListItem>6th</asp:ListItem>
                        <asp:ListItem>7th</asp:ListItem>
                        <asp:ListItem>8th</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="view" runat="server" Text="View Subjects" Width="90px" OnClick="view_Click" Style="cursor: pointer;"></asp:Button></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="chng" runat="server" Text="Apply Changes" OnClick="chng_Click" Style="cursor: pointer;"></asp:Button></td>
                </tr>
            </table>
            <br />
            <asp:Label id="msg" runat="server"></asp:Label>
            <br /><br />
            <asp:Label id="show" runat="server" style="font-size: x-large" ForeColor="#3333FF"></asp:Label>
            <br /><br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <columns>
                    <asp:BoundField DataField="subj_name" HeaderText="Subject Name"/>
                    <asp:BoundField DataField="subj_code" HeaderText="Subject Code"/>
                </columns>
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
        </center>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

