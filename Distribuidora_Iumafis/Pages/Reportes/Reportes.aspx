<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Reportes.Reportes" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Informes - Distribuidora Iumafis</title>
    <style>
        .tab-nav { display:flex; gap:4px; border-bottom:2px solid #9c27b0; margin-bottom:24px; flex-wrap:wrap; }
        .tab-btn { padding:10px 20px; border:none; background:transparent; cursor:pointer; font-size:14px; font-weight:600; color:#888; border-bottom:3px solid transparent; margin-bottom:-2px; transition:all 0.2s; border-radius:6px 6px 0 0; display:flex; align-items:center; gap:6px; }
        .tab-btn.active { color:#6a1b9a; border-bottom-color:#9c27b0; background:#faf4fc; }
        .tab-btn:hover:not(.active) { background:#f5f5f5; color:#555; }
        .tab-content { display:none; }
        .tab-content.active { display:block; }
        .report-summary { display:grid; grid-template-columns:repeat(auto-fit, minmax(160px,1fr)); gap:14px; margin-bottom:20px; }
        .summary-card { background:#faf4fc; padding:16px; border-radius:8px; text-align:center; border-left:3px solid #9c27b0; }
        .summary-card .s-value { font-size:22px; font-weight:700; color:#6a1b9a; }
        .summary-card .s-label { font-size:12px; color:#888; margin-top:4px; }
        @media print { .tab-nav, .search-bar, .btn, .navbar, .footer { display:none !important; } .tab-content { display:block !important; } }
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-chart-bar"></i> Informes y Estadisticas</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / Informes</div>
        </div>
        <button onclick="window.print()" class="btn btn-info"><i class="fa fa-print"></i> Imprimir</button>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-filter"></i> Filtro de Periodo</div>
        </div>
        <div class="search-bar">
            <div class="form-group">
                <label class="form-label">Desde</label>
                <asp:TextBox ID="txtDesde" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="form-group">
                <label class="form-label">Hasta</label>
                <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="form-group" style="align-self:flex-end;">
                <asp:Button ID="btnGenerar" runat="server" Text="Generar Informes" CssClass="btn btn-primary" OnClick="btnGenerar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" style="margin-left:6px;" />
            </div>
        </div>
    </div>

    <!-- RESUMEN GENERAL -->
    <asp:Panel ID="pnlResumen" runat="server">
        <div class="card">
            <div class="card-header">
                <div class="card-title"><i class="fa fa-tachometer-alt"></i> Resumen del Periodo</div>
            </div>
            <div class="report-summary">
                <div class="summary-card">
                    <div class="s-value"><asp:Label ID="lblTotalVentas" runat="server" Text="$0.00" /></div>
                    <div class="s-label">Total Ventas</div>
                </div>
                <div class="summary-card">
                    <div class="s-value"><asp:Label ID="lblTotalPedidos" runat="server" Text="0" /></div>
                    <div class="s-label">Pedidos</div>
                </div>
                <div class="summary-card">
                    <div class="s-value"><asp:Label ID="lblTotalProductosVendidos" runat="server" Text="0" /></div>
                    <div class="s-label">Unidades Vendidas</div>
                </div>
                <div class="summary-card">
                    <div class="s-value"><asp:Label ID="lblTotalCancelados" runat="server" Text="0" /></div>
                    <div class="s-label">Cancelados</div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- TABS -->
    <div class="card">
        <div class="tab-nav">
            <button class="tab-btn active" onclick="showTab('tabProductos',this)"><i class="fa fa-box"></i> Por Producto</button>
            <button class="tab-btn" onclick="showTab('tabClientes',this)"><i class="fa fa-users"></i> Por Cliente</button>
            <button class="tab-btn" onclick="showTab('tabFechas',this)"><i class="fa fa-calendar"></i> Por Fecha</button>
            <button class="tab-btn" onclick="showTab('tabCategorias',this)"><i class="fa fa-tags"></i> Por Categoria</button>
            <button class="tab-btn" onclick="showTab('tabEstados',this)"><i class="fa fa-info-circle"></i> Estados de Pedidos</button>
        </div>

        <!-- Tab: Por Producto -->
        <div id="tabProductos" class="tab-content active">
            <h3 style="color:#6a1b9a; margin-bottom:16px; font-size:17px;"><i class="fa fa-box"></i> Ventas por Producto</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvProductos" runat="server" CssClass="data-table" AutoGenerateColumns="false" EmptyDataText="">
                    <Columns>
                        <asp:BoundField DataField="Producto" HeaderText="Producto" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                        <asp:BoundField DataField="Unidades" HeaderText="Unidades Vendidas" />
                        <asp:BoundField DataField="Total" HeaderText="Total Ventas" DataFormatString="{0:C2}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblProductosVacio" runat="server" Visible="false" CssClass="empty-state">
                    <br/><i class="fa fa-box-open" style="font-size:48px;color:#ddd;"></i><br/>Sin datos para el periodo seleccionado.
                </asp:Label>
            </div>
        </div>

        <!-- Tab: Por Cliente -->
        <div id="tabClientes" class="tab-content">
            <h3 style="color:#6a1b9a; margin-bottom:16px; font-size:17px;"><i class="fa fa-users"></i> Ventas por Cliente</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvClientes" runat="server" CssClass="data-table" AutoGenerateColumns="false" EmptyDataText="">
                    <Columns>
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="Pedidos" HeaderText="Pedidos" />
                        <asp:BoundField DataField="Total" HeaderText="Total Compras" DataFormatString="{0:C2}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblClientesVacio" runat="server" Visible="false" CssClass="empty-state">
                    <br/><i class="fa fa-users" style="font-size:48px;color:#ddd;"></i><br/>Sin datos para el periodo seleccionado.
                </asp:Label>
            </div>
        </div>

        <!-- Tab: Por Fecha -->
        <div id="tabFechas" class="tab-content">
            <h3 style="color:#6a1b9a; margin-bottom:16px; font-size:17px;"><i class="fa fa-calendar"></i> Ventas por Fecha</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvFechas" runat="server" CssClass="data-table" AutoGenerateColumns="false" EmptyDataText="">
                    <Columns>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Pedidos" HeaderText="Pedidos" />
                        <asp:BoundField DataField="Total" HeaderText="Total Ventas" DataFormatString="{0:C2}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblFechasVacio" runat="server" Visible="false" CssClass="empty-state">
                    <br/><i class="fa fa-calendar" style="font-size:48px;color:#ddd;"></i><br/>Sin datos para el periodo seleccionado.
                </asp:Label>
            </div>
        </div>

        <!-- Tab: Por Categoria -->
        <div id="tabCategorias" class="tab-content">
            <h3 style="color:#6a1b9a; margin-bottom:16px; font-size:17px;"><i class="fa fa-tags"></i> Ventas por Categoria</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvCategorias" runat="server" CssClass="data-table" AutoGenerateColumns="false" EmptyDataText="">
                    <Columns>
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                        <asp:BoundField DataField="Unidades" HeaderText="Unidades Vendidas" />
                        <asp:BoundField DataField="Total" HeaderText="Total Ventas" DataFormatString="{0:C2}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblCategoriasVacio" runat="server" Visible="false" CssClass="empty-state">
                    <br/><i class="fa fa-tags" style="font-size:48px;color:#ddd;"></i><br/>Sin datos para el periodo seleccionado.
                </asp:Label>
            </div>
        </div>

        <!-- Tab: Estados de Pedidos -->
        <div id="tabEstados" class="tab-content">
            <h3 style="color:#6a1b9a; margin-bottom:16px; font-size:17px;"><i class="fa fa-info-circle"></i> Pedidos por Estado</h3>
            <div class="table-responsive">
                <asp:GridView ID="gvEstados" runat="server" CssClass="data-table" AutoGenerateColumns="false" EmptyDataText="">
                    <Columns>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# GetBadgeEstado(Eval("Estado").ToString()) %>'><%#Eval("Estado")%></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function showTab(tabId, btn) {
            document.querySelectorAll('.tab-content').forEach(t => t.classList.remove('active'));
            document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
            document.getElementById(tabId).classList.add('active');
            btn.classList.add('active');
        }
    </script>
</asp:Content>
