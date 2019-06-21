<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student_dashboard.aspx.cs" Inherits="student_dashboard"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Dashboard</title>
</head>
<body>
    <link href="css/studentFbDashboard.css" rel="stylesheet" type="text/css">
    <noscript>
        <div>
            You must enable javascript in your browser to continue
        </div>
    </noscript>
    <form id="access" runat="server">
        <div class="mainFrame">
            <div style="float: left; color: white">
                <asp:Label ID="studentId" runat="server" Text=""></asp:Label>
            </div>
            <div style="float: right;">
                <a href="student_login.aspx">
                    <asp:Button ID="logout" runat="server" Text="Logout" CssClass="logout_button" BackColor="#aea2c6" OnClick="logout_Click" />
                </a>
            </div>
            <br />
            <center>
                <asp:Label runat="server" Text="Welcome to Student Feedback System Dashboard" style="font-family:Georgia;color:Black; font-size: x-large;"></asp:Label>
            </center>
            <div class="content">
                <div class="panels">
                    <div class="panel1">
                        <br />
                        <asp:Button ID="profileStatusPanel" runat="server" Text="Check Profile Status" CssClass="panel-button1" OnClick="profileStatusPanel_Click" />
                    </div>
                    <div class="panel2">
                        <br />
                        <asp:Button ID="linkRollPanel" runat="server" Text="Link With Roll No." OnClick="linkRollPanel_Click" />
                    </div>
                    <div class="panel3">
                        <br />
                        <asp:Button ID="changePwdPanel" runat="server" Text="Change Password" CssClass="panel-button3" OnClick="changePwdPanel_Click" />
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="updatePanels">
                    <asp:Label ID="topMessage" runat="server" CssClass="topMessage"></asp:Label>
                    <br />
                    <asp:UpdatePanel ID="profilePanel" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                                <table class="tableBox">
                                    <tr>
                                        <td colspan="2">
                                            <center>
                                                 <asp:Label runat="server" cssclass="tableBoxLabel" Text="Profile Status"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td>User Id : </td>
                                        <td class="tableBoxTd2">
                                            <asp:Label ID="profileUser" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Stream : </td>
                                        <td class="tableBoxTd2">
                                            <asp:Label ID="profileStream" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Semester : </td>
                                        <td class="tableBoxTd2">
                                            <asp:Label ID="profileSem" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Roll No : </td>
                                        <td class="tableBoxTd2">
                                            <asp:Label ID="profileRoll" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Last Login : </td>
                                        <td class="tableBoxTd2">
                                            <asp:Label ID="profileLastLog" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Feedback Given : </td>
                                        <td class="tableBoxTd2">
                                            <asp:Label ID="profileNoFeedback" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="link" runat="server">
                        <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="linkButton" EventName="Click" />
                        </Triggers>--%>
                        <ContentTemplate>
                            <table class="tableBox">
                                <tr>
                                    <td colspan="2">
                                        <center>
                                                 <asp:Label runat="server" cssclass="tableBoxLabel" Text="Link Profile With Your Roll No"></asp:Label>
                                        </center>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <%--<tr>
                                    <td>User Id : </td>
                                    <td class="tableBoxTd2">
                                        <asp:Label ID="linkUser" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>Roll No :</td>
                                    <td class="tableBoxTd2">
                                        <asp:TextBox ID="linkRlText" runat="server" CssClass="textBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password :
                                    </td>
                                    <td class="tableBoxTd2">
                                        <asp:TextBox ID="linkPassword" runat="server" CssClass="textBox" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="tableBoxTd2">
                                        <asp:Button ID="linkButton" CssClass="styled-button-link" runat="server" Text="Submit" OnClick="linkButton_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><center><asp:Label ID="msg2" runat="server" Text=""></asp:Label></center></td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <pre style="float:left"><u>Warning</u> : Do not try to use roll no. of any other student. 
          You will be caught.
                            </pre>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="password" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <asp:Triggers>
                            <asp:AsyncPostBackTrigger ControlID="pwdButton" EventName="Click"/>
                        </asp:Triggers>
                        <ContentTemplate>
                            <table class="tableBox">
                                <tr>
                                    <td colspan="2">
                                        <center>
                                                 <asp:Label runat="server" cssclass="tableBoxLabel" Text="Change Your Password"></asp:Label>
                                        </center>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td>Current Password :</td>
                                    <td class="tableBoxTd2">
                                        <asp:TextBox ID="pwdCurrent" runat="server" CssClass="textBox" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>New Password :</td>
                                    <td class="tableBoxTd2">
                                        <asp:TextBox ID="pwdNew" runat="server" CssClass="textBox" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Confirm New Password :</td>
                                    <td class="tableBoxTd2">
                                        <asp:TextBox ID="pwdConfirm" runat="server" CssClass="textBox" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="tableBoxTd2">
                                        <asp:Button ID="pwdButton" CssClass="styled-button-link" runat="server" Text="Change Password" OnClick="pwdButton_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><center><asp:Label ID="msg3" runat="server" Text=""></asp:Label></center></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <table class="tableBox">
                        <tr>
                            <td colspan="2">
                                <center><asp:Button ID="giveFeedback" runat="server" CssClass="feedbackButton" Text="Go Give Feedback>>" OnClick="giveFeedback_Click" /></center>
                            </td>
                        </tr>
                        <tr><td colspan="2" style="visibility:hidden">xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx</td></tr>
                        <tr><td colspan="2"></td></tr>
                        <tr>
                            <td colspan="2">
                                <center><asp:Label ID="msg" runat="server" Text="" CssClass="message"></asp:Label></center>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
