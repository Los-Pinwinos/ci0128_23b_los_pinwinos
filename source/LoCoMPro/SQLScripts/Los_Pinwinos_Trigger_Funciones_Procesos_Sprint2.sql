use Equipo2

-- Funciones y procedimientos

------------------------------- Procedimientos --------------------------------
-- Procedimiento creado por Enrique Guillermo Vílchez Lizano - C18477
go
create procedure [dbo].[actualizarCalificacionDeRegistro]
    (@creacionDeRegistro datetime2(7),
     @usuarioCreadorDeRegistro nvarchar(20),
     @nuevaCalificacion decimal(18,2)) 
as
begin
	declare @calificacionExistente decimal(18,2);

	-- Obtener la calificación existente para el registro
	select @calificacionExistente = calificacion
	from Registros
	where creacion = @creacionDeRegistro and usuarioCreador = @usuarioCreadorDeRegistro;

   -- Verificar si se encontró una calificación existente
   if @calificacionExistente is not null and @calificacionExistente > 0 begin
	-- Obtener la cantidad de usuarios que han calificado al registro
       declare @nuevoPromedio float, @cantidadCalificaciones int, @sumaCalificaciones int;

	   select @cantidadCalificaciones = count(calificacion), @sumaCalificaciones = sum(calificacion)
	   from Calificaciones
	   where creacionRegistro = @creacionDeRegistro and usuarioCreadorRegistro = @usuarioCreadorDeRegistro;

	   set @nuevoPromedio = (@sumaCalificaciones * 1.0) / (@cantidadCalificaciones * 1.0);

       -- Actualizar la calificación en la tabla
       update Registros
       set calificacion = @nuevoPromedio
       where creacion = @creacionDeRegistro and usuarioCreador = @usuarioCreadorDeRegistro;

   end
   else begin
       -- No se encontró una calificación existente, solo hay que establecer la nueva calificación.
       update Registros
       set calificacion = @nuevaCalificacion
       where creacion = @creacionDeRegistro and usuarioCreador = @usuarioCreadorDeRegistro;
   end
end;


-- Procedimiento creado por Emilia María Víquez Mora - C18625
go
create procedure [dbo].[actualizarModeracion]
	(@nombreUsuario nvarchar(max))
as 
begin
	declare @esModerador bit, @cantidadRegistros int, @calificacionUsuario float

	-- Obtener cantidad de registros realizados
	select @cantidadRegistros = count(r.usuarioCreador)
		from Registros as r
		where r.usuarioCreador = @nombreUsuario and
			  r.visible = 1;

	if (@cantidadRegistros > 0) begin
			-- Obtener calificación promedio del usuario basada en sus registros
			select @calificacionUsuario = u.calificacion
			from Usuario AS u
			where u.nombreDeUsuario = @nombreUsuario

			if (@cantidadRegistros >= 10 and @calificacionUsuario >= 4.9) begin
				-- Cumple con los requisitos para ser moderador
				set @esModerador = 1
			end
			else begin
				set @esModerador = 0 
			end
	end
	else begin
		set @esModerador = 0 
	end

	update Usuario
    set esModerador = @esModerador
    where nombreDeUsuario = @nombreUsuario;
end;


-- Procedimiento creado por Angie Sofía Solís Manzano - C17686
go
create procedure [dbo].[calificarRegistro]
	(@usuarioCalificador nvarchar(20),
	 @usuarioCreadorRegistro nvarchar(20),
	 @creacionRegistro datetime2(7),
	 @calificacion int)
as
begin
	declare @calificacionRegistrada int;

	-- Primero se debe averiguar si el usuario ya ha calificado ese registro
	select @calificacionRegistrada = calificacion
	from Calificaciones
	where usuarioCalificador = @usuarioCalificador
		and usuarioCreadorRegistro = @usuarioCreadorRegistro
		and creacionRegistro = @creacionRegistro;

	-- Si el usuario no ha calificado ese registro
	if @calificacionRegistrada is null begin
		-- Inserta la nueva calificacion
		insert into Calificaciones
			values (@usuarioCalificador, @usuarioCreadorRegistro, @creacionRegistro, @calificacion, null);
	end
	else begin
		-- Se revisa que el usuario esté actualizando su calificación
		if @calificacion != @calificacionRegistrada begin
			-- Actualiza la tupla a la nueva calificación
			update Calificaciones
			set calificacion = @calificacion
			where usuarioCalificador = @usuarioCalificador
				and usuarioCreadorRegistro = @usuarioCreadorRegistro
				and creacionRegistro = @creacionRegistro;
		end
	end
