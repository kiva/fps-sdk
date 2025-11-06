import os
import requests
import json
import sys

# ---------------------------------------------------------------------------
# Data
# ---------------------------------------------------------------------------

limit = 20
PartnerId = ""
BearerToken = ""
AuthDomain = "auth.staging.kiva.org";                # URI only, no protocol, no path   
PartnerDomain = "partnerapi.staging.kiva.org";       #  same as above, no protocol, no path


# ---------------------------------------------------------------------------
# Functions
# ---------------------------------------------------------------------------

# Please see the auth sample for discussion of how the authorization is expected to work
def get_authorization_token():
    global PartnerId, BearerToken

    headers = {
        'Accept': 'application/json',
    }

    parameters = {
        'client_id': os.getenv('client_id'),
        'client_secret': os.getenv('client_secret'),
        'audience': os.getenv('audience'),
        'grant_type': 'client_credentials',
        'scope': os.getenv('scope')
    }

    response = requests.post(f'https://{AuthDomain}/oauth/token', data=parameters, headers=headers)

    if response.status_code == 200:
        kiva_authorization = response.json()
        
        # TODO: parse out auth token and partner id
        PartnerId = kiva_authorization['partnerId']
        BearerToken = kiva_authorization['access_token']

    else:
        print(f"error: {response.status_code}: {response.text}")
        sys.exit(1)

# ---------------------------------------------------------------------------
def get_loans():
    headers = {
        'Accept': 'application/json',
        'Authorization': f'Bearer {BearerToken}',
    }

    response = requests.get(f'https://{PartnerDomain}/v3/partner/{PartnerId}/loans?limit=20', headers=headers)
    
    if response.status_code == 200:
        json_response = response.json()
        print(f"\nGet Loans returned: \n {json.dumps(json_response, indent=2)}\n")
    else:
        print(f"error: {response.status_code}: {response.text}")

# ---------------------------------------------------------------------------
# Program execution
# ---------------------------------------------------------------------------

print("Kiva Partner API for listing loans")
print("    -- Step 1 Getting authorization token")
get_authorization_token()

print(f"    -- Step 2 listing the loans for the partner, with a limit of {limit} loans")
get_loans()

