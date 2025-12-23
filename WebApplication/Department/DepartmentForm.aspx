<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentForm.aspx.cs" Inherits="WebApplication.Department.DepartmentForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

<form id="form1" runat="server">
    <h2><asp:Label ID="lblTitle" runat="server" /></h2>

    Nama:<br />
    <asp:TextBox ID="txtName" runat="server" />
    <asp:RequiredFieldValidator runat="server"
        ControlToValidate="txtName"
        ErrorMessage="Nama wajib diisi"
        ForeColor="Red" /><br /><br />

    <asp:Button ID="btnSave" runat="server" Text="Simpan" OnClick="btnSave_Click" />
    <asp:Button ID="btnBack" runat="server" Text="Kembali" OnClick="btnBack_Click" />
</form>

</body>
</html>
