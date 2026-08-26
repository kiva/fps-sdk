---
sidebar_position: 5
---

# Using the API loans fetch endpoint

## Process
The loan data stored in PA2 can be fetched via the API. See our Swagger documentation for more details: [GET /v3/partner/{id}/loans](https://partner-api-stage.kiva.org/swagger-ui/#/partners/loansRouteUsingGET)

There are 4 search request parameters you can send: `query`, `status`, `offset`, and `limit`.

**Query**: can be any string that would be typed into the search bar in PA2 (e.g. name, Loan ID, Client ID or Kiva ID)

**Status**: can be one of the following:
* `refunded`
* `inactive`
* `inactive_expired`
* `fundRaising`
* `expired`
* `raised`
* `payingBack`
* `ended`
* `reviewed`
* `issue`
* `defaulted`

To ensure that all payments are reported when using the "GET" option, include all loans with the status "PayingBack" and "defaulted". Loans in these statuses can receive payments and should be reported to Kiva.

**Offset, limit**: works the same as any pagination, offset is how far into the list to go and limit is how many results to return. So to get the first 20 matches it would be offset=0, limit=20 and the next 20 would be offset=20 limit=20, etc.. The default is offset=0 and limit=20.
We don’t recommend requesting more than 300, because the volume of data can become very large and constrain server resources
After making the request, the response from the API will be returned in JSON format.  _Please note_:  If you do not include `offset` and `limit`, the first 20 rows will be returned.

## Technical documentation
All of Kiva's technical documentation, including endpoints, can be found here:
Test environment (Stage): https://partnerapi.staging.kiva.org/swagger-ui/
Production (to use after testing): https://partnerapi.production.kiva.org/swagger-ui
