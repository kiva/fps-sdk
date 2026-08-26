---
sidebar_position: 8
---

# Pasando al entorno de producción (Después de las pruebas)

___Por favor, sólo cambie al entorno de producción de Kiva después de haber recibido la confirmación para hacerlo por parte de nosotros. Si desea autenticarse en el entorno de prueba, visite [esta página](/es/docs/overview/authentication).___

Tendrá que actualizar cuatro datos centrales en su autentificación:

* Token URL: https://auth.k1.kiva.org/oauth/token
* Audience/Partner API URL: https://partner-api.k1.kiva.org
* Puede encontrar la siguiente información accediendo a PA2> Cuenta > Acceso a la API > Acceso a la información de la API, o en un correo electrónico enviado desde Kiva
  * client_id
  * client_secret
  
### Solicitud de muestra
```
1 curl --location --request POST 'https://auth.k1.kiva.org/oauth/token' \
2 --header 'Accept: application/json' \
3 --header 'Content-Type: application/x-www-form-urlencoded' \
4 --data-urlencode 'grant_type=client_credentials' \
5 --data-urlencode 'scope=create:loan_draft read:loans' \
6 --data-urlencode 'audience=https://partner-api.k1.kiva.org' \
7 --data-urlencode 'client_id=<client ID>' \
8 --data-urlencode 'client_secret=<client secret from Partner Admin>'
```
### Post Data
* **grant_type** - required. Siempre tendrá el valor client_credentials.
* **scope** - representa las acciones que dentro de la API del socio deben ser ejecutadas. Si el socio está autorizado para estas acciones, el JWT devuelto contendrá todas las acciones autorizadas. Los scopes válidos son read:loans, create:loan_draft, create:journal_update, create:repayment
* **audience** - identifica la audiencia del JWT, que es la API donde se utilizará el JWT. Para la API del entorno de producción, esto es:  https://partner-api.k1.kiva.org.
* **client_id** - es la primera mitad de las credenciales del cliente. Es accesible desde Partner Admin e identifica directamente al socio dentro del sistema de Kiva.
* **client_secret** -esta es la segunda mitad de las credenciales del cliente. Se puede acceder a ella desde Partner Admin y es necesaria para validar una solicitud de credenciales de cliente. client_id y client_secret deben tratarse como secretos sensibles.

### Datos de respuesta
Si la autenticación es exitosa, debería recibir una respuesta como la siguiente:

```json
1 {
2    "access_token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjFublhjRFRHIn0.eyJhdWQiOlsiaHR0cHM6Ly9wYXJ0bmVyLWFwaS5rMS5raXZhLm9yZyJdLCJzY29wZSI6WyJjcmVhdGU6bG9hbl9kcmFmdCIsInJlYWQ6bG9hbnMiXSwiaXNzIjoiaHR0cHM6Ly9hdXRoLmsxLmtpdmEub3JnLyIsInBhcnRuZXJJZCI6IjEiLCJleHAiOjE2MDIxNTY2MTgsImp0aSI6IlJVc2l2VVhHZ2hoeC1Zdjl6emEzZ2daaTZhbyIsImNsaWVudF9pZCI6IlFEMmxPRzZMbTN2RWQ5QTZEdVh3eFJWOE1OMEp6cDVreSJ9.U_tCMX5ra7Q0NFwr1FKlgqCBEmlprY-PuWRv6bNzEREtJABh0hBr-zEKXQEhHYTpHjjNquOHK7Q8hnQ30IVVhE6jXUO8_OgRfmczlQ8sDkRzmx5PTc99my0bs6zn8owRfEEwBGJcvNt_oT8iRASnlij99d7dozTFguBnT7_hauXoq2C4DFmRx3rjfnCbI9G7Ue_4Gh3jnF7VYI9HefLvYHBCS0SP3a-QqNuR5w1itRevj8KOIhC5lKuJn22cRXW9PQL3G9XGyK0h8sFZj7blhLETMLFAHbrWFUGzawEBAeLQbQhvvu78dp0RzgY0OvS2XXzTgxpg0TcgsrWuDdjFAA",
3    "token_type": "bearer",
4    "expires_in": 43199,
5    "scope": "create:loan_draft read:loans",
6    "iss": "https://auth.k1.kiva.org/",
7    "partnerId": "1",
8    "jti": "RUsivUXGghhx-Yv9zza3ggZi6ao"
9 }
```
* **access_token** - es el token de portador que se utilizará para acceder a la API de socios.
* **token_type** -  indica el tipo de token, que siempre debe ser de portador.
* **expires_in** -  número de segundos durante los que el token será válido (compruebe también la exp claim dentro del JWT para ver la fecha de caducidad).
* **scope** -  es una intersección entre los scopes, ambitos solicitados y los ámbitos para los que el socio ha sido autorizado.
* **iss** - el emisor del JWT.
* **partnerId** - el identificador de Kiva para el socio. Puede encontrar su partnerId en PA2 yendo a la página Cuenta > Perfil.
* **jti** - el identificador único para el token


### Partner API Authentication
Una vez que haya recibido un token de acceso, introdúzcalo como token de portador en la cabecera de autorización como en el siguiente ejemplo de curl. Tendrá que personalizar dos componentes:

1. Inserte su ID de socio en la URL donde dice "PARTNERID". Puede encontrar su ID de socio en PA2 yendo a la página Cuenta > Perfil.
2. Inserte el token único al portador que recibió.


```json
curl --location --request GET 'https://partner-api.k1.kiva.org/v3/partner/PARTNERID/loans' \
--header 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjFublhjRFRHIn0.eyJhdWQiOlsiaHR0cHM6Ly9wYXJ0bmVyLWFwaS5rMS5raXZhLm9yZyJdLCJzY29wZSI6WyJjcmVhdGU6bG9hbl9kcmFmdCIsInJlYWQ6bG9hbnMiXSwiaXNzIjoiaHR0cHM6Ly9hdXRoLmsxLmtpdmEub3JnLyIsInBhcnRuZXJJZCI6IjEiLCJleHAiOjE2MDIyMjA0MTYsImp0aSI6IlpldUt0WTZXQU5VU2lWai1EZTVtZE5nRnFGSSIsImNsaWVudF9pZCI6IlFEMmxPRzZMbTN2RWQ5QTZEdVh3eFJWOE1OMEp6cDVreSJ9.mdOHScBFzkKribTjFCfUG_BrzrDELFgznvp7OPwDvE_-dOZ-qbSR0IoItgw9Nzsgv13pY0MOM8euEzHThvaxi8gtr1WV0MY4TCE3ffgApaUo_-uC5cXu1NoMPjToE53kHthRmv4cWOu_ycFYMvPV606U24Jsgs1txNrobu_ZlUsaFpyPN-9Pr1wq8N0VQWOS9qt_lkKB0aJhbMHsNOHysTXTclkGh2jbXKj10H5LnXBQsh-UpLSKCw3UoMlepR4tjRxyXnSYLgZ80jTPSsOU1oKkAYdLRSbUHEM4g30FfZ8__kUI7LNtlmuVWYNV3ZVn0yxLO1wSu4n31TsIZUX_Ag
```

Para pasar de Stage a producción: Aquí está la URL de la documentación técnica para producción. https://partner-api.k1.kiva.org/swagger-ui/