<%@ Page Title="View Feedback Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="fdb_general.aspx.cs" Inherits="fdb_general" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 445px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="css/grdView.css" rel="stylesheet" type="text/css">
    <asp:Panel ID="access" runat="server">
        <br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="strSem" runat="server" Text=""></asp:Label></td>
                <td>SUBJECT WISE FEEDBACK :</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="btnf1" runat="server" OnClick="btnf1_Click" Text="View feedback 1" CssClass="button-style-1" />
                    &nbsp;&nbsp;<asp:Button ID="btnf2" runat="server" OnClick="btnf2_Click" Text="View feedback 2" CssClass="button-style-1" />
&nbsp;
                    <asp:Button ID="btnfall" runat="server" OnClick="btnfall_Click" Text="View both" CssClass="button-style-1"/><br /><br />
                    <center><asp:Button ID="hide" runat="server" Text="Hide Details" OnClick="hide_Click" cssclass="button-0" /></center>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Vertical">
                        <asp:ListItem>All</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>

                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="All Feedback"  CssClass="button-style-1"/>
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Feedback 1"  CssClass="button-style-1"/>
                    &nbsp;
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Feedback 2"  CssClass="button-style-1"/>
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Forecolor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
       <center> <asp:Label ID="msg" runat="server"></asp:Label></center>
        <br />
        <br />
        <center><asp:Label ID="lblstat" runat="server" ForeColor="#3366FF" Font-Size="X-Large"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False" Visible="false">
            <Columns>

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
    <br />
    <asp:Label ID="lblex" runat="server"></asp:Label>
    <br />
    <asp:Label ID="lblmex" runat="server"></asp:Label>
            </center>
    </asp:Panel>
</asp:Content>

