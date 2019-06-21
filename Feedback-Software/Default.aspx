<%@ Page Title="Admin Home" Language="C#" MasterPageFile="~/admin.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" CodeFile="~/Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        <%--$(function () {
            var isShiftPressed = false;
            var isCapsOn = null;
            var nm = document.getElementById("<%=tbname.ClientID %>");
            $("#tbname").bind("keydown", function (e) {
                var keyCode = e.keyCode ? e.keyCode : e.which;
                if (keyCode == 16) {
                    isShiftPressed = true;
                }
            });
            $("#tbname").bind("keyup", function (e) {
                var keyCode = e.keyCode ? e.keyCode : e.which;
                if (keyCode == 16) {
                    isShiftPressed = false;
                }
                if (keyCode == 20) {
                    if (isCapsOn == true) {
                        isCapsOn = false;
                        $("#error").hide();
                    } else if (isCapsOn == false) {
                        isCapsOn = true;
                        $("#error").show();
                    }
                }
            });
            $("#tbname").bind("keypress", function (e) {
                var keyCode = e.keyCode ? e.keyCode : e.which;
                if (keyCode >= 65 && keyCode <= 90 && isShiftPressed) {
                    isCapsOn = true;
                    $("#error").show();
                } else {
                    $("#error").hide();
                }
            });
        });--%>
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
        var controlId = document.getElementById("<%=tbname.ClientID %>");
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
        var controlId = document.getElementById("<%=tbpass.ClientID %>");
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
    <style type="text/css">
        /*#error
        {
            border:1px solid #ffff66;
            background-color:#ffffcc;
            display:inline-block;
            margin-left:10px;
            padding:3px;
            display:none;
        }*/
        .auto-style1 {
            width: 54px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="css/txtStyles.css" rel="stylesheet" type="text/css">
    <noscript>
        <div>
            You must enable javascript in your browser to continue
        </div>
    </noscript>
    <p style="font-size: large; font-weight: bold; color: white;">
        Login Console
    </p>
    <br />
    <table frame="box" cellspacing="20" style="background-color: lightgray; color: black; font-family: Arial">
        <tr>
            <td class="auto-style1"><b>User id  &nbsp&nbsp</b></td>
            <td>
                <asp:TextBox ID="tbname" runat="server" CssClass="bg-change"></asp:TextBox><%--<span id="error">Caps Lock is ON</span>--%></td>
        </tr>

        <tr>
            <td class="auto-style1"><b>Password &nbsp &nbsp</b></td>
            <td>
                <asp:TextBox ID="tbpass" TextMode="password" runat="server" CssClass="bg-change"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <center>
                            <asp:Button ID="btnlogin" runat="server" Text="Login" Width="150" onClientClick="return Allvalidate()" onclick="btnlogin_Click" Style="cursor: pointer;"/>
                            </center>
            </td>
        </tr>
    </table>

    <br />

    <asp:Label ID="Labelstate" runat="server" Text="" ForeColor="#006600"></asp:Label>

</asp:Content>