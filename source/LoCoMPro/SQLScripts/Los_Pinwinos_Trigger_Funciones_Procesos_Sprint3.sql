use Equipo2

-- Funciones y procedimientos

------------------------------- Procedimientos --------------------------------

-- Procedimiento creado por Luis David Solano Santamar�a - C17634
go
create procedure [dbo].[aplicarClusters]
	(@nombreNuevo nvarchar(256),
	 @nombreViejo nvarchar(256),
	 @categoriaNueva nvarchar(256))
as
begin
	begin try
		-- Se crea la transacci�n serializable
		set transaction isolation level serializable;
		begin transaction aplicarClusters;
		-- Desactivar la verificaci�n de las restricciones
		exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';

		-- Actualizar el nombre del producto y su categor�a
		delete Productos
		where nombre = @nombreViejo;

		-- Actualizar todos los registros del producto a la nueva informaci�n
		update Registros
		set productoAsociado = @nombreNuevo
		where productoAsociado = @nombreViejo;

		-- Actualizar todos los favoritos del producto a la nueva informaci�n
		update Favoritos
		set nombreProducto = @nombreNuevo
		where nombreProducto = @nombreViejo;

		-- Reactivar la verificaci�n de restricciones en las tablas
		exec sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';

		commit transaction aplicarClusters;
	end try

	begin catch
		rollback transaction aplicarClusters;
		throw;
	end catch;
end;

-- Procedimiento creado por Enrique Guillermo V�lchez Lizano - C18477
-- Modificado por Angie Sof�a Sol�s Manzano - C17686
GO
create procedure [dbo].[actualizarCalificacionDeRegistro]
    (@creacionDeRegistro datetime2(7),
     @usuarioCreadorDeRegistro nvarchar(20),
     @nuevaCalificacion decimal(18,2)) 
as
begin
	begin try
		
		-- Establecer el nivel de aislamiento
		set transaction isolation level serializable;

		-- Crear una transacci�n
		begin transaction;

		declare @calificacionExistente decimal(18,2), @cantidadCalificaciones int, @sumaCalificaciones int, @nuevoPromedio float;

		-- Obtener la calificaci�n existente para el registro
		select @calificacionExistente = calificacion
		from Registros
		where creacion = @creacionDeRegistro and usuarioCreador = @usuarioCreadorDeRegistro;

	   -- Verificar si se encontr� una calificaci�n existente
	   if @calificacionExistente is not null and @calificacionExistente > 0 begin
		-- Obtener la cantidad de usuarios que han calificado al registro
		   select @cantidadCalificaciones = count(calificacion), @sumaCalificaciones = sum(calificacion)
		   from Calificaciones
		   where creacionRegistro = @creacionDeRegistro and usuarioCreadorRegistro = @usuarioCreadorDeRegistro;

		   set @nuevoPromedio = (@sumaCalificaciones * 1.0) / (@cantidadCalificaciones * 1.0);

		   -- Actualizar la calificaci�n en la tabla
		   update Registros
		   set calificacion = @nuevoPromedio
		   where creacion = @creacionDeRegistro and usuarioCreador = @usuarioCreadorDeRegistro;
	   end
	   else begin
		   -- No se encontr� una calificaci�n existente, solo hay que establecer la nueva calificaci�n.
		   update Registros
		   set calificacion = @nuevaCalificacion
		   where creacion = @creacionDeRegistro and usuarioCreador = @usuarioCreadorDeRegistro;

		   set @cantidadCalificaciones = 1;
		   set @nuevoPromedio = @nuevaCalificacion * 1.0;
	   end

	   select @cantidadCalificaciones, @nuevoPromedio;

	   -- Terminar la transacci�n
	   commit;

	end try
	-- En caso de haber un error
	begin catch
		-- Deshacer la transacci�n
		rollback;

	end�catch
end;

-- Procedimiento creado por Emilia Mar�a V�quez Mora - C18625
go
create procedure [dbo].[actualizarModeracion]
	(@nombreUsuario nvarchar(max))
as 
begin
	declare @esModerador bit, @cantidadRegistros int, @calificacionUsuario float

	BEGIN TRY
        BEGIN TRANSACTION tActualizarModeracion;
			SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

			-- Obtener cantidad de registros realizados
			select @cantidadRegistros = count(r.usuarioCreador)
				from Registros as r
				where r.usuarioCreador = @nombreUsuario and
						r.visible = 1;

			if (@cantidadRegistros >= 10) begin
					-- Obtener calificaci�n promedio del usuario basada en sus registros
					select @calificacionUsuario = u.calificacion
					from Usuario AS u
					where u.nombreDeUsuario = @nombreUsuario

					if (@calificacionUsuario >= 4.9) begin
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
		COMMIT TRANSACTION tActualizarModeracion;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION tActualizarModeracion;
        THROW;
    END CATCH;
