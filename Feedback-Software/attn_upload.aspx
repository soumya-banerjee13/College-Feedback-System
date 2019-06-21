<%@ Page Title="Attendance Upload Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="attn_upload.aspx.cs" Inherits="attn_upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center>
            <asp:Label runat="server" Text="Select Stream,Semester then select a .csv File to Upload Students Attendances" ForeColor="#7D2BA2" style="font-size: large"></asp:Label>
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
                    <td>Feedback :&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td>
                      <asp:DropDownList ID="DropDownList3" runat="server" Width="150px">
                        <asp:ListItem>Select Feedback</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>1st</asp:ListItem>
                        <asp:ListItem>2nd</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Select a File(.csv) :&nbsp;&nbsp; </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                    </td>
                </tr>
                <tr style="height:1px"><td style="height:1px"></td></tr>
                <tr>
                    <td>
                        View Order Before Upload&nbsp;:
                    </td>
                    <td>
                        <asp:Button ID="reqOrder" runat="server" Text="Check Order" Width="100px" OnClick="reqOrder_Click" Style="cursor: pointer;"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="attn_upl" runat="server" Text="Upload" OnClick="attn_upl_Click" Style="cursor: pointer;"></asp:Button>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Label id="msg" runat="server"></asp:Label>
            <br /><br />
            <asp:Label id="show" runat="server" style="font-size: large" ForeColor="#3333FF"></asp:Label>
            <br /><br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <columns>
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
    </asp:Panel>
</asp:Content>

