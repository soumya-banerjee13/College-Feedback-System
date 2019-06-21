<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="fdb_del.aspx.cs" Inherits="fdb_del" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="css/grdView.css" rel="stylesheet" type="text/css">
    <asp:Panel ID="access" runat="server">
        <center>
            <asp:Label runat="server" Text="Deletion Form of Detailed Feedbacks" CssClass="header-text"></asp:Label>
            <br />
            <br />
            <table>
                <tr style="color:#6c19eb;">
                    <td>
                        Stream&nbsp;&nbsp;&nbsp;&nbsp; :
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="125px">
                            <asp:ListItem>Select Stream</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="color:#6c19eb;">
                    <td>
                        Semester&nbsp; :
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="125px">
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
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="color:#6c19eb;">
                    <td>
                        Feedback :
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" Width="125px">
                            <asp:ListItem>Select Feedback</asp:ListItem>
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>1st</asp:ListItem>
                            <asp:ListItem>2nd</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="comp_del" runat="server" Text="Compress" style="margin-top:10px;cursor: pointer;" Width="90px" CssClass="button-style-2" OnClick="comp_del_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="just_del" runat="server" Text="Delete" style="margin-top:7px" width="90px" CssClass="button-style-2" OnClick="just_del_Click"></asp:Button>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Label id="msg" runat="server"></asp:Label>
        </center>
    </asp:Panel>
</asp:Content>

