import * as dotenv from 'dotenv';

const log = (text: string, data?: any) => {
    console.log(text, data || '');
};

let partnerId = '';
let authToken = '';

// this method is the same as in the auth example
// please see that example for more comments
const gethAuthToken = async () => {
    dotenv.config();
    log(`getting autho token using client_id '${process.env.client_id}'`);
    log(`\twith secret '${process.env.client_secret}'`);

    const uri = 'https://auth-stage.kiva.org/oauth/token';
    const details = {
        grant_type: 'client_credentials',
        scope: 'create:loan_draft read:loans',
        audience: 'https://partner-api-stage.kiva.org',
        client_id: process.env.client_id,
        client_secret: process.env.client_secret,
    };

    const headers = {
        Accept: 'application/json',
        'Content-Type': 'application/x-www-form-urlencoded',
    };

    const formBody = [];
    for (const property in details) {
        const encodedKey = encodeURIComponent(property);
        // @ts-ignore
        const encodedValue = encodeURIComponent(details[property]);
        formBody.push(encodedKey + '=' + encodedValue);
    }
    const body = formBody.join('&');
    const response = await fetch(uri, {
        method: 'POST',
        headers,
        body,
    });

    const data = await response.json();
    partnerId = data.partnerId;
    authToken = data.access_token;
};

const getLoans = async () => {
    log(`\r\n\r\nGET LOANS EXAMPLE\r\n\r\n`);

    // get the auth token since that will be needed for the next call
    await gethAuthToken();

    const uri = `https://partner-api-stage.kiva.org/v3/partner/${partnerId}/loans`;
    log(`calling ${uri}`);

    // creating the headers.  authToken value is retrieved from the function
    // gethAuthToken above.  authToken ensures you have permissions and rights to make
    // the api call below
    const headers = {
        Accept: 'application/json',
        Authorization: `Bearer ${authToken}`,
    };

    // now make the get loans call sending only required fields and leaving all optional
    // fields blank.
    // this is documented here:
    // https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360051230151-Using-the-API-loans-fetch-endpoint
    const response = await fetch(uri, {
        method: 'GET',
        headers,
    });

    // the data needs to be read from the stream
    const data = await response.json();

    // print out the results of the call.  if no loans are posted
    // the output will be minimal
    log(`v3/loans received`, data);
};

// this code will get executed by running the command
// npm run get-loans from the typescript directory
getLoans()
    .then(() => {
        // this line executes if the api calls succeed
        log(`GET loans api call completed successfully`);
    })
    .catch((e: unknown) => {
        // errors will be output to the console here
        if (e instanceof Error) {
            log(`error: ${e.message}`);
        } else {
            log(`error, but not understood`, e);
        }
    });
