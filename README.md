# **Proyecto Integrador CI0128**

## **Información del curso**

### **Profesores**

+ Dra. Alexandra Martínez
+ Dr. Allan Berrocal Rojas

### **Descripción del curso**

El curso conforma un `Proyecto Integrador` de los cursos `Bases de Datos`  e `Ingeniería de Software`. De esta manera, durante su realización se da un enfoque en la aplicación e integración de los conocimientos obtenidos de ambos cursos por medio de un proyecto. En el desarrollo del proyecto se espera que seguir buenas prácticas de trabajo, para realizar una aplicación web con su correspondiente base de datos.

## **Información de equipo**

### **Nombre de equipo**

#### **Los Pinwinos**

<img src="https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExb3NwbHhwb3d5NDJtczg2YXJ1OW9xZzVzZDZhamJnbTBkOTh1aWU5cCZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9cw/6PgR6hoJrhlMwZBuiY/giphy.gif" width="300">

### **Integrantes**

+ [Luis David Solano Santamaría - C17634](https://github.com/GoninDS)
+ [Angie Sofía Solís Manzano - C17686](https://github.com/AngieS23)
+ [Enrique Guillermo Vílchez Lizano - C18477](https://github.com/EnriqueVilchezL)
+ [Kenneth Daniel Villalobos Solís  - C18548](https://github.com/kdaniel1652)
+ [Emilia María Víquez Mora - C18625](https://github.com/EmiliaViq)

## **Descripción del problema**

`LoCoMPro` es un sistema que consiste en la `Localización y Consulta del Mejor Producto`. En otras palabras, es un sistema de búsqueda donde sus usuarios pueden obtener información de artículos en venta en base a su localización. Para realizar las consultas, se utilizará una base de datos que tendrá productos conformados por medio de registros. Cada registro es creado por datos proveídos por los mismos usuarios, formando un sistema basado en el concepto de *crowdsourcing*.

La página debe permitir el ingreso de usuarios dando funcionalidades básicas de manejo de usuarios, como una opción de cambio de contraseña o borrado de cuenta. De igual manera, el sistema debe cumplir ciertas características para la manipulación de registros y filtrado y ordenamiento de productos. Para la manipulación de sus propios registros, los usuarios, podrán editar la información que contienen y eliminarlos en caso de que no deseen que se mantengan en el sistema. Para el manejo de productos, podrán ser ordenados o filtrados según ciertos atributos que cada producto posee. Además, la página cuenta con un rol de moderador, el cual tiene funciones superiores a las de un usuario regular, ya que puede manipular los reportes realizados en el sistema.

## **Estructura de carpetas**

El repositorio está conformado por tres directorios principales:

+ [*design*](./design/) donde se encuentran los archivos de diseño realizados en las diferentes etapas del proceso de desarrollo.

+ [*source*](./source/) donde se encuentran el código fuente de la página web y modelos de la base de datos.

+ [*test*](./test/) donde se encuentra el proyecto de pruebas, que contiene las diferentes clases encargadas de realizar pruebas unitarias del sistema.

## **Manual de usuario de la aplicación**

### **Información de acceso de la aplicación**

Al ser una aplicación web, debe acceder al dominio requerido donde se esté ejecutando para poder acceder a la página y sus funcionalidades.

### **Uso de las funcionalidades de la aplicación**

Las funcionalidades básicas implementadas, actualmente, en la aplicación consisten en:

+ Registrarse como usuario al sistema

+ Ingresar al sistema con un usuario válido

+ Búsqueda simple

+ Búsqueda avanzada

+ Ordenamiento de resultados de búsqueda

+ Filtrado de resultados de búsqueda

+ Agregar un registro de un producto junto con imágenes

+ Ver los registros asignados a un producto junto con las características de dicho producto

+ Agrupar los registros asignados a un producto según su día, semana, mes o año de creación para visualizar aspectos importantes como precio mínimo, promedio y máximo, junto con su calificación promedio.

+ Ver la información de un registro de forma detallada

+ Calificar registros de forma tal que esto actualice la calificación del usuario

+ Ver los aportes de un usuario

+ Ver los datos de un usuario registrado

+ Cambiar los datos del usuario (nombre de usuario y ubicación)

+ Reportar un registro

+ Asignar rol de moderador a los usuarios que cumplan con tener 10 o más registros y tengan una calificación promedio mayor o igual a 4,9

+ Ver reportes de un registro

+ Aceptar o rechazar reportes para los registros

Se debe tomar en cuenta que no todas las funcionalidades requieren estar loggeado al sistema como un usuario. Las funcionalidades que lo requieren son: ver los aportes realizados, agregar un registro de un producto, reportar un registro y calificar un registro. Además, solo los usuarios loggeados que son moderadores pueden visualizar, aceptar o rechazar los reportes realizados.


#### **Registrarse como usuario al sistema**

Para registrarse como usuario al sistema debe ingresar a la página de `Iniciar sesión` por medio del botón en el *layout* de la aplicación.

Una vez encontrado en esa página, debe darle click al botón de `Registrase` e ingresar los datos solicitados para crear un usuario. Recibirá un correo electrónico generado por la aplicación para poder validar su cuenta.


#### **Ingresar al sistema con un usuario válido**

Para ingresar al sistema como un usuario válido, debe haber pasado previamente por el proceso de registrarse como usuario al sistema y debe haber validado su cuenta con el botón de confirmación que se muestra en el correo electrónico. 

Posteriormente, debe ingresar los datos solicitados de su respectivo usuario.


#### **Búsqueda simple**

Para realizar una búsqueda simple, desde la página *home*, debe rellenar el nombre del producto deseado y presionar el botón de `Buscar`.

La aplicación no va a permitir realizar búsquedas con una cadena vacía en el buscador.


#### **Búsqueda avanzada**

Para realizar una búsqueda avanzada, desde la página *home*, debe presionar el botón de `Búsqueda avanzada` donde se encontrará con una página donde puede modificar diferentes parámetros para la búsqueda.

Se permite interacción con un mapa para delimitar la región del país que se desea, pero se puede modificar por medio de *comboboxes* la provincia y cantón del producto buscado.


#### **Ordenamiento de resultados de búsqueda**

Una vez encontrados los resultados, sin importar el tipo de búsqueda, se puede realizar un ordenamiento a los resultados (ascendiente o descendiente) al darle click a cualquiera de sus columnas.


#### **Filtrado de resultados de búsqueda**

Una vez encontrados los resultados, sin importar el tipo de búsqueda, se puede realizar un filtro de los resultados según los siguientes parámetros:

+ Provincia

+ Cantón

+ Tienda

+ Marca

+ Precio

+ Fecha

Al ser realizado por medio de *checkboxes*, se pueden aplicar varios filtros a la vez donde se pueden delimitar los resultados de la búsqueda posteriormente.

Estos filtros son aplicados a los resultados iniciales de la búsqueda y si se desean nuevos se deben realizar nuevas búsquedas.


#### **Agregar un registro de un producto**

Debido a la naturaleza de *crowdsourcing* del proyecto, los usuarios registrados en el sistema pueden agregar un registro de un producto. Para esto se deben seguir tres pasos clave:

1. Identificar la tienda respectiva.

2. Identificar el producto respectivo.

3. Agregar los datos del registro deseado.


#### **Ver los registros de un producto**

Estando en la página de resultados de la búsqueda (puede ser avanzada o regular), seleccionar alguno de los productos resultados de la búsqueda para acceder a los registros del mismo. Además, se puede navegar por las imágenes de todos los registros asociados desde el rectángulo mostrado en la parte superior de la página. Estas imágenes vienen acompañadas con flechas que permiten la fácil navegación por las imágenes mostradas. 


#### **Agrupar los registros de un producto**

Para poder observar el precio mínimo, promedio y máximo junto con la calificación promedio de los registros, se puede seleccionar alguna de las opciones de agrupamiento que se presentan en el lado izquierdo de la página. Es importante indicar que solo se permite un agrupamiento a la vez.


#### **Ver la información de un registro**

En la página de ver los registros de un producto, se puede seleccionar uno de los registros para desplegar la información del mismo junto con las imágenes asignadas a dicho registro. Cabe indicar que no se puede acceder a esta información si hay algún agrupamiento activo.


#### **Calificar los registros de un producto**

Una vez en la ventana de observar la información de un registro en particular, se tiene acceso a las estrellas de calificación. Estas despliegan la calificación actual asignada al registro y permiten que el usuario seleccione la estrella con la que desea calificar el registro y la almacena. Si se vuelve a ingresar a la página del mismo registro, la calificación mostrada corresponde al nuevo promedio del registro.


#### **Reportar un registro**

En la ventana de observar la información de un registro se muestra el botón de `Reportar`. Al seleccionarlo, se despliega el espacio para indicar el motivo del reporte y confirmar el envío del mismo. Esta funcionalidad solo existe para usuarios ingresados en el sistema.


#### **Ver la información de un usuario**

Una vez ingresado en el sistema, se muestra en las opciones en la parte superior de la ventana un botón de `Mi Perfil`, que permite desplegar la información del mismo y cambiar aspectos como el nombre de usuario, provincia, cantón y distrito.


#### **Ver los aportes de un usuario**

Una vez igresado en el sistema, tanto en la ventana de `Mi Perfil` como en el menú de la ventana principal, se muestra la opción de `Ver aportes`. En esta página se muestran los registros ingresados al sistema por dicho usuario.


#### **Obtener rol de moderador**

Para ser moderador, un usuario debe cumplir con las siguientes características:

1. Tener más de 10 registros en el sistema.

2. Tener una calificación mayor o igual a 4.9 según el promedio de calificaciones de sus registros.

Una vez cumplidas las características, el usuario moderador podrá ver la opción de `Ver reportes`, que le permite aceptar o rechazar los reportes del usuario. Es importante indicar que si el usuario que se convirtió en moderador se encuentra ingresado en el sistema en el momento en el que su estado cambia, no se le notificará hasta que no cierre su sesión y vuelva a ingresar al sistema.


#### **Ver los reportes agregados a los diferentes registros**

En la página principal, los usuarios con permisos de moderador pueden ver la opción de `Ver reportes`. En esta ventana, el usuario puede decidir si:

1. Aceptar un reporte: esto significa darle la razón a la persona que reportó y ocultar el registro para los demás usuarios.

2. Rechazar un reporte: esto significa indicar que el reporte es inválido y que no se quiere ocultar el registro.

3. Saltar un reporte: esto significa que el moderador no quiere tomar ninguna decisión sobre dicho reporte.


## **Manual técnico de la aplicación**

### **Requerimientos de instalación**

Para instalar y ejecutar el proyecto se requieren las siguientes aplicaciones:

+ `Visual Studio Community (2022)`

+ `Microsoft SQL Server Management Studio`

Dentro de Visual Studio se requieren la siguiente extensión para desarrollo web:

+ `ASP.NET and web development`

De igual manera, dentro de los archivos del proyecto es requerido la carpeta `jquery` en el directorio `wwwroot/js` que incluye las bibliotecas `jQuery` y `jQuery UI`.

En el caso de las pruebas funcionales, se necesita tener instalado el programa `Selenium` para Visual Studio, `Selenum Support` y un driver para el buscador `Chrome`. Esto se puede encontrar dentro de la opción `Manage NuGet Packages for Solution`.

### **Preparación de la base de datos**

#### **Para la ejecución del proyecto**
Para la preparación de la base de datos se debe tener configurado el *`connection string`* que establece la IP, puerto y características de la conexión con el servidor.

En caso de estar utilizando una IP de la ECCI, se debe utilizar el **VPN de la ECCI**. Puede consultar información sobre su uso e instalación [aquí](https://wiki.ecci.ucr.ac.cr/estudiantes/vpn).

`Observación`: si anteriormente ejecutó las pruebas funcionales, es necesario revertir los cambios realizados para que el connection string se mantenga como `LoCoMProContextRemote`.

#### **Para la ejecución de las pruebas funcionales**

##### **Prerequisitos**
Primeramente, debe hacerse un cambio en los archivos `Program.cs` y `ControladorComandosSQL.cs`. En el archivo `Program.cs`, debe cambiar la constante `connectionString` para que esta contenga el string: `LoCoMProContextTest`. En el archivo `ControladorComandosSQL.cs` se debe cambiar la constante `connectionString` en el constructor para que contenga el mismo string mencionado anteriormente. Una vez realizado esto, debe ejecutar el programa para crear la base de datos necesaria para realizar las pruebas. 

Una vez creada la base de datos, debe ejecutar los procedimientos, funciones y triggers en la nueva base de datos que se encuentra en la carpeta [SQLScripts](./source/LoCoMPro/SQLScripts).

Para ejecutar las pruebas funcionales se necesita que la aplicación esté corriendo en el `puerto 5150 en localhost`. Esto se puede lograr ejecutando la aplicación desde la terminal. Para esto, se debe abrir la carpeta del proyecto en donde se tiene la aplicación ya compilada (con las carpetas bin y obj generadas) y correr el comando:

    dotnet run

Una vez realizado esto, la aplicación se ejecutará desde el `puerto 5150`. Además, es necesario tener el navegador Google Chrome instalado, pues Selenium se ejecuta por este medio. En caso de que el puerto deseado se encuentre bloqueado, deberá averiguar cuál es el puerto que se tomó y cambiar los enlaces en las diferentes pruebas funcionales para que contengan el puerto actual. 

Se realizaron diversas pruebas funcionales con la herramienta de Selenium. Para el funcionamiento adecuado de las diferentes pruebas se requiere lo siguiente: (para ejecutarlas todas, deben cumplirse todas las condiciones mencionadas. Se indican de forma separada en caso de que solo se desee ejecutar alguna prueba de forma individual).

##### **Títulos de columnas en agrupamientos de la ventana de Ver Registros**
Para acceder a la ventana de Ver Registros se necesita estar visualizando un registro, en esta prueba, de forma aleatoria, se seleccionó ver los registros del producto `Camisa`, con las siguientes características:

- Categoría: Ropa
- Marca: Adidas
- Unidad: Cantidad
- Tienda: Maxi Pali
- Provincia: Heredia
- Canton: Heredia

En caso de que este producto no existiese junto con registros, las pruebas igual serían válidas, simplemente no se visualizarían registros asociados dentro de la ventana, pero las columnas igual cambiarían al seleccionar los agrupamientos, lo cual es lo que se desea probar con la prueba.

##### **Filtros de provincia**

Para ejecutar esta prueba se requiere que existan productos cuyos registros asociados tengan como provincia a Guanacaste. Esto puesto que se seleccionó esta provincia para realizar las pruebas. 

##### **Cantones de Agregar Tienda**

Esta prueba se encarga de verificar que los cantones que se muestran en el *combobox* de cantón correspoden únicamente a los cantones de la provincia de Limón. Para ejecutar esta prueba se requiere que exista la provincia de Limón y sus respectivos cantones.

##### **Cambiar vivienda de un usuario**

Para realizar esta prueba se debe contar con un usuario válido y autenticado en el sistema que cumpla con las siguientes características:
- Nombre de usuario = `Usuario1*`
- Contraseña = `Usuario1*`
- Provincia de vivienda != `Guanacaste`
- Cantón de vivienda !=  `Hojancha`
- Distrito de vivienda: `Huacas`

El nombre de usuario y contraseña serán utilizados como credenciales para ingresar al sistema con el fin de acceder a la página de Mi perfil, donde se probará la funcionalidad para cambiar la vivienda del usuario. Debido a esto y dado que el botón para guardar los cambios se vuelve disponible hasta que se seleccionen datos distintos a los que presentaba la cuenta originalmente, es imperativo que la vivienda del usuario no estuviese en Guancaste, Hojancha, Huacas, pues estos son los valores a los que la prueba intentará cambiar la vivienda. Cualquier otra vivienda será válida.

##### **Revisar ordenamiento de flechas**

Para realizar esta prueba se requiere la conexión con la base de datos para realizar una búsqueda, pero no es dependiente de los resultados ya que solo se revisa la presencia de las flechas.

### **Manual de instalación o ejecución del sistema**

Para la ejecución, por simpleza, se puede ejecutar desde `Visual Studio`.

Para esto debe abrir el archivo `LoCoMPro.sln` que al abrirlo configura las carpetas del archivo.

De esta manera puede ejecutar la ejecución desde su *browser* deseado y escoger las siguientes opciones:

+ `HTTP`
+ `HTTPS`
+ `IIS EXPRESS`
+ `WSL`

### **Manual de ejecución de los casos de prueba**

Para la ejecución de los casos de prueba del proyecto, puede abrir el archivo `LoCoMPro.sln` con `Visual Studio`.

De esta manera se le va a abrir el proyecto principal y el de proyectos de prueba.

Una vez abierto, desde la ventana `Test` se puede escoger `Test Explorer` donde se puede manejar la ejecución y resultados de las pruebas.

### **Reporte del Sprint 0**

Dentro del Sprint 0 se entregaron dos avances, cada uno con su respectivo diseño del *mockup* de la aplicación y diseño de la base de datos.

#### **Primer avance**

+ [Diseño del *mockup*](./design/sprint0/mockups/avance1/Mockup_AvanceSemana4.pdf)
+ [Diseño de la base de datos](./design/sprint0/database/avance1/Diseño%20conceptual%20de%20la%20base%20de%20datos.pdf)

#### **Segundo avance**

+ [Diseño del *mockup*](./design/sprint0/mockups/avance2/Mockup_AvanceSemana5.pdf)
+ [Diseño de la base de datos](./design/sprint0/database/avance2/diseño_base_avance2_los_pinwinos.pdf)

### **Reporte del Sprint 1**

Dentro del Sprint 1 se entragaron tres avances, con sus respectivos archivos de diseño y progreso. Posteriormente, puede encontrar el acceso a ciertos de estos archivos según su respectivo avance:

#### **Primer avance**

+ [Diseño del *mockup*](./design/sprint1/mockups/avance1/Mockup_Sprint1_Avance1.pdf)

#### **Segundo avance**

+ [Diagramas UML de casos de uso, actividad y clases](https://app.diagrams.net/#G1lMwHHsaEFior3vl9VeQtuhsC4UZvpcjy)

+ [Diseño conceptual de la base de datos](https://app.diagrams.net/#G1qMmeaKcCo1zlALDRB_4TRdMQL70PeTvw)

+ [Diseño lógico de la base datos](https://app.diagrams.net/#G1dnGUz38Jw0BdR7JvzJaeD736ZpiCPNJy) 

#### **Tercer avance**

+ [Código fuente del proyecto](./source/LoCoMPro)

+ [Código fuente de las pruebas unitarias](./test/unit_tests/)


### **Reporte del Sprint 2**

Dentro del Sprint 2 se entragaron tres avances, con sus respectivos archivos de diseño y progreso. Posteriormente, puede encontrar el acceso a ciertos de estos archivos según su respectivo avance:

#### **Primer avance**

+ [Diseño del *mockup*](./design/sprint2/mockups/Mockup_Sprint2_Avance1.pdf)


#### **Segundo avance**

+ [Diagramas UML de casos de uso, actividad y clases](https://app.diagrams.net/#G1qJkUy_LJMj5MtduuwwVkmtTsn6y5Bip9)


#### **Tercer avance**

+ [Código fuente del proyecto](./source/LoCoMPro)

+ [Código fuente de las pruebas unitarias](./test/unit_tests/)

+ [Código fuente de las pruebas funcionales](./test/functional_tests/)