end;


-- Procedimiento creado por Angie Sof�a Sol�s Manzano - C17686
go
create procedure [dbo].[calificarRegistro]
	(@usuarioCalificador nvarchar(20),
	 @usuarioCreadorRegistro nvarchar(20),
	 @creacionRegistro datetime2(7),
	 @calificacion int)
as
begin
	begin try
		-- Se crea la transacci�n serializable
		set transaction isolation level serializable;
		begin transaction transaccionCalificarRegistro;

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
			-- Se revisa que el usuario est� actualizando su calificaci�n
			if @calificacion != @calificacionRegistrada begin
				-- Actualiza la tupla a la nueva calificaci�n
				update Calificaciones
				set calificacion = @calificacion
				where usuarioCalificador = @usuarioCalificador
					and usuarioCreadorRegistro = @usuarioCreadorRegistro
					and creacionRegistro = @creacionRegistro;
			end
		end

		-- Se cierra la transacci�n
		commit transaction transaccionCalificarRegistro;
	end try
	begin catch
		rollback transaction transaccionCalificarRegistro;
		throw;
	end catch;
end;

-- Procedimiento creado por Angie Sof�a Sol�s Manzano - C17686
go
create procedure [dbo].[actualizarCalificacionDeUsuario]
	(@nombreDeUsuario nvarchar(20))
as
begin
	begin try
		-- Se crea la transacci�n serializable
		set transaction isolation level serializable;
		begin transaction transaccionCalificacionUsuario;

		declare @totalCalificaciones int, @sumaCalificaciones int, @promedioCalificaciones float;

		-- Se obtiene el total de calificaciones y la suma de dichas calificaciones
		select @totalCalificaciones = count(c.usuarioCreadorRegistro), @sumaCalificaciones = sum(c.calificacion)
		from Calificaciones as c join Registros as r on
			 c.creacionRegistro = r.creacion and
			 c.usuarioCreadorRegistro = r.usuarioCreador
		where c.usuarioCreadorRegistro = @nombreDeUsuario and
			  r.visible = 1;

		-- Se actualiza la calificaci�n del usuario
		set @promedioCalificaciones = (1.0 * @sumaCalificaciones) / (1.0 * @totalCalificaciones);

		if @promedioCalificaciones is null
			set @promedioCalificaciones = 0

		update Usuario
		set calificacion = @promedioCalificaciones
		where nombreDeUsuario = @nombreDeUsuario;

		-- Se cierra la transacci�n
		commit transaction transaccionCalificacionUsuario;
	end try
	begin catch
		rollback transaction transaccionCalificacionUsuario;
		throw;
	end catch
end;

-- Procedimiento creado por Kenneth Daniel Villalobos Sol�s - C18548
go
create procedure [dbo].[cambiarNombreUsuario]
	(@anteriorNombre nvarchar(20),
	 @nuevoNombre nvarchar(20))
