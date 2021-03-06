# Microservicio para la gestión de usuarios

La resolución de la prueba práctica se encuentra dividida en 4 proyectos:
- **[bitsa-api-gateway](https://github.com/njarma/bitsa/tree/master/bitsa-api-gateway):** El punto de entrada a los métodos del proyecto de usuarios y de autenticación.
- **[bitsa-api-identity](https://github.com/njarma/bitsa/tree/master/bitsa-api-identity):** Autentica el usuario por medio de email y contraseña, y posteriormente genera un token jwt
- **[bitsa-api-users](https://github.com/njarma/bitsa/tree/master/bitsa-api-users):** Api rest con los requerimientos de la consigna.
- **[bitsa-base](https://github.com/njarma/bitsa/tree/master/bitsa-base):** Emplea Entity Framework Core (Code First) para la creación de los modelos y la base de datos en mysql.

En la raíz del repositorio se incluye un script con la estructura y los datos de prueba utilizados en el proceso de desarrollo, su nombre es **script-database.sql**

Para dar seguimiento al presente manual se recomienda utilizar la herramienta **Postman:**.

### Tareas previas
1. Crear la base de datos de prueba con el script **script-database.sql**.
2. Ejecutar los 4 proyectos descriptos al inicio.

### Interación con el proyecto api gateway para autenticación
1. Crear una consulta **POST en postman** con la siguiente url: https://localhost:44354/api/Auth/Login
2. Ir a la **sección BODY**, seleccionar el **radio button RAW** y pegar en la caja de texto el siguiente json. Aclaración importante, la contraseña de todos los usuarios es la misma.
```json
{
	"Email": "njarma@gmail.com",
	"Password": "12345678"
}
```
3. Presionar el botón **SEND**
4. Si el email y password son válidos, postman devolverá los datos del usuario autenticado. Es importante reservar la propiedad **access_token** para los próximos pasos.
```json
{
    "revoked": 0,
    "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxIiwiQWRtaW5pc3RyYXRvciI6IjEiLCJzdWIiOiJuamFybWFAZ21haWwuY29tIiwianRpIjoiMzUzZmZmMTYtNjUwNy00YzgwLWFmMmQtMTIwNDQzZWNjNDcyIiwiaWF0IjoiMTcvNi8yMDIwIDIwOjUxOjQ1IiwibmJmIjoxNTkyNDI3MTA1LCJleHAiOjE1OTI2MDcxMDUsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6IkJpdHNhIn0.w60ZH7I_OQTJEe8fPuoQOUy1t--bvREy4oSUc5GccE8",
    "expires_in": 180000,
    "first_Name": "Nicolás",
    "last_Name": "Jarma",
    "email": "njarma@gmail.com",
    "entry_Date": "2020-06-16T07:54:52",
    "enabled": 1,
    "balance": 3.33,
    "administrator": 1,
    "alias": "njarma",
    "id": 1
}
```

### Formatos de consultas
### **Sección User:**
Para consultar los métodos correspondientes a la sección **users**, se deberá respetar el siguiente formato:
```
https://localhost:44394/user-service/User/[nombre-del-método]
```
Por ejemplo, si quisera transferir el balance a otro usuario por medio de su Alias, utilice la siguiente ruta:
https://localhost:44394/user-service/User/TransferBalance

### **Sección Admin:**
Para consultar los métodos correspondientes a la sección **admin**, se deberá respetar el siguiente formato:
```
https://localhost:44394/user-service/Admin/[nombre-del-método]
```
Por ejemplo, si quisera agregar balance a un usuario por Id, utilice la siguiente ruta:
https://localhost:44394/user-service/Admin/AddBalance

### Interación con el proyecto api gateway para consultar API's
Para el ejemplo se utilizará el método que obtiene todos los usuarios del sistema, el cual es accesible únicamente para usuarios administradores.
Para interactuar con el proyecto **bitsa-api-users**, es necesario seguir los siguientes pasos:
1. Crear una consulta **GET en Postman** con la siguiente url: https://localhost:44394/user-service/Admin
2. Ir a la **pestaña AUTHORIZATION**, seleccionar de la lista desplegable la opción **Bearer Token** y pegar en la caja de téxto el token de la propiedad **access_token** que fue reservado en pasos anteriores. Se comparte un ejemplo para que vean el formato que deberá tener:
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxIiwiQWRtaW5pc3RyYXRvciI6IjEiLCJzdWIiOiJuamFybWFAZ21haWwuY29tIiwianRpIjoiMzUzZmZmMTYtNjUwNy00YzgwLWFmMmQtMTIwNDQzZWNjNDcyIiwiaWF0IjoiMTcvNi8yMDIwIDIwOjUxOjQ1IiwibmJmIjoxNTkyNDI3MTA1LCJleHAiOjE1OTI2MDcxMDUsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6IkJpdHNhIn0.w60ZH7I_OQTJEe8fPuoQOUy1t--bvREy4oSUc5GccE8
```
3. Presionar el botón **SEND**
4. Si el usuario posee rol administrador, devolverá la lista de todos los usuarios del sistema. Caso contrario, informará un error **401 Unauthorized**
