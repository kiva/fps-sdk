---
sidebar_position: 1
---

# Change Log

** Keep track of changes to the Kiva Partner API. **

This changelog lists all additions and updates to the Kiva Partner API, in chronological order.


## April 30, 2025
1. updated URLS to reflect changes in Kiva infrastructure

## Dec 16, 2024
1. Added details on report payment rate limiting.  
2. Added details on status fields for fetching loans. 
3. Added addiotnal information on posting group loan drafts.

## October 14, 2021
1. Additional documentation on scopes and 403 errors.
2. Internally updated documentation ordering

## September 30, 2022
1. Released updated documentation portal.

## September 26, 2022
1. Image Upload Improvements for Loan Drafting: The `loan_draft` endpoint now accepts an `image_encoded` field, and the `image_url` field has been **deprecated**.

### Additional Details

#### Image upload improvements for Loan Drafting
Historically, Kiva accepts borrower images in the `image_url` field. **In order to protected the privacy of our borrowers, we recommend our partners update their implementations to send the image through the `image_encoded` field**. To maintain compatibility, `image_url` will continue to be supported, but it will be removed from the official documentation.

This new field accepts a base64-encoded binary image string. Base64 encoding is a common way for transforming binary data into text so that it can be passed through a text-based protocol, i.e. HTTP with a content-type of `application/json`. You can find more documentation on this field on the Model section of our [Swagger UI page for drafting loans](https://partner-api.k1.kiva.org/swagger-ui/#/partners/loanDraftRouteUsingPOST).
