<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="deleteUser_manually.aspx.cs" Inherits="deleteUser_manually" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center>
            <div style="font-size:large">
                <asp:Label runat="server" Text="Delete User by Roll Number" style="font-size: large" ForeColor="#3366CC" Font-Bold="True"></asp:Label>
                <br />
                <br />
                <br />
                User ID :  <asp:TextBox id="queryBox" runat="server"></asp:TextBox>
                <br />
                <asp:Button id="deleteUser" runat="server" style="margin-top:5px;" Text="Delete User" OnClick="deleteUser_Click"></asp:Button>
                <br />
                <br />
                <asp:Label ID="msg" runat="server" ForeColor="#996633"></asp:Label>
                <br />
                <br />
                <br />
                <br />
            </div>
        </center>
    </asp:Panel>
</asp:Content>

