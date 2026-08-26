---
sidebar_position: 4
---

# Using the API to post draft loans

We recommend that your technical team consults with the Kiva Coordinator at your organization to fully understand Kiva's Repayment Reporting process. To do so, please have your technical team review the following:

* [This video](https://www.youtube.com/watch?v=9gScexv-yZo&amp;t=5s) provides a thorough explanation on how to post an individual loan
* [This video](https://www.youtube.com/watch?v=KvKUScWF73M&amp;t=1s) provides a thorough explanation on how to post a group loan
* Visit the New Loans section for more information, specifically [Step 1: Description - Individual Loans](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360030919632) 
* and [Step 1: Description - Group Loans](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360031260191)

### Process

* Information for a new loan is sent from MIS to PA2 via the API
* A draft profile is automatically created. Drafts can be viewed by clicking the “Drafts” link in the “Loans” box on the PA2 homepage.
* Each draft must be reviewed and then published by a person.

### Additional information

Before the loan can be published, PA2 will run all of the same validation checks that it runs on loans posted without the API. All error messages will need to be resolved before the loan can be published.

It is fine if the API sends information for some but not all fields required to post a loan. For example, not all MIS's store Kiva-specific fields such as the borrower description or sector. While these fields are required to publish a loan on PA2, they are not required to be sent via the API. The information for these fields can be added manually after the draft loan has been created in PA2. Note that the more information you can send via the API, the more time savings the Kiva Coordinator will have.

Depending on if you are posting a group or individual loan, certain fields are not required. If the loan you are posting is an individual loan, and you send data that is specific to a group loan, PA2 will give you an error message and you will be unable to publish the loan.

If posting an individual loan, do not include any of the following fields. **The following fields are required only when posting a group loan:**

* `group_name`: this is the name of the group that will appear on Kiva.org.
* `internal_client_id`: this is the ID of each client represented within the group (for example, if a group has three members, each member might have their own client ID that should be listed here)
* `internal_loan_id`: this is the ID for each individual borrower's loan (for example, a member of the group might be taking out their third loan with the organization, and that loan might have a unique ID. Enter that ID here)
* `not_pictured`: use this field in case any borrower is listed in Step 1: Description does not appear in the photo

At least one `entreps` object must be included in the JSON document when posting a group loan.


### not_pictured field

The `not_pictured` field is a list of true or false values that correspond to the order of the borrowers listed 
in [Step 1: Description - Group Loans](https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360031260191).

For example, if the first borrower listed in Step 1 is _not pictured_, 
the first value in the `not_pictured` list should be `true`. If the second borrower listed in Step 1 is 
pictured, the second value in the `not_pictured` list should be `false`. If the third borrower listed in Step 1
is _not pictured_, the third value in the `not_pictured` list should be `true`. And so on.

When providing values for the `not_pictured` field, values for all borrowers listed in Step 1 must be provided.

The borrower photo can be sent to PA2 as a base-64 encoded image. Repayment Schedules can be sent in several different formats. If you believe a format different from the one in the example below will work better for your organization, please let Kiva know and we will provide more information.

### Loan Draft Configurations

To create a fully-populated loan draft, there are several fields which must be populated with inputs that are defined in Kiva's system. Each of these identifiers can be retrieved through Kiva's `config` APIs. **Please note that these APIs are read-only**.

You can find example calls to these endpoints in our [Postman collection](https://github.com/kiva/fps-sdk/tree/main/samples/postman).

#### From PA2 - Step 1: Description
* **1**: `location` - This must be populated with a location that is currently defined in Kiva's system. Locations are managed in the Account -> Locations section in PA2.
  * [GET Partner Locations](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/locationConfigsRouteUsingGET)
* **2**: `activity_id` - This must be populated with the identifier of the Loan Activity.
  * [GET Loan Activities](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/activityConfigsRouteUsingGET)
* **3**: `description_language_id` - The ID of the language that the loan description is written in.
  * [GET Locales](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/localeConfigsRouteUsingGET)

When populated on the Loan Draft, these fields will show up in PA2 as follows:
![loandraft_step1.png](@site/static/img/pa2/loandraft_step1.png)

*Note that the Primary Sector field is set automatically from the provided Activity.*

#### From PA2 - Step 2: Loan Terms
* **4**: `theme_type_id` - This must be populated with the identifier of the Loan Theme. This is a broader classification of the loan than the Loan Activity that is relevant to the terms and conditions agreed upon by the partner.
  * [GET Loan Themes](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/themeConfigsRouteUsingGET)
* **5**: `currency` - The ISO currency code for the currency corresponding to the amount the borrower is requesting.
  * [GET Locales](https://partner-api.k1.kiva.org/swagger-ui/#/partner-configurations/localeConfigsRouteUsingGET)

When populated on the Loan Draft, these fields will show up in PA2 as follows:
![loandraft_step2.png](@site/static/img/pa2/loandraft_step2.png)

## Validation
To check if the JSON document you created is correct, you can use an online JSON validator like this one:  https://jsonlint.com/.

## Technical documentation
All of Kiva's technical documentation, including endpoints, can be found here:
* Test environment (Stage): https://partnerapi.staging.kiva.org/swagger-ui/
* Production (to use after testing): https://partnerapi.production.kiva.org/swagger-ui