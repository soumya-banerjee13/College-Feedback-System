<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="userName_query.aspx.cs" Inherits="userName_query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="access" runat="server">
        <center>
            <div style="font-size:large">
                <asp:Label runat="server" Text="User Query by Roll Number" style="font-size: large" ForeColor="#3366CC" Font-Bold="True"></asp:Label>
                <br />
                <br />
                <br />
                Roll Number :  <asp:TextBox id="queryBox" runat="server"></asp:TextBox>
                <br />
                &nbsp;
                <asp:Button id="query" runat="server" style="margin-top:5px;" Text="Check" OnClick="query_Click"></asp:Button>
                <br />
                <br />
                <asp:Label ID="msg" runat="server" ForeColor="#996633"></asp:Label>
                <br />
                <br />
                <br />
                <br />
            </div>
            <pre>
                Note: This page only should be used when a student would complain his/her roll no. is used by     
                      some other student/person in feedback system. Admin should check in this case that user id  
                      linked with his/her roll no. matches with the user id given to him/her or not,if not        
                     admin should delete the user id linked with his/her roll no. immediately from              
                    'Delete Username' page to ensure that no one other than the student can use his/her roll no.
            </pre>
        </center>
    </asp:Panel>
</asp:Content>