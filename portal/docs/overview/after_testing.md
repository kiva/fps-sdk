---
sidebar_position: 8
---

# Using production environment

___Please only switch to Kiva's production environment after you've received confirmation to do so from Kiva. If you are looking to authenticate with Kiva's test environment, visit [this page](/docs/overview/authentication).___

You will need to update four central pieces of information in your authentication:

* Token URL: https://auth.k1.kiva.org/oauth/token
* Audience/Partner API URL: https://partner-api.k1.kiva.org
* You can find the below information by navigating to PA2 > Account > API Access > Access API Information, or in an email from Kiva
  * client_id
  * client_secret
  
### Sample Request
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
* **grant_type** - required. This will always have the value client_credentials.
* **scope** - this represents the actions within the Partner API that need to be executed. If the partner is authorized for these actions, the returned JWT will contain all authorized actions. Valid scopes are read:loans, create:loan_draft, create:journal_update, create:repayment.
* **audience** - this identifies the audience for the JWT, which is the API where the JWT will be used. For the production Partner API, this is https://partner-api.k1.kiva.org.
* **client_id** - this is the first half of the client credentials. It is accessible from within Partner Admin and directly identifies the Partner within Kiva’s system.
* **client_secret** - this is the second half of the client credentials. Both It is accessible from within Partner Admin and is required to validate a client credentials request. client_id and client_secret should be treated as sensitive secrets.

### Response Data
On successful authentication, you should receive a response like the following:
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
* **access_token** - this is the bearer token that you will use to access the Partner API.
* **token_type** - this indicates the type of token, this should always be bearer.
* **expires_in** - number of seconds the token will be valid for (also check the exp claim inside the JWT for an expiration timestamp).
* **scope** - this is an intersection between scopes that were requested and scopes that the Partner has been authorized for.
* **iss** - the issuer of the JWT.
* **partnerId** - the Kiva identifier for the partner. You can find your partnerId in PA2 by going to the Account > Profile page.
* **jti** - the unique identifier for the token

### Partner API Authentication
Once you have received an access token, supply it as a Bearer token in the Authorization header as in the below curl example. You will need to customize two components:

Insert your partner ID in the URL where it says "PARTNERID". You can find your partner ID in PA2 by going to the Account > Profile page.
Insert the unique bearer token you received

```json
curl --location --request GET 'https://partner-api.k1.kiva.org/v3/partner/PARTNERID/loans' \
--header 'Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjFublhjRFRHIn0.eyJhdWQiOlsiaHR0cHM6Ly9wYXJ0bmVyLWFwaS5rMS5raXZhLm9yZyJdLCJzY29wZSI6WyJjcmVhdGU6bG9hbl9kcmFmdCIsInJlYWQ6bG9hbnMiXSwiaXNzIjoiaHR0cHM6Ly9hdXRoLmsxLmtpdmEub3JnLyIsInBhcnRuZXJJZCI6IjEiLCJleHAiOjE2MDIyMjA0MTYsImp0aSI6IlpldUt0WTZXQU5VU2lWai1EZTVtZE5nRnFGSSIsImNsaWVudF9pZCI6IlFEMmxPRzZMbTN2RWQ5QTZEdVh3eFJWOE1OMEp6cDVreSJ9.mdOHScBFzkKribTjFCfUG_BrzrDELFgznvp7OPwDvE_-dOZ-qbSR0IoItgw9Nzsgv13pY0MOM8euEzHThvaxi8gtr1WV0MY4TCE3ffgApaUo_-uC5cXu1NoMPjToE53kHthRmv4cWOu_ycFYMvPV606U24Jsgs1txNrobu_ZlUsaFpyPN-9Pr1wq8N0VQWOS9qt_lkKB0aJhbMHsNOHysTXTclkGh2jbXKj10H5LnXBQsh-UpLSKCw3UoMlepR4tjRxyXnSYLgZ80jTPSsOU1oKkAYdLRSbUHEM4g30FfZ8__kUI7LNtlmuVWYNV3ZVn0yxLO1wSu4n31TsIZUX_Ag
```

For switching from stage to prod: Here is the URL for the [technical documentation](https://partnerapi.production.kiva.org/swagger-ui).