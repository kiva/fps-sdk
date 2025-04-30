# Authorization

Kiva Protocol uses OAuth for site access and role security.

The main takeaway for authorization is that your implementation must get a token and pass that token in the `Authorization` header of the API call.

The token is retrieved from body returned from [API authentication](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360051231131-API-authentication-client-credential-flow-).

A token will be returned in `access_token` field in the return body, formatted like this:
```
{
    "access_token": "fFaAzZciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik5BQTdIeWlxIn0.eyJhdWQiOlsiaHR0cHM6Ly9wYXJ0bmVyLWFwaS1zdGFnZS5kazEua2l2YS5vcmciXSwic2NvcGUiOlsicmVhZDpsb2FucyJdLCJpc3MiOiJodHRwczovL2F1dGgtc3RhZ2UuZGsxLmtpdmEub3JnLyIsInBhcnRuZXJJZCI6IjYzIiwiZXhwIjoxNjY5Nzc4MjQyLCJqdGkiOiJRczRxb09KeUM2dndrTDFGb1hPUnhmcXdOaXMiLCJjbGllbnRfaWQiOiJneWtxNHpMTkdwWUJtMTM0dTl5QTVhbkUyZGwzNlpQTWoifQ.YmCB_ODPiaVwh-qOzeuYRzfiO5jbw0sBuFEnijX0S_TgwzIOuwtgOvJh0H0kKU3BrJOYAW0ZVXaqc6nW5wsPpS21zptwUFiVMzXQhj97bk4hcox8H-CeI__32wbWomsc7c1hcxzoyFpfaoeTP6pLB0XoUlb6ynrhCKGUIWgv2P9hE79lBM7mk0ZczCsnyw5sB-0_FP_yI4a_IVIk2dYJZqIu49zptgIg1nnCdiA1YlzVGe9YInEcOiVm2DD8BkTKlOBkT1C-YlxudcMx8IPAP5Cq-JAWCFRwDLT-JvpXQUpbTSGAdFJt0AXozvtnmmwOvfvCiUxusncjaiUcQpqFag",
    "token_type": "bearer",
    "expires_in": 43199,
    "scope": "read:loans",
    "iss": "https://auth-stage.kiva.org/",
    "partnerId": "7326",
    "jti": "Qs4qoOJyC6vwkL1FoXORxfqwNis"
}
```

When sent in the API headers, the value from `access_token` becomes the value of `Authorization` header formatted like this:
```
bearer fFaAzZciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik5BQTdIeWlxIn0.eyJhdWQiOlsiaHR0cHM6Ly9wYXJ0bmVyLWFwaS1zdGFnZS5kazEua2l2YS5vcmciXSwic2NvcGUiOlsicmVhZDpsb2FucyJdLCJpc3MiOiJodHRwczovL2F1dGgtc3RhZ2UuZGsxLmtpdmEub3JnLyIsInBhcnRuZXJJZCI6IjYzIiwiZXhwIjoxNjY5Nzc4MjQyLCJqdGkiOiJRczRxb09KeUM2dndrTDFGb1hPUnhmcXdOaXMiLCJjbGllbnRfaWQiOiJneWtxNHpMTkdwWUJtMTM0dTl5QTVhbkUyZGwzNlpQTWoifQ.YmCB_ODPiaVwh-qOzeuYRzfiO5jbw0sBuFEnijX0S_TgwzIOuwtgOvJh0H0kKU3BrJOYAW0ZVXaqc6nW5wsPpS21zptwUFiVMzXQhj97bk4hcox8H-CeI__32wbWomsc7c1hcxzoyFpfaoeTP6pLB0XoUlb6ynrhCKGUIWgv2P9hE79lBM7mk0ZczCsnyw5sB-0_FP_yI4a_IVIk2dYJZqIu49zptgIg1nnCdiA1YlzVGe9YInEcOiVm2DD8BkTKlOBkT1C-YlxudcMx8IPAP5Cq-JAWCFRwDLT-JvpXQUpbTSGAdFJt0AXozvtnmmwOvfvCiUxusncjaiUcQpqFag
```


### Samples

We have provided some sample code which demonstrates getting and using the OAuth token.

[Typescript](../samples/typescript/README.md) | [PostMan](../samples/postman/README.md) | [C#](../samples/dotnet/README.md)

## Errors

Anytime you make an API call and get a `Unauthorized 401` error, the first thing to suspect is the token (if the API requires the token), 
or the credentials used when attempting to get a token.

You can use this table to help diagnosis `Unauthorized 401` errors.

| API | Header Fields to Check   | Documentation                                                                                                               |
| ------ |--------------------------|-----------------------------------------------------------------------------------------------------------------------------|
| API authentication | client_id, client_secret | [link](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360051231131-API-authentication-client-credential-flow-) | 
| Get Loans | Authorization            | [link](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360051230151-Using-the-API-loans-fetch-endpoint)         |
| All other API | Authorization | [link](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360051228911-Overview-of-Kiva-s-API)                     |



## Resources
[oAuth Documentation](https://oauth.net/2/)  