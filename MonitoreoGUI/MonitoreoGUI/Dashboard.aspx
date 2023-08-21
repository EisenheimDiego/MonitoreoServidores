<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="MonitoreoGUI.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="CSS/EstiloDashboard.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Dashboard</title>
</head>
<body>
    <form runat="server">
        <div class="app">
            <header class="app-header">
                <div class="app-header-logo">
                    <div class="logo">
                        <span class="logo-icon">
                            <img src="https://assets.codepen.io/285131/almeria-logo.svg" />
                        </span>
                        <h1 class="logo-title">
                            <span>Monitoreo</span>
                            <span>Servidores</span>
                        </h1>
                    </div>
                </div>
                <div class="app-header-navigation">
                    <div class="tabs">
                        <a href="#" class="active">Servidor
                        </a>
                    </div>
                </div>
                <div class="app-header-actions">
                    <button class="user-profile">
                        <span id="usuarioLinea" runat="server">Usuario en linea</span>
                        <span>
                            <img src="https://assets.codepen.io/285131/almeria-avatar.jpeg" />
                        </span>
                    </button>
                </div>
                <div class="app-header-mobile">
                    <button class="icon-button large">
                        <i class="ph-list"></i>
                    </button>
                </div>
            </header>
            <div class="app-body">
                <div class="app-body-navigation">
                    <nav class="navigation">
                        <a href="#">
                            <i class="ph-browsers"></i>
                            <span>Dashboard</span>
                        </a>
                    </nav>
                    <footer class="footer">
                        <h1>Monitoreo<small>©</small></h1>
                        <div>
                            Desarrollado por:<br />
                            Victoria Cañas y<br />
                            Diego Solano<br />
                            2023
                        </div>
                    </footer>
                </div>
                <div class="app-body-main-content">
                    <div>
                        <asp:DropDownList runat="server" ID="ddlServidores"></asp:DropDownList>
                        <asp:Button runat="server" class="save-button" Text="Servicios" 
                            ID="btnServicio" OnClick="btnServicio_Click"/>
                        <asp:Button runat="server" class="save-button" Text="Encargados"
                            ID="btnEncargados" OnClick="btnEncargados_Click"/>
                    </div>
                    <div>
                        <asp:ImageButton runat="server" ID="btnMail"
                            AlternateText="Enviar Correo" class="save-button"
                            OnClick="btnMail_Click" ImageUrl="~/assets/mail.png"
                            ImageAlign="Middle"/>
                        <asp:ImageButton runat="server" ID="btnNotOn" 
                            AlternateText="Activar Notificaciones" class="save-button"
                            OnClick="btnNotOn_Click" ImageUrl="~/assets/notificacionOn.png"
                            ImageAlign="Middle"/>
                        <asp:ImageButton runat="server" ID="btnNotOff" 
                            AlternateText="Desactivar Notificaciones" class="save-button"
                            OnClick="btnNotOff_Click" ImageUrl="~/assets/notificacionOff.png"
                            ImageAlign="Middle"/>
                    </div>
                    <asp:Label runat="server" ID="lblError"></asp:Label>
                    <section class="service-section">
                        <asp:GridView ID="gridMonitoreos" runat="server"></asp:GridView>
                    </section>
                </div>
                <div class="app-body-sidebar">
                    <section class="payment-section">
                        <div class="payments">
                            <div class="payment">
                                <div class="payment-details">
                                    <div>
                                        <span>Encargados:</span>
                                    </div>
                                    <asp:GridView ID="gridEncargados" runat="server"></asp:GridView>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
