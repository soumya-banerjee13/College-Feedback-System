<%@ Page Title="View Current Users" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="viewstudent.aspx.cs" Inherits="viewstudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <asp:Label ID="msg" runat="server" Text=""></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" OnPageIndexChanging="GridView1_PageIndexChanging" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="user_id" HeaderText="User ID" SortExpression="user_id" />
            <asp:BoundField DataField="password" HeaderText="Password" SortExpression="password" />
            <asp:BoundField DataField="stream" HeaderText="Stream" SortExpression="stream" />
            <asp:BoundField DataField="semester" HeaderText="Semester" SortExpression="semester" />
            <asp:BoundField DataField="logged_in" HeaderText="Feedback Given" SortExpression="logged_in" />
            <asp:BoundField DataField="date" HeaderText="date" SortExpression="date" />
            <%--<asp:BoundField DataField="last_log" HeaderText="last_log" SortExpression="last_log" />--%>
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
<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:studentDetails %>" SelectCommand="SELECT [user_id], [password], [stream], [semester], [logged_in], [date], [last_log] FROM [student_list]"></asp:SqlDataSource>--%>
</asp:Panel>
</asp:Content>

