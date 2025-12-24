<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="WebApplication.Department.DepartmentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />

    <asp:GridView ID="gvDepartments" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="Id"
        OnRowCommand="gvDepartments_RowCommand">

        <Columns>
             <asp:BoundField DataField="Id" HeaderText="Id" />
             <asp:BoundField DataField="Name" HeaderText="Name" />

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <!-- Edit -->
                    <asp:LinkButton ID="btnEdit" runat="server"
                        Text="Edit"
                        CommandName="EditRow"
                        CommandArgument="<%# Container.DataItemIndex %>" />

                    &nbsp;|&nbsp;

                    <!-- Delete -->
                    <asp:LinkButton ID="btnDelete" runat="server"
                        Text="Delete"
                        CommandName="DeleteRow"
                        CommandArgument="<%# Container.DataItemIndex %>"
                        OnClientClick='<%# "return confirm(\"Yakin hapus position: " + Eval("Name") + "?\");" %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>


    </asp:GridView>
</form>

</body>
</html>