as
begin
	declare @anteriorExiste nvarchar(20), @usuarioExiste nvarchar(20);

	begin try
		-- Se crea la transacci�n serializable
		set transaction isolation level serializable;
		begin transaction;

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
			-- Desactivar la verificaci�n de restricciones para
			-- ejecutar las modificaciones en las tablas
			alter table Registros nocheck constraint FK_Registros_Usuario_usuarioCreador;
			alter table Etiquetas nocheck constraint FK_Etiquetas_Registros_creacion_usuarioCreador;
			alter table Fotografias nocheck constraint FK_Fotografias_Registros_creacion_usuarioCreador;
			alter table Calificaciones nocheck constraint FK_Calificaciones_Registros_creacionRegistro_usuarioCreadorRegistro;
			alter table Calificaciones nocheck constraint FK_Calificaciones_Usuario_usuarioCalificador;
			alter table Reportes nocheck constraint FK_Reportes_Registros_creacionRegistro_usuarioCreadorRegistro;
			alter table Reportes nocheck constraint FK_Reportes_Usuario_usuarioCreadorReporte;
			alter table Favoritos nocheck constraint FK_Favoritos_Usuario_nombreUsuario;

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
			set usuarioCreadorRegistro = @nuevoNombre
			where usuarioCreadorRegistro = @anteriorNombre

			update Calificaciones
			set usuarioCalificador = @nuevoNombre
			where usuarioCalificador = @anteriorNombre

			update Reportes
			set usuarioCreadorRegistro = @nuevoNombre
			where usuarioCreadorRegistro = @anteriorNombre;

			update Reportes
			set usuarioCreadorReporte = @nuevoNombre
			where usuarioCreadorReporte = @anteriorNombre;

			update Favoritos
			set nombreUsuario = @nuevoNombre
			where nombreUsuario = @anteriorNombre;

			-- Reactivar la verificaci�n de restricciones en las tablas
			alter table Registros with check check constraint FK_Registros_Usuario_usuarioCreador;
			alter table Etiquetas with check check constraint FK_Etiquetas_Registros_creacion_usuarioCreador;
			alter table Fotografias with check check constraint FK_Fotografias_Registros_creacion_usuarioCreador;
			alter table Calificaciones with check check constraint FK_Calificaciones_Registros_creacionRegistro_usuarioCreadorRegistro;
			alter table Calificaciones with check check constraint FK_Calificaciones_Usuario_usuarioCalificador;
			alter table Reportes with check check constraint FK_Reportes_Registros_creacionRegistro_usuarioCreadorRegistro;
			alter table Reportes with check check constraint FK_Reportes_Usuario_usuarioCreadorReporte;
			alter table Favoritos with check check constraint FK_Favoritos_Usuario_nombreUsuario;
		end

		-- Terminar la transacci�n
		commit;

	end try
	begin catch
		rollback;

		-- Reactivar la verificaci�n de restricciones en las tablas
		alter table Registros with check check constraint FK_Registros_Usuario_usuarioCreador;
		alter table Etiquetas with check check constraint FK_Etiquetas_Registros_creacion_usuarioCreador;
		alter table Fotografias with check check constraint FK_Fotografias_Registros_creacion_usuarioCreador;
		alter table Calificaciones with check check constraint FK_Calificaciones_Registros_creacionRegistro_usuarioCreadorRegistro;
		alter table Calificaciones with check check constraint FK_Calificaciones_Usuario_usuarioCalificador;
		alter table Reportes with check check constraint FK_Reportes_Registros_creacionRegistro_usuarioCreadorRegistro;
		alter table Reportes with check check constraint FK_Reportes_Usuario_usuarioCreadorReporte;
		alter table Favoritos with check check constraint FK_Favoritos_Usuario_nombreUsuario;
	end catch;
end;

-- Procedimiento creado por Kenneth Daniel Villalobos Sol�s - C18548
go
create procedure [dbo].[ocultarRegistrosObsoletos]
    (@producto nvarchar(256),
	 @tienda nvarchar(256),
	 @distrito nvarchar(30),
	 @canton nvarchar(20),
	 @provincia nvarchar(10),
	 @fechaCorte datetime2(7))
as
begin
	-- Declarar variables
	declare @creacion as datetime2(7) = null;
	declare @usuario as nvarchar(20) = null;

	-- Try para la transacci�n
	begin try
		-- Establecer el nivel de aislamiento
		set transaction isolation level serializable;

		-- Crear una transacci�n
		begin transaction;

		-- Crear un cursor para pasar por todos los
		-- registros que se deben ocultar
		declare cursorRegistro cursor for 
		select creacion, usuarioCreador
		from Registros
		where creacion <= @fechaCorte and
			  visible = 1 and
			  productoAsociado = @producto and
			  nombreTienda = @tienda and
			  nombreDistrito = @distrito and
			  nombreCanton = @canton and
			  nombreProvincia = @provincia;

		-- Abrir cursor y obtener los datos en las variables
		open cursorRegistro;
		fetch next from cursorRegistro into  @creacion, @usuario
	 
		-- Ciclo while mientras haya tuplas que cumplan
		while @@FETCH_STATUS = 0 begin
			-- Ocultar el registro
			update Registros
			set visible = 0
			where creacion = @creacion;

			-- Actualizar la calificacion y moderaci�n del usuario
			-- creador del registro
			exec actualizarCalificacionDeUsuario @usuario;
			exec actualizarModeracion @usuario;

			-- Obtener los proximos datos
			fetch next from cursorRegistro into  @creacion, @usuario
		end
	
		-- Cerrar y dealocar cursor
		close cursorRegistro;
		deallocate cursorRegistro;

	   -- Terminar la transacci�n
	   commit;

	end try
	-- En caso de haber un error
	begin catch
		-- Deshacer la transacci�n
		rollback;
	end catch
end;



------------------------------- Funciones --------------------------------
-- Funci�n creada por Enrique Guillermo V�lchez Lizano - C18477
go
create function [dbo].[esUnaExtensionValida]
	(@nombreArchivo nvarchar(200)) 
