---
sidebar_position: 6
---

# Uso de la API para reportar reembolsos
Recomendamos que su equipo técnico consulte al coordinador Kiva en su organización para entender completamente el proceso de elaboración de reportes de reembolsos. Para ello, solicite a su equipo técnico que revise lo siguiente:
* [Este vídeo](https://www.youtube.com/watch?v=KTgcTgjiX5A&t=1s) proporciona una explicación exhaustiva sobre cómo publicar reportes de reembolsos

Dado que el rendimiento del sistema es primordial para usted y para todos nuestros socios, le recomendamos que el número total de préstamos de su informe de reembolso se divida en archivos CSV con un máximo de 200-300 préstamos cada uno. Dado que cada CSV se genera con una llamada API independiente, cada llamada debería contener un máximo de 200-300 préstamos. En otras palabras, el reporte debe dividirse en varias llamadas, cada una enviando un archivo JSON con 200-300 préstamos.  

Incluso si su cartera actual tiene menos de 200 o 300 préstamos, sugerimos configurar la API para dividir automáticamente los futuros reportes en bloques de 200-300 préstamos. De esta manera, a medida que su cartera crezca, estará preparado para reportar grandes volúmenes sin inconvenientes.  

## Proceso
1. La información de reembolsos se envía desde el SIG a PA2 utilizando la API.
2. La persona encargada de los reportes de reembolso entra a PA2 donde revisa y finaliza el informe

## Información adicional
Para los socios que utilizan el formato de dos columnas para los informes de reembolso y el registro de seguimientos, sólo se necesita el ID del préstamo (internal_loan_id). Para los socios que utilizan el formato de tres columnas, se necesita tanto el ID del préstamo (internal_loan_ID) como el ID del cliente (internal_client_ID). Esto se puede verificar yendo a PA2 -> Cuenta -> Perfil -> Formato de carga del CSV.

Cuando pruebe la conexión de la API para los seguimientos, por favor, utilice la información de un cliente/prestatario real que haya sido publicado en Kiva. No utilice la misma información de cliente utilizada para publicar un borrador de préstamo de prueba, ya que PA2 no registrará este crédito como un préstamo activo cuyos reembolsos deben ser reportados.
  * Para encontrar un prestatario de Kiva existente, haga clic en la pestaña "Reembolsos" en PA2 y luego en el número azul de préstamos con reembolsos esperados. Esto le llevará a un reporte de todos los clientes de Kiva para los cuales Kiva espera un reembolso. Seleccione cualquiera de los ID de cliente y de préstamo de estos clientes para enviarlos a través de la API: ![repayments_expected.png](@site/static/img/repayments_expected.png)
  * La cantidad reportada debe coincidir con lo que aparece en el SIG de su organización.
  * El coordinador de Kiva aún deberá iniciar sesión en PA2 para finalizar el reporte de reembolsos.

Para comprobar si el formato JSON que ha creado es correcto, puede utilizar un validador JSON en línea como éste: https://jsonlint.com/ .

## Documentación técnica
* Toda la documentación técnica de Kiva, incluidos los puntos finales, puede encontrarse aquí:
  * Ambiente de prueba (Stage): https://partnerapi.staging.kiva.org/swagger-ui/
  * Producción (Para usar después de las pruebas): https://partnerapi.production.kiva.org/swagger-ui