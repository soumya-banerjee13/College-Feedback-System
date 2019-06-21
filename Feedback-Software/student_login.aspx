<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student_login.aspx.cs" Inherits="student_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Login Page</title>
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
    </style>
</head>
<body>
    <noscript>
        <div>
            You must enable javascript in your browser to continue
        </div>
    </noscript>
    <script type="text/javascript">
        function Allvalidate() {
            var ValidationSummary = "", a1 = "", b1 = "";
            a1 = uidValidation();
            b1 = pwdValidation();

            ValidationSummary = a1 + b1;

            if (a1 != "" || b1 != "") {
                alert(ValidationSummary);
                return false;
            }
            else {
                //alert("Order created successfuly");
                return true;
            }
        }

        function uidValidation() {
            var name;
            var controlId = document.getElementById("<%=user.ClientID %>");
                name = controlId.value;

                var val = /^[a-zA-Z0-9]+$/;
                if (name == "") {
                    return ("Please enter user id" + "\n");
                }
                else {
                    return "";
                }
            }

            function pwdValidation() {
                var pwd;
                var controlId = document.getElementById("<%=password.ClientID %>");
            pwd = controlId.value;

            var val = /^[a-zA-Z0-9 ]+$/;
            if (pwd == "") {
                return ("Please enter password." + "\n");
            }
            else {
                return "";
            }
        }
    </script>
    <link href="css/txtStyles.css" rel="stylesheet" type="text/css">
    <link href="css/LoginPageCSS.css" rel="stylesheet" type="text/css">
    <link href="css/grdView.css" rel="stylesheet" type="text/css">
    <center>
        <div style="margin-top:15px;">
        <img src="gcettb_logo.jpg"; style="height: 200px; width: 200px; opacity:0.8; margin-left: 11px;"h2"><br /><br />
        </div>
        <div style="font-family:Georgia; font-size:x-large">
            <asp:Label runat="server" Text="GCETTB Students Feedback System"></asp:Label>
        </div>
    <form id="form1" runat="server" class="tableBox">
        <label><span class="auto-style1">Login Form</span></label><br />
            User Name :    <asp:TextBox ID="user" runat="server" CssClass="shadow-beauty"></asp:TextBox><br />
            Password :&nbsp;&nbsp;
                <asp:TextBox ID="password" runat="server" CssClass="shadow-beauty" TextMode="Password"></asp:TextBox><br />
            <asp:Button ID="login_button" runat="server" Text="Login" onClientClick="return Allvalidate()" OnClick="login_button_Click" CssClass="student_login_button"/>
        <br />
    </form>
        <asp:Label ID="Labelstate" runat="server" Text="" ForeColor="#006600"></asp:Label>
    </center>
    <footer>
        <center><p style="color:white;">Designed by- Some GCETTBIAN</p></center>
    </footer>
</body>
</html>
