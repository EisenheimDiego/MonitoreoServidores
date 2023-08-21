<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardDetallado.aspx.cs" Inherits="MonitoreoGUI.DashboardDetallado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="CSS/EstiloDashboard.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle</title>
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
				<a href="#" class="active">
					Servidor detallado
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
			<section class="service-section">
				<h2 runat="server" id="codServ">Código: 00</h2>
				<div class="tiles">
					<article id="CPU" class="green" runat="server">
						<div class="tile-header">
							<i class="ph-lightning-light"></i>
							<h3>
								<span>CPU</span>
								<span>Porcentaje de uso:</span>
							</h3>
						</div>
						<span runat="server" id="porCPU">50%</span>
					</article>
					<article id="Memoria" class="yellow" runat="server">
						<div class="tile-header">
							<i class="ph-fire-simple-light"></i>
							<h3>
								<span>Memoria</span>
								<span>Porcentaje de uso:</span>
							</h3>
						</div>
						<span runat="server" id="porMem">30%</span>
					</article>
					<article id="disco" class="red" runat="server">
						<div class="tile-header">
							<i class="ph-file-light"></i>
							<h3>
								<span>Disco</span>
								<span>Porcentaje de uso:</span>
							</h3>
						</div>
						<span runat="server" id="porDisco">90%</span>
					</article>
				</div>
			</section>
            <div class="tiles">
                <h2>Estado:</h2>
                <div class="payment">
                    <div class="card gray">
                        <h3 runat="server" id="estadoServ">Advertencia </h3>
                    </div>
                </div>
            </div>
			<section class="transfer-section">
				<div class="transfer-section-header">
					<div class="faq">
					<h2>Servicios</h2>
					<div>
						<input type="text" placeholder="Código del servicio"/>
					</div>
					</div>
				</div>
				<div class="transfers">
					<div class="transfer">
						<asp:GridView runat="server" ID="servicios">
						</asp:GridView>
					</div>
				</div>
			</section>
		</div>
		<div class="app-body-sidebar">
            <div>
                <asp:DropDownList runat="server" ID="ddlServicios"></asp:DropDownList>
            </div>
            <asp:ImageButton runat="server" ID="btnNotOn"
                AlternateText="Activar Notificaciones" class="save-button"
                OnClick="btnNotOn_Click" ImageUrl="~/assets/notificacionOn.png"
                ImageAlign="Middle" />
            <asp:ImageButton runat="server" ID="btnNotOff"
                AlternateText="Desactivar Notificaciones" class="save-button"
                OnClick="btnNotOff_Click" ImageUrl="~/assets/notificacionOff.png"
                ImageAlign="Middle" />
            <asp:Label runat="server" ID="lblError"></asp:Label>
            <section class="payment-section">
                <div class="payments">
                    <div class="payment">
						<div class="payment-details">
							<h3>Encargados</h3>
							<asp:GridView runat="server" ID="gridEncargados"></asp:GridView>
						</div>
					</div>
				</div>
				<div class="faq">
					<p>Notificación de error a encargados</p>
					<div>
						<label>Destinatarios</label>
						<input type="text" placeholder="Ej: corr1@gmail.com"/>
					</div>
				</div>
				<div class="payment-section-footer">
					<button class="save-button">
						Enviar correo
					</button>
				</div>
			</section>
		</div>
	</div>
</div>
    </form>
</body>
</html>
