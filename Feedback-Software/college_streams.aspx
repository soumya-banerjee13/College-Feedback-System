<%@ Page Title="Stream Entry Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="college_streams.aspx.cs" Inherits="college_streams" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <center>
        <asp:UpdatePanel ID="access" runat="server"><ContentTemplate>
            <asp:Label ID="lst" runat="server" Text="List of Streams Available in GCETTB" style="font-size: large" ForeColor="#3366CC"></asp:Label><br /><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" OnPageIndexChanging="GridView1_PageIndexChanging" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="stream_name" HeaderText="Stream Name" SortExpression="stream_name" />
            <asp:BoundField DataField="short_name" HeaderText="Short Name" SortExpression="short_name" />
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
        <asp:Label ID="stat" runat="server" Text="" Visible="false"></asp:Label>
    <br /><br /><br /><br />
     <asp:Label runat="server" Text="Insertion Form of New Stream in GCETTB" style="font-size: large" ForeColor="#006699"></asp:Label><br /><br /><table>
            <tr>
                <td>New Stream Name&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </td>
                <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            </tr>
           <tr>
               <td>Short Name of New Stream&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
               <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
           </tr>
           <tr>
               <td></td>
               <td><asp:Button ID="createStream" runat="server" Text="Insert New Stream" OnClick="createStream_Click" Style="cursor: pointer;"></asp:Button></td>
           </tr>
                </table><br />
           <asp:Label id="msg1" runat="server" Text=""></asp:Label>
       <br /><br /><br /><br />
               <asp:Label runat="server" Text="Deletion Form of Old Stream" style="font-size: large" ForeColor="#CC6699"></asp:Label><br /><br />
               <table>
                   <tr>
                       <td>Short Name of Stream to Delete:&nbsp;&nbsp; </td>
                       <td><asp:DropDownList ID="DropDownList1" runat="server">
                           <asp:ListItem>Select Stream</asp:ListItem>
                           </asp:DropDownList></td>
                   </tr>
                   <tr>
                       <td></td>
                       <td><asp:Button ID="delStream" runat="server" Text="Delete Old Stream" OnClick="delStream_Click" Style="cursor: pointer;"></asp:Button></td>
                   </tr>
               </table><br />
               <asp:Label id="msg2" runat="server" Text=""></asp:Label></ContentTemplate>
               </asp:UpdatePanel>
           </center>
</asp:Content>

