USE [tiusr5pl_ProyectoPrograV]
GO
/****** Object:  Table [dbo].[Componente]    Script Date: 15/3/2023 22:38:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Componente](
	[CodigoC] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CodigoC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EncargadoServicio]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncargadoServicio](
	[Usuario] [varchar](50) NOT NULL,
	[CodigoServicio] [int] NOT NULL,
	[Notificaciones] [bit] NULL,
 CONSTRAINT [pkEnServicio] PRIMARY KEY CLUSTERED 
(
	[Usuario] ASC,
	[CodigoServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EncargadoServidor]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncargadoServidor](
	[Usuario] [varchar](50) NOT NULL,
	[Codigo] [int] NOT NULL,
	[Notificaciones] [bit] NULL,
 CONSTRAINT [pkEnServidor] PRIMARY KEY CLUSTERED 
(
	[Usuario] ASC,
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitoreoServicio]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitoreoServicio](
	[MonitoreoID] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[CodigoServicio] [int] NOT NULL,
	[Timeout] [float] NOT NULL,
	[Disponibilidad] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MonitoreoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitoreoServidor]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitoreoServidor](
	[MonitoreoID] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Codigo] [int] NOT NULL,
	[CPU] [float] NOT NULL,
	[Memoria] [float] NOT NULL,
	[Disco] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MonitoreoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parametro]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametro](
	[CodigoP] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CodigoP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParametroServicio]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParametroServicio](
	[CodigoP] [int] NOT NULL,
	[CodigoServicio] [int] NOT NULL,
	[Valor] [varchar](50) NULL,
 CONSTRAINT [pkParametroServicio] PRIMARY KEY CLUSTERED 
(
	[CodigoP] ASC,
	[CodigoServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicio]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicio](
	[CodigoServicio] [int] NOT NULL,
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](255) NULL,
	[Timeout] [float] NOT NULL,
	[Tipo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[CodigoServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servidor]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servidor](
	[Codigo] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](255) NULL,
	[UsuarioAdmin] [varchar](50) NOT NULL,
	[Contrasena] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Umbral]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Umbral](
	[CodigoUmbral] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CodigoUmbral] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UmbralComponente]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UmbralComponente](
	[Codigo] [int] NOT NULL,
	[CodigoC] [int] NOT NULL,
	[CodigoUmbral] [int] NOT NULL,
	[Porcentaje] [float] NOT NULL,
 CONSTRAINT [pkUmbralComponente] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC,
	[CodigoC] ASC,
	[CodigoUmbral] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Usuario] [varchar](50) NOT NULL,
	[Correo] [varchar](255) NOT NULL,
	[Contrasena] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EncargadoServicio] ADD  DEFAULT ((1)) FOR [Notificaciones]
GO
ALTER TABLE [dbo].[EncargadoServidor] ADD  DEFAULT ((1)) FOR [Notificaciones]
GO
ALTER TABLE [dbo].[EncargadoServicio]  WITH CHECK ADD FOREIGN KEY([CodigoServicio])
REFERENCES [dbo].[Servicio] ([CodigoServicio])
GO
ALTER TABLE [dbo].[EncargadoServicio]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[Usuarios] ([Usuario])
GO
ALTER TABLE [dbo].[EncargadoServidor]  WITH CHECK ADD FOREIGN KEY([Codigo])
REFERENCES [dbo].[Servidor] ([Codigo])
GO
ALTER TABLE [dbo].[EncargadoServidor]  WITH CHECK ADD FOREIGN KEY([Usuario])
REFERENCES [dbo].[Usuarios] ([Usuario])
GO
ALTER TABLE [dbo].[MonitoreoServicio]  WITH CHECK ADD FOREIGN KEY([CodigoServicio])
REFERENCES [dbo].[Servicio] ([CodigoServicio])
GO
ALTER TABLE [dbo].[MonitoreoServidor]  WITH CHECK ADD FOREIGN KEY([Codigo])
REFERENCES [dbo].[Servidor] ([Codigo])
GO
ALTER TABLE [dbo].[ParametroServicio]  WITH CHECK ADD FOREIGN KEY([CodigoP])
REFERENCES [dbo].[Parametro] ([CodigoP])
GO
ALTER TABLE [dbo].[ParametroServicio]  WITH CHECK ADD FOREIGN KEY([CodigoServicio])
REFERENCES [dbo].[Servicio] ([CodigoServicio])
GO
ALTER TABLE [dbo].[Servicio]  WITH CHECK ADD FOREIGN KEY([Codigo])
REFERENCES [dbo].[Servidor] ([Codigo])
GO
ALTER TABLE [dbo].[UmbralComponente]  WITH CHECK ADD FOREIGN KEY([Codigo])
REFERENCES [dbo].[Servidor] ([Codigo])
GO
ALTER TABLE [dbo].[UmbralComponente]  WITH CHECK ADD FOREIGN KEY([CodigoC])
REFERENCES [dbo].[Componente] ([CodigoC])
GO
ALTER TABLE [dbo].[UmbralComponente]  WITH CHECK ADD FOREIGN KEY([CodigoUmbral])
REFERENCES [dbo].[Umbral] ([CodigoUmbral])
GO
/****** Object:  StoredProcedure [dbo].[Dashboard]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Dashboard]
@Servidor int
as
begin

select Servidor.Codigo, Servidor.Nombre, MonitoreoServidor.Fecha as 'Ultima fecha',
case when MonitoreoServidor.CPU > --CUANDO EL USO DEL CPU SEA MÁS GRANDE
(select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 1 and UmbralComponente.CodigoUmbral = 2) --QUE EL USO ADVERTENCIA
then 'Error'
when MonitoreoServidor.Disco > --O CUANDO EL USO DEL DISCO SEA MÁS GRANDE
(select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 3 and UmbralComponente.CodigoUmbral = 2) --QUE EL USO NORMAL
then 'Error'
when MonitoreoServidor.Memoria > --O CUANDO EL USO DE LA MEMORIA SEA MÁS GRANDE
(select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 2 and UmbralComponente.CodigoUmbral = 2) --QUE EL USO NORMAL
then 'Error'
when (MonitoreoServidor.CPU > --CUANDO EL USO DEL CPU SEA MÁS GRANDE
(select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 1 and UmbralComponente.CodigoUmbral = 1) --QUE EL USO NORMAL
and MonitoreoServidor.CPU < (select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 1 and UmbralComponente.CodigoUmbral = 2))
then 'Advertencia'
when (MonitoreoServidor.Disco > --O CUANDO EL USO DEL DISCO SEA MÁS GRANDE
(select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 3 and UmbralComponente.CodigoUmbral = 1) --QUE EL USO NORMAL
and MonitoreoServidor.CPU < (select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 3 and UmbralComponente.CodigoUmbral = 2))
then 'Advertencia'
when (MonitoreoServidor.Memoria > --O CUANDO EL USO DE LA MEMORIA SEA MÁS GRANDE
(select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 2 and UmbralComponente.CodigoUmbral = 1) --QUE EL USO NORMAL
and MonitoreoServidor.CPU < (select Porcentaje from UmbralComponente where UmbralComponente.Codigo = @Servidor
and UmbralComponente.CodigoC = 2 and UmbralComponente.CodigoUmbral = 2))
then 'Advertencia' else 'Normal' end as 'Estado',
MonitoreoServidor.CPU as 'Uso de CPU',
MonitoreoServidor.Memoria as 'Uso de Memoria',
MonitoreoServidor.Disco as 'Uso de Disco'
--Servicio.CodigoServicio as 'Codigo Servicio',
--Servicio.Nombre as 'Nombre Servicio',
--MonitoreoServicio.Disponibilidad
from Servidor, MonitoreoServidor
where Servidor.Codigo = @Servidor and MonitoreoServidor.Codigo = @Servidor
and MonitoreoServidor.Codigo = Servidor.Codigo
and MonitoreoServidor.Fecha in (select max(Fecha) from MonitoreoServidor where Codigo = @Servidor)
--and Servicio.Codigo = @Servidor and MonitoreoServicio.CodigoServicio = Servicio.CodigoServicio
group by Servidor.Codigo, Servidor.Nombre, MonitoreoServidor.Fecha,
MonitoreoServidor.CPU, MonitoreoServidor.Memoria, MonitoreoServidor.Disco
--Servicio.CodigoServicio, Servicio.Nombre, MonitoreoServicio.Disponibilidad
order by MonitoreoServidor.Fecha desc

end
GO
/****** Object:  StoredProcedure [dbo].[InicioSesion]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InicioSesion]
@usuario varchar(50),
@contrasena varchar(255)
as
begin
	if exists(select 1 from Usuarios where usuario = @usuario and contrasena = @contrasena)
	select 1
	else
	select 0
end
GO
/****** Object:  StoredProcedure [dbo].[Notificaciones]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Notificaciones]
@tipo int,
@usuario varchar(50),
@codigo int,
@valor int
as
begin

	if @tipo = 1
	begin
		update EncargadoServidor set Notificaciones = @valor where Usuario = @usuario and Codigo = @codigo
	end
	else begin
		update EncargadoServicio set Notificaciones = @valor where Usuario = @usuario and CodigoServicio = @codigo
	end

end
GO
/****** Object:  StoredProcedure [dbo].[ServicioServidor]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ServicioServidor]
@servidor int
as
begin
	select * from Servicio where Codigo = @servidor
end
GO
/****** Object:  StoredProcedure [dbo].[ServidorNombre]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ServidorNombre]
@Nombre varchar(50)
as
begin

	select * from Servidor where lower(Nombre) like ('%'+@Nombre+'%')

end
GO
/****** Object:  StoredProcedure [dstask18].[Correos]    Script Date: 15/3/2023 22:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dstask18].[Correos]
@valor int,
@id int
as
begin

if @valor = 1
begin

	select Usuarios.Correo from EncargadoServidor, Usuarios
	where Usuarios.Usuario = EncargadoServidor.Usuario and
	EncargadoServidor.Codigo = @id and EncargadoServidor.Notificaciones = 1

end else
begin
	
	select Usuarios.Correo from EncargadoServicio, Usuarios
	where Usuarios.Usuario = EncargadoServicio.Usuario and
	EncargadoServicio.CodigoServicio = @id and EncargadoServicio.Notificaciones = 1

end

end
GO
