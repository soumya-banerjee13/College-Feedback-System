<%@ Page Title="Student List Upload Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="user_upload.aspx.cs" Inherits="user_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center>
            <asp:Label runat="server" text="Student List Upload Form" style="font-size: x-large; font-weight: 700; font-style: italic;" ForeColor="#3366CC"></asp:Label>
            <br /><br /><br />
            <table>
                <tr>
                    <td>Select a File(.csv) to Upload&nbsp;&nbsp;:&nbsp;&nbsp;</td>
                    <td><asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload></td>
                </tr>
                <tr>
                    <td style="height:1px"></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="stu_upload" runat="server" Text="Upload" OnClick="stu_upload_Click" Style="cursor: pointer;"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Label id="msg" runat="server"></asp:Label>
        </center>
    </asp:Panel>
</asp:Content>

