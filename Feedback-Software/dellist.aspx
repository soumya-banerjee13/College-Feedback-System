<%@ Page Title="Student List Deletion Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="dellist.aspx.cs" Inherits="dellist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <p style="color:red;">Click the button below to delete the current student list :</p><br /> <asp:Button ID="btndel" runat="server" text="Delete student list" OnClick="btndel_Click" Style="cursor: pointer;"/>
    <br />
    <asp:Label ID="lblstate" runat="server"></asp:Label>
    <br />
</asp:Content>

