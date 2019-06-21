<%@ Page Title="Start Dates View" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="viewDate.aspx.cs" Inherits="viewDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center>
            <asp:Label runat="server" Text="You Are Viewing Start Dates of Feedback-I & Feedback-II" ForeColor="#3333FF" style="font-weight: 700; font-style: italic; font-size: large"></asp:Label>
            <br />
            <br />
            <table border="1">
                <tr><th>Start Date of Feedback1</th><th>Start Date of Feedback2</th></tr>
                <tr><td>
                    <asp:Label ID="Label1" runat="server"></asp:Label></td><td>
                    <asp:Label ID="Label2" runat="server"></asp:Label></td></tr>
            </table>
            <br />
            <br />
            <asp:Label id="msg" runat="server"></asp:Label>
        </center>
    </asp:Panel>
</asp:Content>

