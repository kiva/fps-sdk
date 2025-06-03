# Typescript Sample


# Getting started
1. download the code from github
2. navigate to `samples\typescript directory` and run `npm install`
3. copy dummy.env to .env
4. update .env client_id and client_secret to the values given to you by your coordinator

### Assumptions
You already have [node js and npm installed](https://nodejs.org/en/download/).  These samples were tested
using node `18.12.1` and npm `8.19.2`.  

## Getting an authorization token example
This example shows how to retrieve an auth token with the credentials provided.  This example 
demostrates using `https://auth.staging.kiva.org/oauth/token` api.  
   
1. run `npm run auth`  
[Code](./src/demo/auth.ts)

## Using the authorization token example
This example shows how to use the auth token to make another API call, in this case getting a list of loans. This example 
uses the auth API (as above) as well as `https://partner-api-stage.kiva.org/v3/partner/{partnerId}/loans`.  
   
1. run `npm run get-loans`  
[Code](./src/demo/get.loans.ts)
