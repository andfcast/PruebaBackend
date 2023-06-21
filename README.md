# PruebaBackend
Esta es la prueba de Backend.
Se crearon en la solución los proyectos siguientes:
-- API: Donde se encuentra la declaración de las operaciones expuestas del CRUD
-- AccesoDatos: Donde se encuentra la asociación con la BD. 
-- LogicaNegocio: Capa de lógica de negocio
-- Persistencia: Donde encontramos tanto als ultidades de conversión entre Dto y Entidad, como las distintas entidades mapeadas de base de datos y las Dto utilizadas
-- Tarea Programada: se creó un worker service que se ejecuta cada 30 seg y cambia el contador para determinar la página que se va a enviar en la petición al servicio externo.

Se manejó una BD de SQLite. Se proporciona la carpeta DB que está en la solución, pero esta carpeta debe copiarse a la raíz de la unidad C.
Para este momento se entrega ese archivo sin registros, sólo con la tabla creada.

Los GUID usados para el punto de la autenticación, y que van en el header de la petición de creación son:

Client_secret = F9730667-4137-4A3E-902C-E771F5AE8683
Client_id = EFAF9902-0DE7-43AF-8B60-FCA5F70BC8E3

Se utilizó Swagger para documentar las operaciones expuestas en el proyecto de WebAPI

Para la ejecución de los proyectos, por defecto arrancará con la aplicación de WEB API. Previamente, se debió atender la 
recomendación referente a la ubicación del archivo de base de datos.
