---
sidebar_position: 5
---

# Usando el punto final de obtención de préstamos de la API

## Proceso

* Se envía una consulta de información a PA2 a través de la API, [GET /v3/partner/{id}/loans](https://partner-api-stage.kiva.org/swagger-ui/#/partners/loansRouteUsingGET).
* Hay 4 parámetros de solicitud de búsqueda que puede enviar: query, status, offset y limit
  * Query: puede ser cualquier línea que se escriba en la barra de búsqueda de PA2 (por ejemplo, nombre, ID de préstamo, ID de cliente o ID de Kiva) 
  * Status: puede ser uno de los siguientes:
    * refunded
    * inactive
    * inactive_expired
    * fundRaising
    * expired
    * raised
    * payingBack
    * ended
    * reviewed
    * issue
    * defaulted  

Para garantizar que todos los pagos se reporten al utilizar la opción "GET", incluya todos los préstamos con el estado "PayingBack" y los préstamos "defaulted". Los préstamos en estos estados pueden recibir pagos y deben ser reportados a Kiva.    

**Offset & limit (“offset”, “limit”):** funciona igual que cualquier paginación, offset es la distancia que hay que recorrer en la lista y limit es el número de resultados que hay que devolver. Así que para obtener las primeras 20 coincidencias sería offset=0, limit=20 y las siguientes 20 serían offset=20 limit=20, etc..  El valor por defecto es offset=0 y limit=20.
* No recomendamos solicitar más de 300, ya que el volumen de datos puede llegar a ser muy grande y limitar los recursos del servidor.
* Después de realizar la solicitud, la respuesta de la API se devolverá en formato JSON

## Documentación técnica

* Toda la documentación técnica de Kiva, incluyendo los puntos finales, se puede encontrar aquí:
  * Entorno de pruebas (Stage): https://partner-api-stage.kiva.org/swagger-ui/
  * Producción (para usar después de las pruebas): https://partner-api.kiva.org/swagger-ui/
on (to use after testing): https://partner-api.kiva.org/swagger-ui/