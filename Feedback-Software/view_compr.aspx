<%@ Page Title="Compressed Records View" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="view_compr.aspx.cs" Inherits="view_compr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;
            document.body.innerHTML = printContents;
            window.print();
            document.body.innerHTML = originalContents;
        }
	 </script>
    <asp:Panel ID="access" runat="server">
        <center>
            <asp:Label runat="server" Text="Compressed Feedbacks View" ForeColor="#6873F4" style="font-style: italic; font-weight: 700; text-shadow:5px 2px 4px #8b97c1; font-size: x-large"></asp:Label>
            <br />
            <br />
            <table>
                <tr>
                    <td>Stream&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList1" runat="server" Width="125px">
                        <asp:ListItem>Select Stream</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Semester&nbsp; :&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList2" runat="server" Width="125px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
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
                    <td>Subject&nbsp;&nbsp;&nbsp; :&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList3" runat="server" Width="125px">
                        <asp:ListItem>Select Subject</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Feedback :&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList4" runat="server" Width="125px">
                        <asp:ListItem>Select Feedback</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>1st</asp:ListItem>
                        <asp:ListItem>2nd</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Year&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList5" runat="server" Width="125px">
                        <asp:ListItem>Select Year</asp:ListItem>
                        <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Order by&nbsp; :&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList6" runat="server" Width="125px">
                        <asp:ListItem>None</asp:ListItem>
                        <asp:ListItem Value="subj_code">Subject</asp:ListItem>
                        <asp:ListItem Value="status">Feedback</asp:ListItem>
                        <asp:ListItem Value="year">Year</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Records Per Page&nbsp; :&nbsp;</td>
                    <td><asp:DropDownList ID="DropDownList7" runat="server" Width="125px">

                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="view" runat="server" Text="View" Style="margin-top:7px" OnClick="view_Click" Width="50px"></asp:Button></td>
                </tr>
            </table>
            <asp:Label id="msg" runat="server"></asp:Label>
            <br />
            <br />
            <div id="Print">
            <asp:Label id="show" runat="server" ForeColor="#3366FF" Font-Size="X-Large"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False" Visible="false">
            <Columns>
                <asp:BoundField DataField="subj_name" HeaderText="Subject Name" SortExpression="subj_name" />
                <asp:BoundField DataField="opt_count" HeaderText="Options Marked" SortExpression="opt_count" />
                <asp:BoundField DataField="total" HeaderText="Total Feedbacks" SortExpression="total" />
                <asp:BoundField DataField="marks" HeaderText="Total Marks" SortExpression="marks" />
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
                </div>
            <br />
            <br />
            <input type="button" onclick="printDiv('Print')" value="Print Records" style="cursor:pointer"/>
        </center>
    </asp:Panel>
</asp:Content>

