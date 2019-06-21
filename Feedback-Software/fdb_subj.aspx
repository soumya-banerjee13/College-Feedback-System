<%@ Page Title="Subject wise Feedback Summary" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="fdb_subj.aspx.cs" Inherits="fdb_subj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="css/grdView.css" rel="stylesheet" type="text/css">
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
            <div id="bdy" runat="server">
                <asp:Label ID="show" runat="server" style="font-size:28px;color:#d15b5b;"></asp:Label>
                <br /><br /><br />
                <asp:Label ID="msg" runat="server"></asp:Label>
                <br />
                <asp:Label ID="mark" runat="server" Text="Mark-Sheet" Font-Size="22px" ForeColor="#318D2C" style="text-decoration: underline;" CssClass="mark"></asp:Label>
                <br /><br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
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
            </div>
            <br />
            <br />
            <br />
            <input type="button" onclick="printDiv('MainContent_bdy')" value="Print Result" style="cursor:pointer"/>
        </center>
    </asp:Panel>
</asp:Content>