end;


-- Procedimiento creado por Angie Sofía Solís Manzano - C17686
go
create procedure [dbo].[actualizarCalificacionDeUsuario]
	(@nombreDeUsuario nvarchar(20),
	 @calificacion int)
as
begin
	declare @totalCalificaciones int, @sumaCalificaciones int, @promedioCalificaciones float;

	-- Se obtiene el total de calificaciones y la suma de dichas calificaciones
	select @totalCalificaciones = count(c.usuarioCreadorRegistro), @sumaCalificaciones = sum(c.calificacion)
	from Calificaciones as c join Registros as r on
		 c.creacionRegistro = r.creacion and
		 c.usuarioCreadorRegistro = r.usuarioCreador
	where c.usuarioCreadorRegistro = @nombreDeUsuario and
		  r.visible = 1;

	-- Se actualiza la calificación del usuario
	set @promedioCalificaciones = (1.0 * @sumaCalificaciones) / (1.0 * @totalCalificaciones);

	update Usuario
	set calificacion = @promedioCalificaciones
	where nombreDeUsuario = @nombreDeUsuario;
end;


-- Procedimiento creado por Kenneth Daniel Villalobos Solís - C18548
go
create procedure [dbo].[cambiarNombreUsuario]
	(@anteriorNombre nvarchar(20),
	 @nuevoNombre nvarchar(20))
as
begin
	declare @anteriorExiste nvarchar(20), @usuarioExiste nvarchar(20);

	-- Verificar si el usuario a modificar existe
	select @anteriorExiste = nombreDeUsuario
	from Usuario
	where nombreDeUsuario = @anteriorNombre;

	-- Verificar si el usuario nuevo no existe
	select @usuarioExiste = nombreDeUsuario
	from Usuario
	where nombreDeUsuario = @nuevoNombre;

	-- Si las anteriores dos condiciones se cumplen
	if @anteriorExiste is not null and @usuarioExiste is null begin
		-- Desactivar la verificación de restricciones para
		-- ejecutar las modificaciones en las tablas
		exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'

		update Usuario
		set nombreDeUsuario = @nuevoNombre
		where nombreDeUsuario = @anteriorNombre

		update Registros
		set usuarioCreador = @nuevoNombre
		where usuarioCreador = @anteriorNombre

		update Etiquetas
		set usuarioCreador = @nuevoNombre
		where usuarioCreador = @anteriorNombre

		update Fotografias
		set usuarioCreador = @nuevoNombre
		where usuarioCreador = @anteriorNombre

		update Calificaciones
		set usuarioCalificador = @nuevoNombre
		where usuarioCalificador = @anteriorNombre

		update Calificaciones
		set usuarioCreadorRegistro = @nuevoNombre
		where usuarioCreadorRegistro = @anteriorNombre

		update Reportes
		set usuarioCreadorReporte = @nuevoNombre
		where usuarioCreadorReporte = @anteriorNombre;

		update Reportes
		set usuarioCreadorRegistro = @nuevoNombre
		where usuarioCreadorRegistro = @anteriorNombre;

		-- Reactivar la verificación de restricciones en las tablas
		exec sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'
	end
end;



------------------------------- Funciones --------------------------------
-- Función creada por Enrique Guillermo Vílchez Lizano - C18477
go
create function [dbo].[esUnaExtensionValida]
	(@nombreArchivo nvarchar(200)) 
returns bit
as
begin
    declare @esValida bit = 0;
    declare @extension nvarchar(10);

    -- Extraer la extensión del archivo
    set @extension = lower(right(@nombreArchivo, charindex('.', reverse(@nombreArchivo)) - 1));

    -- Verificar si la extensión es válida
    if @extension IN ('jpg', 'jpeg', 'png')
        set @esValida = 1;

    return @esValida;
