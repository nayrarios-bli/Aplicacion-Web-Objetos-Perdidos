Sistema de Gestión de Objetos Perdidos - UCB Cochabamba 

Este proyecto consiste en una API REST desarrollada en el backend para centralizar, registrar y controlar el flujo de objetos perdidos y reclamos dentro del campus de la Universidad Católica Boliviana "San Pablo" Sede Cochabamba. La solución permite a los usuarios administradores catalogar artículos encontrados y a los estudiantes o docentes iniciar procesos formales de reclamo de manera eficiente y transparente. 

Requisitos Previos 

Antes de proceder con la ejecución del proyecto, asegúrate de contar con las siguientes herramientas instaladas en tu entorno local: 

Entorno de Desarrollo: Visual Studio (versión 2022 recomendada) con la carga de trabajo Desarrollo de ASP.NET y web. 

ASP.NET Core web API de Framework: (la versión más reciente) 

Base de Datos: MySQL Server u SQL Server (configurado localmente). 

Pruebas de Software: Postman (aplicación de escritorio o extensión web). 

Instalación y Ejecución en Entorno Local 

Sigue estos pasos detallados para configurar y levantar la API en tu máquina de desarrollo: 

1. Clonar el repositorio 

Abre tu terminal preferida (Git Bash, CMD o PowerShell) y clona el proyecto con el siguiente comando: 

git clone https://github.com/nayrarios-bli/Aplicacion-Web-Objetos-Perdidos.git 
cd nombre-de-tu-repositorio 
  

2. Configurar la Base de Datos 

Accede a tu gestor de base de datos local. 

Crea la base de datos ejecutando el script .sql provisto en la carpeta del proyecto. 

Actualiza la cadena de conexión (Connection String) dentro del archivo appsettings.json en la raíz de la API con tus credenciales locales. 

3. Apertura y Ejecución del Proyecto 

Inicia la aplicación Visual Studio (ícono morado). 

Abre el archivo de solución del proyecto (con extensión .sln). 

Espera a que Visual Studio restaure automáticamente los paquetes NuGet requeridos. 

En la barra de herramientas superior, asegúrate de tener seleccionada la configuración de ejecución correspondiente al perfil de la API y presiona el botón "Play" (o la tecla F5). 

El servidor local se compilará e iniciará automáticamente en la siguiente dirección base segura: 

URL Local: https://localhost:44388 

- Enlaces Importantes y Documentación 

Consulta las especificaciones de diseño técnico, la planeación y las herramientas de interacción mediante los siguientes enlaces oficiales: 

 Documento de Diseño Técnico (Informe de Proyecto): Acceder al Documento de Trabajo en SharePoint (Nota: Requiere credenciales institucionales de la UCB). 

Interfaz de Pruebas de Endpoints (Postman): Las pruebas de caja negra, respuestas HTTP y validaciones de datos se ejecutan de manera externa consumiendo las rutas de la API desde las colecciones compartidas de Postman apuntando a https://localhost:44388 

Documentación Interactiva de la API: Al ejecutar el proyecto en modo de desarrollo, añade la ruta /api final de la URL local (https://localhost:44388/api) en tu navegador web para visualizar e interactuar directamente con la especificación OpenAPI de los endpoints. 

- Integrantes del Equipo (Autores) 

El diseño, modelado de datos y desarrollo de este sistema fue realizado por las siguientes estudiantes de la Universidad Católica Boliviana "San Pablo": 

Nelly Isabel Ayza Jimenez 

Yara Abigail Cadima Ortuño 

Nayra Gabriela Rios Castro 
