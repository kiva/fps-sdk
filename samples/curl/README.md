# Using curl with Kiva PA2 API

This document provides step-by-step instructions on how to use `curl` to interact with the Kiva PA2 API. 
It covers obtaining an OAuth2 access token and making authenticated requests to the API. This document assumes you have some
familiarity with using the command line and `curl`.

There is some other good documentation on Kiva's API here: https://fps-sdk-portal.web.app/ as well
as the swagger docs here: https://partnerapi.staging.kiva.org/swagger-ui/index.html

### Note
1. These commands were testing using zsh on MacOS. Adjustments may be needed for other shells or operating systems.
2. These commands are setup to use staging.  If you want to use production, replace the staging URLs with production URLs.

## Step 1
Export the following variables:

```bash
export PARTNER_ID=[a number]
export CLIENT_ID="[a client id]"
export CLIENT_SECRET="[client secret]"
export SCOPE="[scope values]"
export TOKEN=$(
  curl -s --location --request POST 'https://auth.staging.kiva.org/oauth/token' \
    -H 'Accept: application/json' \
    -H 'Content-Type: application/x-www-form-urlencoded' \
    --data-urlencode 'grant_type=client_credentials' \
    --data-urlencode "scope=$SCOPE" \
    --data-urlencode "client_id=$CLIENT_ID" \
    --data-urlencode "client_secret=$CLIENT_SECRET" \
    --data-urlencode 'audience=https://partner-api-stage.dk1.kiva.org' \
  | sed -n 's/.*"access_token":"\([^"]*\)".*/\1/p'
)
``` 

Update the shell variables `PARTNER_ID`, `CLIENT_ID`, `CLIENT_SECRET`, and `SCOPE` with your actual credentials.  These will be given to you when you register your application with Kiva.

_Note_: Partner id can be pulled from the OAuth token response as well, but we chose to set it explicitly here for simplicity.

## Step 2
If you defined the `$TOKEN` variable as shown above, you can skip this step.  Otherwise,
get an access token by making a POST request to the Kiva OAuth2 token endpoint:

```bash
curl --location --request POST 'https://auth.staging.kiva.org/oauth/token' \
  --header 'Accept: application/json' \
  --header 'Content-Type: application/x-www-form-urlencoded' \
  --data-urlencode 'grant_type=client_credentials' \
  --data-urlencode 'scope=create:journal_update create:loan_draft create:repayment read:loans' \
  --data-urlencode "client_id=$CLIENT_ID" \
  --data-urlencode "client_secret=$CLIENT_SECRET" \
  --data-urlencode 'audience=https://partner-api-stage.dk1.kiva.org'
```

It will return some data like this:

```json
{"access_token":"eyJraWQiOiJOQUE3...truncoted....9Y30DwIw","scope":"create:journal_update create:repayment read:loans create:loan_draft","iss":"https://auth-stage.dk1.kiva.org/","partnerId":"145","token_type":"bearer","expires_in":43199,"jti":"4bf21f5f-7f87-4b86-9ca8-fa53a50b67d9"}% 
```

Please note that the `access_token` value above is truncated for readability.  You will need to extract the full value from your actual response.

Extract the access_token value from the response and set it as an environment variable named TOKEN.
You can use the following command to do this in a Unix-like shell:

```bash
export TOKEN="eyJraWQiOiJOQUE3...truncoted....9Y30DwIw"
```

## Step 3 Calling a GET API
For starters, let's make a GET request to themes for your loan accounts:

```bash
curl -X 'GET' \
  "https://partnerapi.staging.kiva.org/v3/partner/$PARTNER_ID/config/themes" \
  -H 'accept: application/json' \
  -H "Authorization: Bearer $TOKEN"
```

If everything is set up correctly, you should get back a JSON response with an array of theme data.
```json
{"as_of_date_time":"2026-01-09T15:48:12Z","themes":[{"theme_type_id":1,"theme_type":"Green"},{"theme_type_id":2,"theme_type":"Higher Education"},{"theme_type_id":48,"theme_type":"General"},{"theme_type_id":63,"theme_type":"Community Impact Loan"},{"theme_type_id":105,"theme_type":"Water and Sanitation"}],"code":"api.success","message":"Found 5 value(s) for configuration 'themes'"}
```

You can replace the URL and parameters in the curl command to call other endpoints of the Kiva PA2 API as needed. Just ensure you include the `Authorization` header with the Bearer token for authenticated requests.
