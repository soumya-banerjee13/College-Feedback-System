<%@ Page Title="Gateway to Feedback Details" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="fdb_gateway.aspx.cs" Inherits="fdb_gateway" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center>
            <asp:Label id="wlcm" runat="server" Text="To View Feedback Details and Manipulate them select Stream and Semester from the List below:" style="font-size: large"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td>Stream&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList1" runat="server" Width="120px">
                        <asp:ListItem>Select Stream</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Semester&nbsp;:&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList2" runat="server" Width="120px">
                        <asp:ListItem>Select Semester</asp:ListItem>
                        <asp:ListItem>1st</asp:ListItem>
                        <asp:ListItem>2nd</asp:ListItem>
                        <asp:ListItem>3rd</asp:ListItem>
                        <asp:ListItem>4th</asp:ListItem>
                        <asp:ListItem>5th</asp:ListItem>
                        <asp:ListItem>6th</asp:ListItem>
                        <asp:ListItem>7th</asp:ListItem>
                        <asp:ListItem>8th</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="go" runat="server" Text="Go" Width="60 px" OnClick="go_Click" Style="cursor: pointer;"></asp:Button></td>
                </tr>
            </table>
            <br /><br />
            <asp:Label id="msg" runat="server"></asp:Label>
        </center>
    </asp:Panel>
</asp:Content>

