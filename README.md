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

La página debe permitir el ingreso de usuarios dando funcionalidades básicas de manejo de usuarios, como una opción de cambio de contraseña o borrado de cuenta. De igual manera, el sistema debe cumplir ciertas características para la manipulación de registros y filtrado y ordenamiento de productos. Para la manipulación de sus propios registros, los usuarios, podrán editar la información que contienen y eliminarlos en caso de que no deseen que se mantengan en el sistema. Para el manejo de productos, podrán ser ordenados o filtrados según ciertos atributos que cada producto posee.

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

+ Búsqueda avanzada:

+ Ordenamiento de resultados de búsqueda:

+ Filtrado de resultados de búsqueda:

+ Agregar un registro de un producto:

Se debe tomar en cuenta que no todas las funcionalidades requieren estar loggeado al sistema como un usuario, solo las relacionadas a agregar un registro de un producto.

#### **Registrarse como usuario al sistema**

Para registrarse como usuario al sistema debe ingresar a la página de `Iniciar sesión` por medio del botón en el *layout* de la aplicación.

Una vez encontrado en esa página, debe darle click al botón de `Registrase` e ingresar los datos solicitados para crear un usuario.

#### **Ingresar al sistema con un usuario válido**

Para ingresar al sistema como un usuario válido, debe haber pasado previamente por el proceso de registrarse como usuario al sistema. 

Posteriormente, debe ingresar los datos solicitados de su respectivo usuario.

#### **Búsqueda simple**

Para realizar una búsqueda simple, desde la página *home*, debe rellenar el nombre del producto deseado y presionar el botón de `Buscar`.

La aplicación no va a permitir realizar búsquedas con una cadena vacía en el buscador.

#### **Búsqueda avanzada**

Para realizar una búsqueda avanzada, desde la página *home*, debe presionar el botón de `Búsqueda avanzada` donde se encontrará con una página donde puede modificar diferentes parámetros para la búsqueda.

Se permite interacción con un mapa para delimitar la región del país que se desea, pero se puede modificar por medio de *comboboxes* la provincia y cantón del producto buscado.

#### **Ordenamiento de resultados de búsqueda**

Una vez encontrados los resultados, sin importar el tipo de búsqueda, se puede realizar un ordenamiento a los resultados (ascendiente o descendiente) al darle click a una de las siguientes columnas:

+ Precio

+ Provincia

+ Cantón

Posteriormente, se implementarán nuevos ordenamientos.

#### **Filtrado de resultados de búsqueda**

Una vez encontrados los resultados, sin importar el tipo de búsqueda, se puede realizar un filtro de los resultados según los siguientes parámetros:

+ Provincia

+ Cantón

+ Tienda

+ Marca

Al ser realizado por medio de *checkboxes*, se pueden aplicar varios filtros a la vez donde se pueden delimitar los resultados de la búsqueda posteriormente.

Estos filtros son aplicados a los resultados iniciales de la búsqueda y si se desean nuevos se deben realizar nuevas búsquedas.

#### **Agregar un registro de un producto**

Debido a la naturaleza de *crowdsourcing* del proyecto, los usuarios registrados en el sistema pueden agregar un registro de un producto. Para esto se deben seguir tres pasos clave:

1. Identificar la tienda respectiva.

2. Identificar el producto respectivo.

3. Agregar los datos del registro deseado.

## **Manual técnico de la aplicación**

### **Requerimientos de instalación**

Para instalar y ejecutar el proyecto se requieren las siguientes aplicaciones:

+ `Visual Studio Community (2022)`

+ `Microsoft SQL Server Management Studio`

Dentro de Visual Studio se requieren la siguiente extensión para desarrollo web:

+ `ASP.NET and web development`

De igual manera, dentro de los archivos del proyecto es requerido la carpeta `jquery` en el directorio `wwwroot/js` que incluye las bibliotecas `jQuery` y `jQuery UI`.

### **Preparación de la base de datos**

Para la preparación de la base de datos se debe tener configurado el *`connection string`* que establece la IP, puerto y características de la conexión con el servidor.

En caso de estar utilizando una IP de la ECCI, se debe utilizar el **VPN de la ECCI**. Puede consultar información sobre su uso e instalación [aquí](https://wiki.ecci.ucr.ac.cr/estudiantes/vpn).

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

+ [Código fuente de las pruebas unitarias](./test/LoCoMProTests/)