<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionList.aspx.cs" Inherits="WebApplication.Position.PositionList" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Position List</title>
</head>
<body>
<form id="form1" runat="server">

    <h2>Position List</h2>

    <asp:Button ID="btnAdd" runat="server" Text="Add Position" OnClick="btnAdd_Click" />
    <br /><br />

    <asp:GridView ID="gvPositions" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="Id"
        OnRowCommand="gvPositions_RowCommand">

        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Level" HeaderText="Level" />

            <asp:ButtonField Text="Edit" CommandName="EditRow" />
            <asp:ButtonField Text="Delete" CommandName="DeleteRow" />
        </Columns>
    </asp:GridView>

</form>
</body>
</html>
