# Dotnet Examples

## Getting started
1. Install [dotnet core](https://dotnet.microsoft.com/en-us/download).  These examples were written using dotnet core 7.
2. clone this repo
3. set the following environment.
   1. for mac OS and most linux OSes
   ```
    export client_id=[put your client id here]
    export client_secret=[put your client secret here]
    export audience=[put your audience value here]
    export scope=[put your scope value here]
   ```
   2. for windows command line
   ```
    set client_id=[put your client id here]
    set client_secret=[put your client secret here]
    set audience=[put your audience value here]
    set scope=[put your scope value here]
   ```

## Running the authorization API example   

1. navigate to the `./samples/dotnet` directory
2. to run the authorization example, run this command ` dotnet run --project auth/auth.csproj`

If the environment has been setup correct, the authorization json will be printed on screen like this:
```
Kiva Partner API for authorization

Results returned:
 {"access_token":"ggJhb9aiOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik5BQTdIeWlxIn0.xcJhdWQiOlsiaHR0cHM6Ly9wYXJ0bmVyLWFwaS1zdGFnZS5kazEua2l2YS5vcmciXSwic2NvcGUiOlsiY3JlYXRlOmxvYW5fZHJhZnQiLCJyZWFkOmxvYW5zIl0sImlzcyI6Imh0dHBzOi8vYXV0aC1zdGFnZS5kazEua2l2YS5vcmcvIiwicGFydG5lcklkIjoiNjMiLCJleHAiOjE2Njk3OTUzMzYsImp0aSI6Ikt6bkxNbkpwWE9tZDRZQ3VQbDRrWk1qcFg0OCIsImNsaWVudF9pZCI6Imd5a3E0ekxOR3BZQm0xMzR1OXlBNWFuRTJkbDM2WlBNaiJ9.m9UYEOr__VJvUFri_RWX9rsu6zD3ALizZZyqgsgav8yx9bCt1g8UzWayknRDc_NZogGhfdK1X7xTTCvK4ckQj2iWOvdgQvzg7r2jcX5a-MJ1QVP4PIMrbLoZs9grNzEB8_edB9xpadNQa2_AuCkYCwIPbun5XkG5FMUiYInEWrPAQI7rBytgVDQWL6gTJ93vWtnXQG8LDOBi5Nkp23YnxIvGptphdVrqMaL1ZrPwL3KvUAfym3gTWLjWlxzZ-MvF_aRKp2i0kNsj7wKOXy8OYPAV6d3VqptP6TNK69PbQOtVa_rmKNH5M2L1xAEnwzWtZeZTwZwry9H2ru8Ii61DnA",
  "token_type":"bearer",
   "expires_in":43199,
  "scope":"create:loan_draft read:loans",
  "iss":"https://auth-stage.kiva.org/",
  "partnerId":"9999",
  "jti":"F70LMnJpXOmd4YCuPl4kZMjpG99"}
```

## Running the get loans API example
1. navigate to the `./samples/dotnet` directory
2. to run the list loans example, run this command `dotnet run --project get.loans/get.loans.csproj`

## Running the loan draft API example
1. navigate to the `./samples/dotnet` directory
2. to run the list loans example, run this command `dotnet run --project loan.draft/loan.draft.csproj`


## Running the repayments API example
1. navigate to the `./samples/dotnet` directory
2. to run the list loans example, run this command `dotnet run --project repayments/repayments.csproj`

## Running the repayments API example
1. navigate to the `./samples/dotnet` directory
2. to run the list loans example, run this command `dotnet run --project journal/journal.csproj`