<%@ Page Title="Subject Upload Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="subj_upload.aspx.cs" Inherits="subj_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center><asp:Label runat="server" Text="Select Stream,Semester then select a .csv File to Upload Subjects" ForeColor="#7D2BA2" style="font-size: large"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td>Stream&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </td><td><asp:DropDownList ID="DropDownList1" runat="server" Width="150px">
                    <asp:ListItem>Select Stream</asp:ListItem>
                    </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Semester :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; </td><td>
                      <asp:DropDownList ID="DropDownList2" runat="server" Width="150px">
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
                    <td>Select a File(.csv) :&nbsp;&nbsp; </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" ></asp:FileUpload>
                    </td>
                </tr>
                <tr style="height:1px"><td style="height:1px"></td></tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="subj_upl" runat="server" Text="Upload" OnClick="subj_upl_Click" Style="cursor: pointer;"></asp:Button>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Label id="msg" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label id="change" runat="server" Text="New subjects are uploaded, to apply these changes to the system click on the link below..." Visible="False" ForeColor="#242111" style="font-style: italic; font-weight: 700"></asp:Label>
            <br />
            <asp:HyperLink runat="server" id="go_change" NavigateUrl="db_generate.aspx" ForeColor="#3333FF" visible="false"  Font-Underline="True"  Style="cursor: pointer;">Go to Apply Subject Changes Page</asp:HyperLink>
        </center>
    </asp:Panel>
</asp:Content>

