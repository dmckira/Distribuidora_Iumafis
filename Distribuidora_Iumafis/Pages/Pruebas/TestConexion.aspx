<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestConexion.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.TestConexion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prueba de conexión</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Prueba de conexión MySQL</h2>

            <asp:Button ID="btnProbar" runat="server" Text="Probar conexión" OnClick="btnProbar_Click" />

            <br /><br />

            <asp:Label ID="lblResultado" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>