returns bit
as
begin
    declare @esValida bit = 0;
    declare @extension nvarchar(10);

    -- Extraer la extensi�n del archivo
    set @extension = lower(right(@nombreArchivo, charindex('.', reverse(@nombreArchivo)) - 1));

    -- Verificar si la extensi�n es v�lida
    if @extension IN ('jpg', 'jpeg', 'png')
        set @esValida = 1;

    return @esValida;
end;


-- Funci�n creada por Luis David Solano Santamar�a - C17634
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

	-- Revisar si encontr� un resultado que coincida con el binario y el registro
	if @nombreFotografia is not null begin
		-- Obtener la extensi�n de ambos archivos
		select @extensionInsertada = right(lower(@nombreFotografia), 3);
		select @extensionParametro = right(lower(@nombreInsertada), 3);

		if @extensionInsertada = @extensionParametro begin
			set @enRegistro = 1;
		end
	end

	return @enRegistro;
end;


-- Funci�n creada por Kenneth Daniel Villalobos Sol�s - C18548
go
create function [dbo].[encontrarFechaCorte]
	(@producto nvarchar(256),
	 @tienda nvarchar(256),
	 @distrito nvarchar(30),
	 @canton nvarchar(20),
	 @provincia nvarchar(10)) 
returns datetime2(7)
as
begin
	-- Declara las variables necesarias
	declare @delta int = 0, @numRegistros int = 0, @offset int;
	declare @fechaReciente datetime2(7) = null, @fechaDelta datetime2(7) = null, @fechaCorte datetime2(7) = null;

	-- Guarda la fecha m�s reciente y el n�mero de registros que existe
	select @fechaReciente = max(creacion), @numRegistros = count(*)
	from Registros
	where productoAsociado = @producto and
		  nombretienda = @tienda and
		  nombreDistrito = @distrito and
		  nombreCanton = @canton and
		  nombreProvincia = @provincia and
		  visible = 1;

	-- Calcula la cantidad de filas a saltar
	set @offset = cast(0.8 * @numRegistros as int)-1;
	if @offset < 0 begin
		set @offset = 0;
	end

	-- Guarda la fecha en la posici�n del 80%
	select @fechaDelta = creacion
	from Registros
	where productoAsociado = @producto and
		  nombretienda = @tienda and
		  nombreDistrito = @distrito and
		  nombreCanton = @canton and
		  nombreProvincia = @provincia and
		  visible = 1
	order by creacion desc
	offset @offset rows
	fetch first 1 row only;

	-- Si logr� encontrar la fecha en el 80% y la fecha inicial
    if @fechaDelta is not null and @fechaReciente is not null begin
		-- Calcula delta
		set @delta = datediff(second, @fechaDelta, @fechaReciente);
		-- Calcula la fecha de corte
		set @fechaCorte = dateadd(second, -@delta, @fechaDelta);
    end

	-- Si la fecha de corte es la m�s reciete (est� por marcar a todos como outliers)
	if @fechaCorte >= @fechaReciente begin
		-- Cambia la fecha de corte a la del siguiente registro m�s reciente
		select top 1 @fechaCorte = creacion
		from Registros
		where creacion < @fechaReciente and
			  productoAsociado = @producto and
			  nombretienda = @tienda and
			  nombreDistrito = @distrito and
			  nombreCanton = @canton and
			  nombreProvincia = @provincia and
			  visible = 1
		order by creacion desc;

		-- Si no hab�a ninguno, dejarla en nulo
		if @fechaCorte >= @fechaReciente begin
			set @fechaCorte = null
		end
	end
	
	-- Retorna la fecha de corte (si no hay es nula)
    return @fechaCorte;
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
		-- Si la extensi�n es v�lida y la imagen no se encuentra en este registro
        if dbo.esUnaExtensionValida(@nombreFotografia) = 1 and
			dbo.imagenEnRegistro(@nombreFotografia, @fotografia, @creacion) = 0 begin
            -- Insertar registro v�lido
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

-- Trigger realizado por Luis David Solano Santamar�a - C17634
go
create trigger reemplazarReporte
on Reportes
instead of insert
as
begin
	begin try
		set transaction isolation level serializable;
		begin transaction manejarReporte
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
		end;
		-- Insertar el nuevo reporte
		insert into Reportes
		values(@creadorReporte, @creadorRegistro, @creacionRegisto, @comentario, @creacionReporte, 0, null);
		commit transaction
	end try

	begin catch
		rollback transaction manejarReporte;
		throw;
	end catch;
end;

drop trigger reemplazarReporte