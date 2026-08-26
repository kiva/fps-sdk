---
sidebar_position: 4
---

# Uso de la API para publicar borradores de préstamos

Recomendamos que su equipo técnico consulte con el coordinador de Kiva de su organización para comprender plenamente el proceso de publicación de borradores de préstamos de Kiva. Para ello, pida a su equipo técnico que revise lo siguiente:
* En [este vídeo](https://www.youtube.com/watch?v=9gScexv-yZo&amp;t=5s) se explica detalladamente cómo publicar un préstamo individual
* En [este vídeo](https://www.youtube.com/watch?v=KvKUScWF73M&amp;t=1s) ofrece una explicación detallada sobre cómo publicar un préstamo grupal
* Visite la sección de Nuevos Préstamos para obtener más información, concretamente 
* el [Paso 1: Descripción - Préstamos individuales](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360030919632) y el [Paso 1: Descripción - Préstamos grupales](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360031260191)

## Proceso

* La información de un nuevo préstamo se envía desde el SIG a PA2 a través de la API.
* Se crea automáticamente un borrador préstamo. Los borradores se pueden ver haciendo clic en el enlace "Borradores" en el cuadro "Préstamos" de la página de inicio de PA2.
* Cada borrador deberá ser revisado y luego publicado por una persona.

### Información adicional

Antes de que el préstamo sea publicado, PA2 ejecutará todas las mismas revisiones de validación que ejecuta en los préstamos publicados sin la API. Todos los mensajes de error deberán ser resueltos antes de que el préstamo pueda ser publicado.
 
Está bien que la API no envíe información de algunos campos necesarios para publicar un préstamo. Por ejemplo, no todos los SIGs almacenan campos específicos de Kiva como la descripción del prestatario o el sector. Aunque estos campos son necesarios para publicar un préstamo en PA2, no es necesario que se envíen a través de la API. La información para estos campos puede ser añadida manualmente después de que el borrador del préstamo haya sido creado en PA2. Tenga en cuenta que cuanta más información pueda enviar a través de la API, más tiempo ahorrará el coordinador de Kiva.

Dependiendo de si está publicando un préstamo de grupo o individual, algunos campos no son necesarios. Si el préstamo que está publicando es un préstamo individual, y envía datos que son específicos de un préstamo de grupo, PA2 le dará un mensaje de error y no podrá publicar el préstamo.

Si se publica un préstamo individual, no se debe incluir ninguno de los siguientes campos. **Los siguientes campos sólo son necesarios cuando se contabiliza un préstamo grupal:**

* `group_name`: este es el nombre del grupo que aparecerá en Kiva.org.
* `internal_client_id`: es el ID de cada cliente representado dentro del grupo (por ejemplo, si un grupo tiene tres miembros, cada uno de ellos puede tener su propio ID de cliente que debe figurar aquí)
* `internal_loan_id`: este es el ID de cada préstamo individual (por ejemplo, un miembro del grupo podría estar tomando su tercer préstamo con la organización, y ese préstamo podría tener un ID único. Introduzca ese ID aquí)
* `not_pictured`: utilice este campo en caso de que algún prestatario figure en el Paso 1: La descripción no aparece en la foto

Para publicar un préstamo grupal, es obligatorio incluir al menos un objeto `entreps` (correspondiente a la información de los emprendedores) en el JSON.

### Parámetro: not_pictured

El campo not_pictured es una lista de valores verdadero o falso que corresponden al orden de los prestatarios listados en el Paso 1:  [Paso 1: Descripción - Préstamos grupales](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360031260191).

Por ejemplo, si el primer prestatario enumerado en el Paso 1 no aparece en la foto, el primer valor de la lista not_pictured debe ser verdadero. Si el segundo prestatario enumerado en el Paso 1 aparece en la foto, el segundo valor de la lista not_pictured debe ser false. Si el tercer prestatario del paso 1 no aparece en la foto, el tercer valor de la lista not_pictured debe ser true. Y así sucesivamente.
Al proporcionar valores para el campo not_pictured, se deben proporcionar valores para todos los prestatarios enumerados en el Paso 1. 

La foto del prestatario puede ser enviada a PA2 a través de la API. Los calendarios de reembolso pueden enviarse en varios formatos diferentes. Si cree que un formato diferente al del ejemplo siguiente funcionará mejor para su organización, comuníquelo a Kiva y le proporcionaremos más información. Para comprobar si el documento JSON que has creado es correcto, puedes utilizar un validador de JSON online como este: https://jsonlint.com/.

### Configuraciones para los Borradores de préstamos

Para crear un borrador de préstamo con los campos completamente diligenciados, hay varios campos que deben rellenarse con códigos/comandos que se definen en el sistema de Kiva. Cada uno de estos comandos puede recuperarse a través de las configuraciones de las API. **Tenga en cuenta que estas API son de sólo lectura.**

Puede encontrar ejemplos de llamadas a estos puntos finales en nuestra [colección Postman](https://github.com/kiva/fps-sdk/tree/main/samples/postman).

#### Desde PA2 - Paso 1: Descripción
* **1**: `location` - Debe completarse con una ubicación definida actualmente en el sistema de Kiva. Las ubicaciones se gestionan en la sección Cuenta -> Ubicaciones en PA2.
    * [GET Partner Locations](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/locationConfigsRouteUsingGET)
* **2**: `activity_id` - Debe completarse con el descriptor de la Actividad del Préstamo.
    * [GET Loan Activities](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/activityConfigsRouteUsingGET)
* **3**: `description_language_id` - Debe completarse con el descriptor de la Actividad del Préstamo. 
    * [GET Locales](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/localeConfigsRouteUsingGET)

Al introducirse en el Borrador del Préstamo, estos campos aparecerán en PA2 de esta manera
![loandraft_step1.png](@site/static/img/pa2/loandraft_step1.png)

*Tenga en cuenta que el campo Sector Principal se establece automáticamente a partir de la Actividad proporcionada.*

#### De PA2 - Paso 2: Condiciones del Préstamo
* **4**: `theme_type_id` - Debe rellenarse con el identificador de la Campaña de Préstamo. Se trata de una clasificación del préstamo más amplia que la Actividad de Préstamo que es relevante para los términos y condiciones acordados por el socio.
    * [GET Loan Themes](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/themeConfigsRouteUsingGET)
* **5**: `currency` - El código ISO de la divisa correspondiente al importe que solicita el prestatario.
    * [GET Locales](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/localeConfigsRouteUsingGET)

Cuando se rellenan en el Borrador del Préstamo, estos campos aparecerán en PA2 de la siguiente manera:
![loandraft_step2.png](@site/static/img/pa2/loandraft_step2.png)

## Documentación técnica
Toda la documentación técnica de Kiva, incluidos los puntos finales, puede encontrarse aquí:
* Entorno de pruebas (Stage): https://partnerapi.staging.kiva.org/swagger-ui/
* Producción (para usar después de las pruebas):https://partnerapi.production.kiva.org/swagger-ui
