<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Usuarios.Login" %>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Iniciar Sesión - Distribuidora Iumafis</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
    <style>
        * { box-sizing: border-box; margin: 0; padding: 0; }
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background: linear-gradient(135deg, #4a148c 0%, #9c27b0 50%, #ce93d8 100%); min-height: 100vh; display: flex; align-items: center; justify-content: center; }
        .login-container { width: 100%; max-width: 420px; padding: 20px; }
        .login-card { background: #fff; border-radius: 16px; box-shadow: 0 20px 60px rgba(0,0,0,0.3); overflow: hidden; }
        .login-header { background: linear-gradient(135deg, #6a1b9a, #9c27b0); padding: 40px 36px 30px; text-align: center; color: #fff; }
        .login-logo { width: 72px; height: 72px; background: rgba(255,255,255,0.2); border-radius: 50%; margin: 0 auto 16px; display: flex; align-items: center; justify-content: center; font-size: 32px; }
        .login-title { font-size: 24px; font-weight: 700; margin-bottom: 4px; }
        .login-subtitle { font-size: 13px; opacity: 0.8; }
        .login-body { padding: 36px; }
        .form-group { margin-bottom: 20px; }
        .form-label { display: block; font-weight: 600; font-size: 13px; color: #555; margin-bottom: 7px; }
        .input-wrapper { position: relative; }
        .input-icon { position: absolute; left: 13px; top: 50%; transform: translateY(-50%); color: #9c27b0; font-size: 15px; }
        .form-control { width: 100%; padding: 11px 12px 11px 40px; border: 1.5px solid #ddd; border-radius: 8px; font-size: 14px; transition: border 0.2s, box-shadow 0.2s; }
        .form-control:focus { outline: none; border-color: #9c27b0; box-shadow: 0 0 0 3px rgba(156,39,176,0.12); }
        .btn-login { width: 100%; padding: 13px; background: linear-gradient(135deg, #6a1b9a, #9c27b0); color: #fff; border: none; border-radius: 8px; font-size: 15px; font-weight: 600; cursor: pointer; display: flex; align-items: center; justify-content: center; gap: 8px; transition: opacity 0.2s; margin-top: 8px; }
        .btn-login:hover { opacity: 0.9; }
        .alert { padding: 12px 16px; border-radius: 8px; margin-bottom: 16px; font-size: 13px; display: flex; align-items: center; gap: 8px; }
        .alert-danger { background: #ffebee; color: #c62828; border-left: 4px solid #f44336; }
        .login-footer { text-align: center; padding: 16px; background: #faf4fc; font-size: 12px; color: #888; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="login-card">
                <div class="login-header">
                    <div class="login-logo"><i class="fa fa-spa"></i></div>
                    <div class="login-title">Distribuidora Iumafis</div>
                    <div class="login-subtitle">Sistema de Gestión Comercial</div>
                </div>
                <div class="login-body">
                    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
                        <i class="fa fa-exclamation-circle"></i>
                        <asp:Label ID="lblError" runat="server" />
                    </asp:Panel>

                    <div class="form-group">
                        <label class="form-label">Usuario</label>
                        <div class="input-wrapper">
                            <i class="fa fa-user input-icon"></i>
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario" />
                        </div>
                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="txtUsuario"
                            ErrorMessage="El usuario es obligatorio." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                    </div>

                    <div class="form-group">
                        <label class="form-label">Contraseña</label>
                        <div class="input-wrapper">
                            <i class="fa fa-lock input-icon"></i>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Ingrese su contraseña" />
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="La contraseña es obligatoria." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                    </div>

                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" CssClass="btn-login"
                        OnClick="btnLogin_Click" />
                </div>
                <div class="login-footer">
                    &copy; <%=DateTime.Now.Year%> Distribuidora Iumafis &mdash; Acceso solo para empleados
                </div>
            </div>
        </div>
    </form>
</body>
</html>
