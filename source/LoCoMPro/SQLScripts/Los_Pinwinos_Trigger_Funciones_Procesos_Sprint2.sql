use Equipo2

-- Funciones y procedimientos

-- Función creada por Enrique Guillermo Vílchez Lizano C18477
GO
CREATE FUNCTION dbo.esUnaExtensionValida(@nombreArchivo nvarchar(200)) 
RETURNS bit
AS
BEGIN
    DECLARE @esValida AS bit = 0;
    DECLARE @extension AS nvarchar(10);

    -- Extraer la extensión del archivo
    SET @extension = LOWER(RIGHT(@nombreArchivo, CHARINDEX('.', REVERSE(@nombreArchivo)) - 1));

    -- Verificar si la extensión es válida
    IF @extension IN ('jpg', 'jpeg', 'png')
        SET @esValida = 1;

    RETURN @esValida;
END;

-- Procedimiento creado por Enrique Guillermo Vílchez Lizano C18477
GO
CREATE PROCEDURE dbo.actualizarCalificacionDeRegistro
    (@creacionDeRegistro datetime2(7),
     @usuarioCreadorDeRegistro nvarchar(20),
     @nuevaCalificacion decimal(18,2)) 
AS
BEGIN
   DECLARE @calificacionExistente decimal(18,2);

   -- Obtener la calificación existente para el registro
   SELECT @calificacionExistente = calificacion
   FROM Registros
   WHERE creacion = @creacionDeRegistro AND usuarioCreador = @usuarioCreadorDeRegistro;

   -- Verificar si se encontró una calificación existente
   IF @calificacionExistente IS NOT NULL AND @calificacionExistente > 0
   BEGIN
       -- Calcular el nuevo promedio de calificación
       DECLARE @nuevoPromedio decimal(18,2);
       SET @nuevoPromedio = ((@calificacionExistente + @nuevaCalificacion) / 2);

       -- Actualizar la calificación en la tabla
       UPDATE Registros
       SET calificacion = @nuevoPromedio
       WHERE creacion = @creacionDeRegistro AND usuarioCreador = @usuarioCreadorDeRegistro;
       
   END
   ELSE
   BEGIN
       -- No se encontró una calificación existente, solo hay que establecer la nueva calificación.
       UPDATE Registros
       SET calificacion = @nuevaCalificacion
       WHERE creacion = @creacionDeRegistro AND usuarioCreador = @usuarioCreadorDeRegistro;
       
   END
END;

-- Procedimiento creado por Emilia Víquez Mora C18625
go
create procedure actualizarModeracion
@nombreUsuario nvarchar(max)
as 
begin
	declare @esModerador bit, @cantidadRegistros int, @calificacionUsuario float

	-- Obtener cantidad de registros realizados
	select @cantidadRegistros = count(r.usuarioCreador)
		from Registros as r
		where r.usuarioCreador = @nombreUsuario
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
end

-- Función creada por Luis David Solano Santamaría C17634
GO
CREATE FUNCTION dbo.imagenEnRegistro
	(@nombreFotografia nvarchar(200),
	@fotografia varbinary(max),
	@creacion datetime2(7))
RETURNS bit
AS
BEGIN
	-- Declarar variables requeridas
	DECLARE @enRegistro AS bit = 0;
	DECLARE @nombreInsertada AS nvarchar(200);
	DECLARE @extensionParametro AS char(3);
	DECLARE @extensionInsertada AS char(3);
	-- Revisar si la imagen ya existe en el registro
	SELECT @nombreInsertada = f.nombreFotografia
	FROM FOTOGRAFIAS AS f
	WHERE f.fotografia = @fotografia AND f.creacion = @creacion;
	-- Revisar si encontró un resultado que coincida con el binario y el registro
	IF @nombreFotografia IS NOT NULL BEGIN
		-- Obtener la extensión de ambos archivos
		SELECT @extensionInsertada = RIGHT(LOWER(@nombreFotografia), 3);
		SELECT @extensionParametro = RIGHT(LOWER(@nombreInsertada), 3);
		IF @extensionInsertada = @extensionParametro BEGIN
			SET @enRegistro = 1;
		END
	END
	return @enRegistro;
END;

-- Trigger realizado por todos los integrantes del equipo
GO
CREATE TRIGGER verificarExtensionImagen
ON Fotografias
INSTEAD OF INSERT
AS
BEGIN
    -- Declarar variables
    DECLARE @nombreFotografia AS nvarchar(200)
            , @creacion AS datetime2(7)
            , @usuarioCreador AS nvarchar(20)
            , @fotografia AS varbinary(max);

    -- Declarar cursor
    DECLARE cursorImagen CURSOR FOR 
        SELECT *
        FROM inserted;

    -- Abrir cursor
    OPEN cursorImagen;

    -- Obtener el primer elemento
    FETCH NEXT FROM cursorImagen INTO @nombreFotografia, @creacion, @usuarioCreador, @fotografia;

    WHILE @@FETCH_STATUS = 0 BEGIN
		-- Si la extensión es válida y la imagen no se encuentra en este registro
        IF dbo.esUnaExtensionValida(@nombreFotografia) = 1 AND dbo.imagenEnRegistro(@nombreFotografia, @fotografia, @creacion) = 0 BEGIN
            -- Insertar registro válido
            INSERT INTO Fotografias
            VALUES(@nombreFotografia, @creacion, @usuarioCreador, @fotografia);
        END

        -- Obtener el siguiente elemento
        FETCH NEXT FROM cursorImagen INTO @nombreFotografia, @creacion, @usuarioCreador, @fotografia;
    END

    -- Cerrar cursor
    CLOSE cursorImagen;

    -- Liberar la memoria del cursor
    DEALLOCATE cursorImagen;
