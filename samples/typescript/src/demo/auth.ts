import * as dotenv from 'dotenv';

const log = (text: string, data?: any) => {
    console.log(text, data || '');
};

const gethAuthToken = async () => {
    dotenv.config();
    log(`\r\n\r\nAUTH TOKEN EXAMPLE\r\n\r\n`);
    log(`getting autho token using client_id '${process.env.client_id}'`);
    log(`\twith secret '${process.env.client_secret}'`);

    // URI to the staging system
    const uri: string = 'https://auth.staging.kiva.org/oauth/token';

    // details are documented in
    // https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360051231131-API-authentication-client-credential-flow-
    // expectation with this example is that client_id and client_secret were set in .env file
    const details = {
        grant_type: 'client_credentials',
        scope: 'create:loan_draft read:loans',
        audience: 'https://partner-api-stage.kiva.org',
        client_id: process.env.client_id,
        client_secret: process.env.client_secret,
    };

    // setup headers per documentation
    const headers =  {
            Accept: 'application/json',
            'Content-Type': 'application/x-www-form-urlencoded',
        };


    // since the API expects the details to be posted as x-www-form-urlencoded
    // we have to properly encode each value
    const encodedFormData = [];
    for (const property in details) {
        const encodedKey = encodeURIComponent(property);
        // @ts-ignore
        const encodedValue = encodeURIComponent(details[property]);
        encodedFormData.push(encodedKey + '=' + encodedValue);
    }

    // finally turn the form encoded values into string
    const body = encodedFormData.join("&");

    // make the call
    const response = await fetch(uri, {
        method: 'POST',
        headers,
        body
    });

    // if there are errors, they get caugth in the caller catch handler
    const data = await response.json();

    // print out the results
    log(`access_token:`, data.access_token);
    log(`token_type:`, data.token_type);
    log(`partnerId`, data.partnerId);
};

gethAuthToken().then(() => {
    log(`POST token call successful`);
}).catch((e: unknown) => {
    if (e instanceof Error) {
        log(`error: ${e.message}`);
    } else {
        log(`error, but not understood`, e);
    }
});
