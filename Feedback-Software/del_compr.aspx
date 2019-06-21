<%@ Page Title="Compressed Data Delete Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="del_compr.aspx.cs" Inherits="del_compr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center>
            <asp:Label runat="server" Text="Deletion Form of Compressed Feedbacks" ForeColor="#6873F4" style="font-style: italic; font-weight: 700; text-shadow:5px 2px 4px #8b97c1; font-size: x-large"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td>Stream&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                    <td><asp:DropDownList ID="DropDownList1" runat="server" Width="125px">
                        <asp:ListItem>Select Stream</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Semester&nbsp; :</td>
                    <td><asp:DropDownList ID="DropDownList2" runat="server" Width="125px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        <asp:ListItem>Select Semester</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
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
                    <td>Subject&nbsp;&nbsp;&nbsp; :</td>
                    <td><asp:DropDownList ID="DropDownList3" runat="server" Width="125px">
                        <asp:ListItem>Select Subject</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Feedback :</td>
                    <td><asp:DropDownList ID="DropDownList4" runat="server" Width="125px">
                        <asp:ListItem>Select Feedback</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>1st</asp:ListItem>
                        <asp:ListItem>2nd</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Year&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :</td>
                    <td><asp:DropDownList ID="DropDownList5" runat="server" Width="125px">
                        <asp:ListItem>Select Year</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="del" runat="server" Text="Delete" Style="margin-top:7px" OnClick="del_Click"></asp:Button></td>
                </tr>
            </table>
            <asp:Label id="msg" runat="server"></asp:Label>
        </center>
    </asp:Panel>
</asp:Content>