END

-- Procedimiento creado por Angie Sofía Solís Manzano C17686
GO
CREATE PROCEDURE dbo.calificarRegistro
	(@usuarioCalificador nvarchar(20),
	 @usuarioCreadorRegistro nvarchar(20),
	 @creacionRegistro datetime2(7),
	 @calificacion int)
AS
BEGIN
	DECLARE @calificacionRegistrada int;

	-- Primero se debe averiguar si el usuario ya ha calificado ese registro
	SELECT @calificacionRegistrada = calificacion
	FROM Calificaciones
	WHERE usuarioCalificador = @usuarioCalificador
		AND usuarioCreadorRegistro = @usuarioCreadorRegistro
		AND creacionRegistro = @creacionRegistro;

	-- Si el usuario no ha calificado ese registro
	IF @calificacionRegistrada IS NULL
	BEGIN
		-- Inserta la nueva calificacion
		INSERT INTO Calificaciones
			VALUES (@usuarioCalificador, @usuarioCreadorRegistro, @creacionRegistro, @calificacion, null);
	END
	ELSE
	BEGIN
		-- Se revisa que el usuario esté actualizando su calificación
		IF @calificacion != @calificacionRegistrada
		BEGIN
			-- Actualiza la tupla a la nueva calificación
			UPDATE Calificaciones
			SET calificacion = @calificacion
			WHERE usuarioCalificador = @usuarioCalificador
				AND usuarioCreadorRegistro = @usuarioCreadorRegistro
				AND creacionRegistro = @creacionRegistro;
		END
	END
END;


-- Procedimiento creado por Angie Sofía Solís Manzano C17686
GO
CREATE PROCEDURE dbo.actualizarCalificacionDeUsuario
	(@nombreDeUsuario nvarchar(20),
	 @calificacion int)
AS
BEGIN
	DECLARE @totalCalificaciones int, @sumaCalificaciones int, @promedioCalificaciones float;

	-- Se obtiene el total de calificaciones y la suma de dichas calificaciones
	SELECT  @totalCalificaciones = COUNT(usuarioCreadorRegistro), @sumaCalificaciones = SUM(calificacion)
	FROM Calificaciones
	WHERE usuarioCreadorRegistro = @nombreDeUsuario;

	-- Se actualiza la calificación del usuario
	SET @promedioCalificaciones = (1.0 * @sumaCalificaciones) /(1.0 * @totalCalificaciones);
	UPDATE Usuario
	SET calificacion = @promedioCalificaciones
	WHERE nombreDeUsuario = @nombreDeUsuario;
END;

-- Procedimiento para cambiar un nombre de usuario
-- Procedimiento creado por Kenneth Villalobos - C18548
go
create procedure cambiarNombreUsuario (
	-- Nombre de usuario de la cuenta a modificar
	@anteriorNombre nvarchar(20),
	-- Nuevo nombre de usuario que se desea tener
    @nuevoNombre nvarchar(20)
)
as
begin
	-- Declarar variables para verificar si los parametros son válidos
	declare @anteriorExiste nvarchar(20), @usuarioExiste nvarchar(20);

	-- Verificar si el usuario anterior existe
	select @anteriorExiste = nombreDeUsuario
	from Usuario
	where nombreDeUsuario = @anteriorNombre;

	-- Verificar si el nuevo usuario existe
	select @usuarioExiste = nombreDeUsuario
	from Usuario
	where nombreDeUsuario = @nuevoNombre;

	-- Si el usuario anterior existe y el nuevo usuario no existe
	if @anteriorExiste is not null and @usuarioExiste is null begin
		-- Desactivar temporalmente las restricciones de llaves para
		-- mayor facilidad al actualizar
		exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'

		-- Actualizar el nombre de usuario
		update Usuario
		set nombreDeUsuario = @nuevoNombre
		where nombreDeUsuario = @anteriorNombre

		-- Actualizar los registros
		update Registros
		set usuarioCreador = @nuevoNombre
		where usuarioCreador = @anteriorNombre

		-- Actualizar las etiquetas
		update Etiquetas
		set usuarioCreador = @nuevoNombre
		where usuarioCreador = @anteriorNombre

		-- Actualizar las fotografias
		update Fotografias
		set usuarioCreador = @nuevoNombre
		where usuarioCreador = @anteriorNombre

		-- Actualizar las calificaciones hechas
		-- por el usuario
		update Calificaciones
		set usuarioCalificador = @nuevoNombre
		where usuarioCalificador = @anteriorNombre

		-- Actualizar las calificaciones recibidas
		-- por el usuario
		update Calificaciones
		set usuarioCreadorRegistro = @nuevoNombre
		where usuarioCreadorRegistro = @anteriorNombre

		-- Actualizar los reportes hechos
		-- por el usuario
		update Reportes
		set usuarioCreadorReporte = @nuevoNombre
		where usuarioCreadorReporte = @anteriorNombre;

		-- Actualizar los reportes recibidos
		-- por el usuario
		update Reportes
		set usuarioCreadorRegistro = @nuevoNombre
		where usuarioCreadorRegistro = @anteriorNombre;

		-- Reactivar nuevamente las restricciones de llaves
		exec sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'
	end
end;