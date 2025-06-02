---
sidebar_position: 7
---

# Utilización de la API para publicar seguimientos

Recomendamos que su equipo técnico consulte al coordinador Kiva en su organización para entender completamente el proceso de elaboración de seguimientos Para ello, solicite a su equipo técnico que revise lo siguiente:

* [Este vídeo](https://www.youtube.com/watch?v=9KrerX22pQQ) proporciona una explicación exhaustiva sobre cómo publicar seguimientos.
* Visita la sección “[Seguimientos](https://kivapartnerhelpcenter.zendesk.com/hc/es/categories/360001945772-Entradas-de-Seguimiento)” para más información.

## Proceso
1. La información de un nuevo seguimiento se envía desde el SIG a PA2 a través de la API.
2. La API devolverá una respuesta JSON con una confirm_url.
3. Una persona tiene que ir a la confirm_url y publicar el borrador del seguimiento.

## Información adicional
* Para los socios que utilizan el formato de dos columnas para los informes de reembolso y el registro de seguimientos, sólo se necesita el ID del préstamo (`internal_loan_id`). Para los socios que utilizan el formato de tres columnas, se necesita tanto el ID del préstamo (`internal_loan_id`) como el ID del cliente (`internal_client_id`). *Esto se puede verificar yendo a PA2 -> Cuenta -> Perfil -> Formato de carga del CSV.*
* Cuando pruebe la conexión de la API para los seguimientos, por favor, utilice la información de un cliente/prestatario real que haya sido publicado en Kiva. No utilice la misma información de cliente utilizada para publicar un borrador de préstamo de prueba, ya que PA2 no registrará a este prestatario como elegible para una actualización del seguimiento.
  * Para encontrar un prestatario de Kiva existente, haga clic en la pestaña "Seguimientos" en PA2. Esto le llevará a un informe de todos los clientes de Kiva que son elegibles para un seguimiento. Seleccione cualquiera de los ID de cliente y préstamo de estos clientes para enviarlos a través de la API.
  * El contenido del seguimiento puede ser el que usted desee. El coordinador de Kiva sigue teniendo que entrar en PA2 para revisar los seguimientos antes de enviarlos.

## Documentación técnica
Toda la documentación técnica de Kiva, incluidos los puntos finales, puede encontrarse aquí:
 * Entorno de prueba (Stage): https://partnerapi.staging.kiva.org/swagger-ui/
 * Producción (Para usar después de las pruebas): https://partnerapi.production.kiva.org/swagger-ui
