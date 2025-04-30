---
sidebar_position: 3
---

# Authentification sur l'API

Les instructions suivantes sont destinées à faciliter l'authentification à l'environnement de test Kiva, Stage. Il s'agit de la première étape de toute intégration de l'API. Si vous avez déjà terminé vos tests et que ceux-ci ont été approuvés par Kiva, suivez les instructions de la section [Authentification dans l'environnement de production Kiva (APRÈS les tests)](/docs/overview/after_testing).

Il est possible d'accéder à la nouvelle API des partenaires via un Oauth2 JWT obtenu auprès du service d'authentification Kiva en suivant le flux d'informations d'identification du client Oauth2. Plus simplement, il s'agit d'un échange des informations d'identification du client du partenaire contre un token limité dans le temps qui peut être utilisé pour l'authentification et l'autorisation.

### Demande d'échantillon (environnement de test)

```json
1 curl --location --request POST 'https://auth-stage.kiva.org/oauth/token' \
2 --header 'Accept: application/json' \
3 --header 'Content-Type: application/x-www-form-urlencoded' \
4 --data-urlencode 'grant_type=client_credentials' \
5 --data-urlencode 'scope=create:loan_draft read:loans' \
6 --data-urlencode 'audience=https://partner-api-stage.kiva.org' \
7 --data-urlencode 'client_id=<client ID>' \
8 --data-urlencode 'client_secret=<client secret from Partner Admin>'
```

### Post Data
* **grant_type** - required. Il doit toujours avoir la valeur client_credentials. .
* **scope** - représente les actions qui doivent être exécutées dans l'API du partenaire. Si le partenaire est autorisé pour ces actions, le JWT retourné contiendra toutes les actions autorisées. Les champs d'application valides sont read:loans, create:loan_draft, create:journal_update, create:repayment.
* **audience** - identifie l'audience JWT, qui est l'API où le JWT sera utilisé.
* **client_id** -Il s'agit de la première moitié des informations d'identification du client. Il est accessible depuis l'administration des partenaires et identifie directement le partenaire dans le système Kiva.
* **client_secret** - C'est la deuxième moitié des informations d'identification du client. Il est accessible depuis Partner Admin et est nécessaire pour valider une demande d'informations d'identification du client. client_id et client_secret doivent être traités comme des secrets sensibles.

### Détails sur "Scope" (champ d'application opérationnel de l'API)

Le " Scope " (champ d'application) identifie les APIs auxquelles il est possible d'accéder. Utilisez le tableau suivant pour déterminer les champs d'application (Scopes) à inclure:

| Scope (Campo de aplicación)                                                      | API                                                          |  
|------------------------------------------------------------|--------------------------------------------------------------|
| read:loans | https://partner-api.kiva.org/v3/partner/{id}/loans?limit=20&offset=0 |    
| create:loan_draft | https://partner-api.kiva.org/v3/partner/{id}/loan_draft      |    
| create:journal_update                                                 | https://partner-api.kiva.org/v3/partner/{id}/journals        |    
| create:repayment                                                  | https://partner-api.kiva.org/v3/partner/{id}/repayments |

Consultez [la documentation de l'API dans Swagger](https://partner-api-stage.kiva.org/swagger-ui/) pour obtenir des informations spécifiques sur chaque API.

### Response Data (environnement de test)
Si l'authentification est correcte, vous devriez recevoir une réponse comme la suivante :


```json
1 {
2    "access_token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjFublhjRFRHIn0.eyJhdWQiOlsiaHR0cHM6Ly9wYXJ0bmVyLWFwaS5rMS5raXZhLm9yZyJdLCJzY29wZSI6WyJjcmVhdGU6bG9hbl9kcmFmdCIsInJlYWQ6bG9hbnMiXSwiaXNzIjoiaHR0cHM6Ly9hdXRoLmsxLmtpdmEub3JnLyIsInBhcnRuZXJJZCI6IjEiLCJleHAiOjE2MDIxNTY2MTgsImp0aSI6IlJVc2l2VVhHZ2hoeC1Zdjl6emEzZ2daaTZhbyIsImNsaWVudF9pZCI6IlFEMmxPRzZMbTN2RWQ5QTZEdVh3eFJWOE1OMEp6cDVreSJ9.U_tCMX5ra7Q0NFwr1FKlgqCBEmlprY-PuWRv6bNzEREtJABh0hBr-zEKXQEhHYTpHjjNquOHK7Q8hnQ30IVVhE6jXUO8_OgRfmczlQ8sDkRzmx5PTc99my0bs6zn8owRfEEwBGJcvNt_oT8iRASnlij99d7dozTFguBnT7_hauXoq2C4DFmRx3rjfnCbI9G7Ue_4Gh3jnF7VYI9HefLvYHBCS0SP3a-QqNuR5w1itRevj8KOIhC5lKuJn22cRXW9PQL3G9XGyK0h8sFZj7blhLETMLFAHbrWFUGzawEBAeLQbQhvvu78dp0RzgY0OvS2XXzTgxpg0TcgsrWuDdjFAA",
3    "token_type": "bearer",
4    "expires_in": 43199,
5    "scope": "create:loan_draft read:loans",
6    "iss": "https://auth.stage.kiva.org/",
7    "partnerId": "1",
8    "jti": "RUsivUXGghhx-Yv9zza3ggZi6ao"
9 }
```

* **access_token** - est le token du porteur à utiliser pour accéder à l'API.
* **token_type** - indique le type de token, qui doit toujours être un token porteur.
* **expires_in** - nombre des secondes pendant lesquelles le token sera valide (vérifiez également la date d'expiration dans le JWT).
* **scope** - est une intersection entre les scopes, demandés et les scopes pour lesquels le partenaire a été autorisé.
* **iss** - l'émetteur du JWT.
* **partnerId** - l'identifiant Kiva du partenaire. Vous pouvez trouver votre partnerId dans PA2 en allant sur la page Compte > Profil.
* **jti** - l'identifiant unique du token


### Partner API Authentication
Une fois que vous avez reçu un token d'accès, entrez-le comme token porteur dans l'en-tête d'autorisation comme dans l'exemple curl suivant. Vous devrez personnaliser deux éléments :
1. Insérez votre ID de partenaire dans l'URL où il est indiqué "PARTNERID". Vous pouvez trouver votre ID de partenaire dans PA2 en allant sur la page Compte > Profil.
2. Insérez le token unique au porteur que vous avez reçu.

```
curl --location --request GET 'https://partner-api-stage.kiva.org/v3/partner/PARTNERID/loans' \
--header 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjFublhjRFRHIn0.eyJhdWQiOlsiaHR0cHM6Ly9wYXJ0bmVyLWFwaS5rMS5raXZhLm9yZyJdLCJzY29wZSI6WyJjcmVhdGU6bG9hbl9kcmFmdCIsInJlYWQ6bG9hbnMiXSwiaXNzIjoiaHR0cHM6Ly9hdXRoLmsxLmtpdmEub3JnLyIsInBhcnRuZXJJZCI6IjEiLCJleHAiOjE2MDIyMjA0MTYsImp0aSI6IlpldUt0WTZXQU5VU2lWai1EZTVtZE5nRnFGSSIsImNsaWVudF9pZCI6IlFEMmxPRzZMbTN2RWQ5QTZEdVh3eFJWOE1OMEp6cDVreSJ9.mdOHScBFzkKribTjFCfUG_BrzrDELFgznvp7OPwDvE_-dOZ-qbSR0IoItgw9Nzsgv13pY0MOM8euEzHThvaxi8gtr1WV0MY4TCE3ffgApaUo_-uC5cXu1NoMPjToE53kHthRmv4cWOu_ycFYMvPV606U24Jsgs1txNrobu_ZlUsaFpyPN-9Pr1wq8N0VQWOS9qt_lkKB0aJhbMHsNOHysTXTclkGh2jbXKj10H5LnXBQsh-UpLSKCw3UoMlepR4tjRxyXnSYLgZ80jTPSsOU1oKkAYdLRSbUHEM4g30FfZ8__kUI7LNtlmuVWYNV3ZVn0yxLO1wSu4n31TsIZUX_Ag
```