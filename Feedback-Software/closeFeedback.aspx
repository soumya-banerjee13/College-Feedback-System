<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="closeFeedback.aspx.cs" Inherits="closeFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center>
            <asp:Label runat="server" Text="Open/Close Feedback" ForeColor="#3333FF" style="font-weight: 700; font-style: italic; font-size: large"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        Feedback :  
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem>Choose Feedback</asp:ListItem>
                            <asp:ListItem Value="1">1st Feedback</asp:ListItem>
                            <asp:ListItem Value="2">2nd Feedback</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="openClose" runat="server" Text="Open/Close" OnClick="openClose_Click"></asp:Button></td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Label id="msg" runat="server"></asp:Label>
        </center>
    </asp:Panel>
</asp:Content>