end;


-- Función creada por Luis David Solano Santamaría - C17634
go
create function [dbo].[imagenEnRegistro]
	(@nombreFotografia nvarchar(200),
	 @fotografia varbinary(max),
	 @creacion datetime2(7))
returns bit
as
begin
	-- Declarar variables requeridas
	declare @enRegistro as bit = 0;
	declare @nombreInsertada as nvarchar(200);
	declare @extensionParametro as char(3);
	declare @extensionInsertada as char(3);

	-- Revisar si la imagen ya existe en el registro
	select @nombreInsertada = f.nombreFotografia
	from Fotografias as f
	where f.fotografia = @fotografia AND f.creacion = @creacion;

	-- Revisar si encontró un resultado que coincida con el binario y el registro
	if @nombreFotografia is not null begin
		-- Obtener la extensión de ambos archivos
		select @extensionInsertada = right(lower(@nombreFotografia), 3);
		select @extensionParametro = right(lower(@nombreInsertada), 3);

		if @extensionInsertada = @extensionParametro begin
			set @enRegistro = 1;
		end
	end

	return @enRegistro;
end;



------------------------------- Triggers --------------------------------
-- Trigger realizado por todos los integrantes del equipo
go
create trigger verificarExtensionImagen
on Fotografias
instead of insert
as
begin
    -- Declarar variables
    declare @nombreFotografia as nvarchar(200),
			@creacion as datetime2(7),
			@usuarioCreador as nvarchar(20),
			@fotografia as varbinary(max);

    -- Declarar cursor
    declare cursorImagen cursor for 
        select *
        from inserted;

    -- Abrir cursor
    open cursorImagen;

    -- Obtener el primer elemento
    fetch next from cursorImagen into @nombreFotografia, @creacion, @usuarioCreador, @fotografia;

    while @@FETCH_STATUS = 0 begin
		-- Si la extensión es válida y la imagen no se encuentra en este registro
        if dbo.esUnaExtensionValida(@nombreFotografia) = 1 and
			dbo.imagenEnRegistro(@nombreFotografia, @fotografia, @creacion) = 0 begin
            -- Insertar registro válido
            insert into Fotografias
            values(@nombreFotografia, @creacion, @usuarioCreador, @fotografia);
        end

        -- Obtener el siguiente elemento
        fetch next from cursorImagen into @nombreFotografia, @creacion, @usuarioCreador, @fotografia;
    end

    -- Cerrar cursor
    close cursorImagen;

    -- Liberar la memoria del cursor
    deallocate cursorImagen;
end;

-- Trigger realizado por Luis David Solano Santamaría - C17634
go
create trigger reemplazarReporte
on Reportes
instead of insert
as
begin
	-- Declarar las variables requeridas del insert
	declare @creadorReporte as nvarchar(20),
			@creadorRegistro as nvarchar(20),
			@creacionRegisto as datetime2(7),
			@comentario as nvarchar(256),
			@creacionReporte as datetime2(7);
	-- Seleccionar los datos insertados
	select @creadorReporte = usuarioCreadorReporte,
		   @creadorRegistro = usuarioCreadorRegistro,
		   @creacionRegisto = creacionRegistro,
		   @comentario = comentario,
		   @creacionReporte = creacion
	from inserted;
	-- Revisar si un usuario ha realizado un reporte de este registro previamente
	if exists (select * from Reportes
			   where usuarioCreadorReporte = @creadorReporte and
			   usuarioCreadorRegistro = @creadorRegistro and
			   creacionRegistro = @creacionRegisto) begin
		delete from Reportes
		where usuarioCreadorReporte = @creadorReporte and
		usuarioCreadorRegistro = @creadorRegistro and
		creacionRegistro = @creacionRegisto;
	end
	-- Insertar el nuevo reporte
	insert into Reportes
	values(@creadorReporte, @creadorRegistro, @creacionRegisto, @comentario, @creacionReporte, 0, null);
end;