<%@ Page Title="Password Change Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function Allvalidate() {
            var ValidationSummary = "", a1 = "", b1 = "", c1 = "";

            a1 = uidValidation();
            b1 = pwdValidation();
            c1 = newpwdValidation();

            ValidationSummary = a1 + b1 + c1;

            if (a1 != "" || b1 != "" || c1 != "") {
                alert(ValidationSummary);
                return false;
            }
            else {
                //alert("Item added successfuly");
                return true;
            }
        }

        function uidValidation() {
            var uid;
            var controlId1 = document.getElementById("<%=TextBoxuid.ClientID %>");
           uid = controlId1.value;

           var val = /^[a-zA-Z0-9]+$/;

           if (uid == "") {
               return ("Please enter userid." + "\n");
           }
           else if (val.test(uid)) {
               return "";
           }
           else {
               return ("User id accepts only alphanumeric charcters." + "\n");
           }
       }

       function pwdValidation() {
           var pwd;
           var controlId2 = document.getElementById("<%=TextBoxpwd.ClientID %>");
           pwd = controlId2.value;

           var val = /^[a-zA-Z0-9]+$/;

           if (pwd == "") {
               return ("Please enter old password." + "\n");
           }
           else if (val.test(pwd)) {
               return "";
           }
           else {
               return ("1. Old password accepts only alphanumeric characters." + "\n");
           }
       }

       function newpwdValidation() {

           var controlId1 = document.getElementById("<%=TextBoxnewpwd.ClientID %>");
           var newpwd = controlId1.value;

           var controlId2 = document.getElementById("<%=TextBoxcnfpwd.ClientID %>");
           var cnfpwd = controlId2.value;

           var val = /^[a-zA-Z0-9]+$/;

           if (newpwd == "") {
               return ("Please enter new passwords" + "\n");
           }
           else if (cnfpwd == "") {
               return ("Please enter confirm new passwords" + "\n");
           }
           else if (val.test(newpwd) && val.test(cnfpwd) && newpwd == cnfpwd) {
               return "";
           }
           else if (newpwd != cnfpwd) {
               return ("New Password and Confirm password does not match." + "\n");
           }
           else {
               return ("Password accepts only alphanumeric characters." + "\n");
           }
       }

    </script>

    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" runat="server">
    Change password of <asp:Label ID="Label1" runat="server"></asp:Label>.
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="access" runat="server">
         <fieldset>
                <br/>
                <h3>Change Password Console</h3>
                </p>

                <table frame="box">                        
                     <tr>
                              <td class="auto-style1">User id  &nbsp&nbsp</td>
                              <td class="auto-style1"> <asp:TextBox ID="TextBoxuid" runat="server" Text="" Width="150"></asp:TextBox></td>
                     </tr>

                     <tr>
                              <td>Old password &nbsp &nbsp</td>
                              <td>
                                  <asp:TextBox ID="TextBoxpwd" TextMode="password" runat="server" Width="150"></asp:TextBox>                                  
                             </td>
                     </tr>  
                     <tr>
                              <td>New password &nbsp &nbsp</td>
                              <td>
                                  <asp:TextBox ID="TextBoxnewpwd" TextMode="password" runat="server" Width="150"></asp:TextBox>                                  
                             </td>
                     </tr>  
                     <tr>
                              <td>Confirm new password &nbsp; &nbsp;</td>
                              <td>
                                  <asp:TextBox ID="TextBoxcnfpwd" TextMode="password" runat="server" Width="150"></asp:TextBox>                                  
                             </td>
                     </tr>  
                                             
                     <tr>
                        <td colspan="2">
                        <asp:Button ID="btnsubmit" runat="server" Text="Change password" Width="150" onClientClick="return Allvalidate()" onclick="btnlogin_Click" Style="cursor: pointer;"/>
                        </td>
                    </tr>
                </table>

            </fieldset>
            <br/>

            <asp:Label ID="Labelstate" runat="server" Text="" Width="400"></asp:Label>
        </asp:Panel>
</asp:Content>
