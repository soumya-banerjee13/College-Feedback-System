<%@ Page Title="Comment Entry Page" Language="C#" MasterPageFile="~/adminMaster.master" AutoEventWireup="true" CodeFile="comment.aspx.cs" Inherits="comment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <center>
        <asp:UpdatePanel ID="access" runat="server">
            <ContentTemplate>
                <asp:Label ID="lst" runat="server" style="font-size: large" ForeColor="#3366CC"></asp:Label>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" OnPageIndexChanging="GridView1_PageIndexChanging" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="comm" HeaderText="Comment" SortExpression="comm" />
                        <asp:BoundField DataField="comm_marks" HeaderText="Additional Marks over its Option"  />
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
                <br />
                <div id="hyp" runat="server"></div>
                <br /><br /><br /><br />
                <asp:Label runat="server" Text="Insertion Form of New Comment" style="font-size: large" ForeColor="#116820"></asp:Label>
                <br /><br />
                <table>
                    <tr>
                        <td>Related Option Name&nbsp; :&nbsp;&nbsp; </td>
                        <td><asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>Select an Option</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>New Comment&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp; </td>
                        <td><asp:TextBox ID="TextBox1" runat="server" Width="130px" ToolTip="Write a Comment within 50 letters"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Additional Marks&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp; </td>
                        <td><asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>Select marks</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr style="height:3px"><td></td></tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="addComment" runat="server" Text="Add New Comment" OnClick="addComment_Click" Width="120px" Style="cursor: pointer;"></asp:Button></td>
                    </tr>
                </table>
                <br />
                <asp:Label id="msg1" runat="server"></asp:Label>
                <br /><br /><br /><br />
                <asp:Label runat="server" Text="Deletion Form of Old Comments" style="font-size: large" ForeColor="#CC6699"></asp:Label>
                <br /><br />
                <table>
                    <tr>
                        <td>Related Option Name&nbsp; :&nbsp;&nbsp; </td>
                        <td><asp:DropDownList ID="DropDownList3" runat="server" Width="120px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>Select an Option</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Old Comment Name&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp; </td>
                        <td><asp:DropDownList ID="DropDownList4" runat="server">
                            <asp:ListItem>Select Comment</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr style="height:3px"><td></td></tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="remComment" runat="server" Text="Delete Old Comment" Width="130px" OnClick="remComment_Click" Style="cursor: pointer;"></asp:Button></td>
                    </tr>
                </table>
                <br />
                <asp:Label id="msg2" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>

