# PHP Examples

## Getting started
1. Install [PHP](https://www.php.net/manual/en/install.php).  These examples were written using PHP 7.4, but should work with newer versions, as well.
2. `git clone` this repository.
3. Follow the instructions available in the following sections.


## Encoding text for Partner API transfer

`TextEncodingHelpers.php` contains a few helper functions that you can use to validate your text is properly 
encoded in `UTF-8`. It's important to get this right, as some characters (like the Latin character `ã`) won't
transfer properly unless the encoding is correct.

**When sending text values to the Partner API, please ensure that it is UTF-8 encoded.** This primarily impacts the
[loan_draft](https://partnerapi.production.kiva.org/swagger-ui/#/partners/loanDraftRouteUsingPOST) operation, which accepts
a `location` field that often has Latin characters.

### Using the helper functions

`TextEncodingHelpers.php` can be run directly via the command line: `php TextEncodingHelpers.php`

The functions we have implemented are simple wrappers around core PHP functions. We recommend you use the core 
PHP functions directly, or create a function similar to ours in your own code that suits the specific encodings 
you are working with.
