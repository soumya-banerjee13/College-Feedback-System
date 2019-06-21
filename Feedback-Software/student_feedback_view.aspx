<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student_feedback_view.aspx.cs" Inherits="student_feedback_view" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        Students Feedback
    </title>
</head>
<body>
    <%--<link href="css/grdView.css" rel="stylesheet" type="text/css">--%>
    <%--<script type="text/javascript">
        function show(divToShow) {
            var div = document.getElementById(divToShow);
            div.style.display = (div.style.display == "none") ? "block" : "none";
        }
        <div style="float:right"><input type="button" id="hideshow1" onclick="show('comment1')" value="hide/show"/>
    </script>--%>
    <noscript>
        <div>
            You must enable javascript in your browser to continue
        </div>
    </noscript>
    <link href="css/feedbackForm.css" rel="stylesheet" type="text/css">
    <link href="css/grdView.css" rel="stylesheet" type="text/css">
    <script type="text/javascript">
        function validateRadios(radioNo) {
            var selectionCount = 0;
            for (i = 0; i < document.forms[0].length; i++) {
                e = document.forms[0].elements[i];
                if (e.id.indexOf('Option') != -1 && e.checked) {
                    selectionCount++;
                }
            }
            if (selectionCount != radioNo) {
                alert('Choose any option for each subject');
                return false;
            }
            return true;
        }
    </script>
    <form id="mainForm1" runat="server" class="innerbody">
        <div style="float: left; color: white">
            <asp:Label ID="studentId" runat="server" Text=""></asp:Label>
        </div>
        <div style="float: right;">
            <%--<input type="button" value="Logout" class="feedback_view_button" onclick="window.location='student_login.aspx'" />--%>
            <a href="student_login.aspx">
                <asp:Button ID="logout" runat="server" Text="Logout" CssClass="logout_button" BackColor="#aea2c6" OnClick="logout_Click" />
            </a>
        </div>
        <br />
        <center>
            <asp:Label ID="formName" runat="server" Text="Label" style="font-family:'Times New Roman';color:white; font-size: 175%;"></asp:Label>
        </center>
        <br />
        <div id="access" class="mainForm" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Table ID="subjViewTable" class="mainForm" runat="server">
                        <asp:TableRow Style="margin-bottom: 50px;">
                            <asp:TableCell Style="padding-right: 20px;"><center><u>Subject</u></center></asp:TableCell>
                            <asp:TableCell Style="padding-right: 20px;"><center><u>Options</u></center></asp:TableCell>
                            <asp:TableCell> &nbsp;</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <center>
                <asp:Button ID="feedbackSubmit" runat="server" Text="Submit Feedback" CssClass="feedback_view_button" OnClick="feedbackSubmit_Click"/><br />
                <asp:Button ID="resetButton" runat="server" Text="Reset Responses" CssClass="feedback_view_button" OnClick="resetButton_Click"/>
            </center>
            <br />
            <br />
            <center>
                <asp:Label ID="msg" runat="server" Text="" style="font-family:Georgia;"></asp:Label>
            </center>
        </div>
 </form>
</body>
</html>
