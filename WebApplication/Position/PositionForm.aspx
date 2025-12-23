<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionForm.aspx.cs" Inherits="WebApplication.Position.PositionForm" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Position Form</title>
</head>
<body>
<form id="form1" runat="server">

    <h2><asp:Label ID="lblTitle" runat="server" /></h2>

    Name:<br />
    <asp:TextBox ID="txtName" runat="server" />
    <asp:RequiredFieldValidator runat="server"
        ControlToValidate="txtName"
        ErrorMessage="Name wajib diisi"
        ForeColor="Red" />
    <br /><br />

    Level:<br />
    <asp:TextBox ID="txtLevel" runat="server" />
    <br /><br />

    <asp:Button ID="btnSave" runat="server" Text="Simpan" OnClick="btnSave_Click" />
    <asp:Button ID="btnBack" runat="server" Text="Kembali" OnClick="btnBack_Click" />
    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
    <asp:Button ID="btnMove" runat="server" Text="Employee List" CausesValidation="false" OnClick="btnMove_Click" />
</form>
    
</body>
</html>
