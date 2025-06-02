---
sidebar_position: 7
---

# Using the API to post journals


We recommend that your technical team consults with the Kiva Coordinator at your organization to fully understand Kiva's Journaling process. Additionally, please have your technical team review the following:

[![What is a journal?](https://img.youtube.com/vi/irTYtMImrAw/0.jpg)](https://www.youtube.com/watch?v=irTYtMImrAw)

Visit the [Journals](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/categories/360001945772-Journals) section for more information.

## Process

1. Information for a new journal is sent from MIS to PA2 via the API
2. The API will return a JSON response with a `confirm_url`
3. A person needs to go to the `confirm_url` and actually publish the journal draft

## Additional information

For partners who use the two column format for repayment reporting and journaling, only the Loan ID (`internal_loan_id`) is needed. For partners that use the three column format, both the Loan ID (`internal_loan_id`) and Client ID (`internal_client_id`) are needed. *This can be checked by going to _PA2 -> Account -> Profile -> CSV upload format*. 

When testing the API connection for journals, please use information of a real client/borrower that has been posted to Kiva. Do not use the same client information used to post a test loan draft as PA2 will not register this borrower as eligible for a journal update.
* To find an existing Kiva borrower, click on the “Journals” tab in PA2. This will take you to a report of all Kiva clients who are eligible for a journal. Select any of the client and loan IDs for these clients to send via the API.
The content of the journal can be whatever you'd like it to be. 
* The Kiva Coordinator is still required to log into PA2 to review journals before submitting them.

## Technical documentation
All of Kiva's technical documentation, including endpoints, can be found here:
* Test environment (Stage): https://partnerapi.staging.kiva.org/swagger-ui/
* Production (to use after testing): https://partnerapi.production.kiva.org/swagger-ui
