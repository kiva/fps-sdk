# Partner API Documentation Portal

This website is built using [Docusaurus 2](https://docusaurus.io/), a modern static website generator. It contains
information to help our partners integrate
with [Kiva's Partner API](https://partnerapi.production.kiva.org/swagger-ui/index.html#/partners).

## General Usage

### Installation

```
$ yarn
```

This command installs the necessary packages for building and running the site.

### Build

Run this command in the `fps-sdk/portal` directory:

```
$ yarn build
```

This will generate static content into the `fps-sdk/portal/build` directory and can be served using any static content
hosting service.

The build output will contain a directory for each locale present in [docusaurus.config.js](docusaurus.config.js):

* `build` - EN (default locale)
* `build/es` - ES (Spanish)
* `build/fr` - FR (French)

### Testing

We are using Firebase to host the portal. Firebase is a Google service for hosting static websites. These instructions
assume you have installed the [Firebase CLI](https://firebase.google.com/docs/cli).

The following sections assume you have run the [Install](#install) and [Build](#build) steps.

### Running the Firebase Emulator

Run the following command to start the emulator:

```
firebase emulator:start
```

The [firebase.json](../firebase.json) is set up, so this is all that is needed. The site is at http://127.0.0.1:5000/.
*Once you are satisfied with your work, please follow the steps for creating a Preview site, and link that in your pull
request!*

You can find the steps for setting up a project with the emulator
on [Google's documentation for testing a site locally](https://firebase.google.com/docs/hosting/test-preview-deploy).

### Publishing Changes (CI/CD)

The live site (https://fps-sdk-portal.web.app) is **not** published by running `firebase deploy` locally. It is
deployed automatically by GitHub Actions:

* [`firebase-hosting-pull-request.yml`](../.github/workflows/firebase-hosting-pull-request.yml) builds a preview
  deploy for every pull request.
* [`firebase-hosting-merge.yml`](../.github/workflows/firebase-hosting-merge.yml) builds and deploys to the `live`
  channel whenever a commit lands on `main`.

**All changes, including docs-only edits, must go through a branch and pull request.** `main` is protected and
requires an approving review before merge. Pushing directly to `main` bypasses that protection and has been
observed to *not* reliably trigger the merge-deploy workflow, so the change silently never goes live even though
it's sitting on `main`. Always:

1. Create a branch and open a PR against `main`.
2. Confirm the PR's preview-deploy check succeeds, and review the preview URL it produces.
3. Get it approved and merge — merging is what triggers the live deploy.

The manual `firebase deploy` steps below are for local testing (via the emulator) or as an emergency stopgap if the
GitHub Actions pipeline itself is broken — not the normal way to publish a change.

### Deploying to Firebase

The Firebase project is located at https://console.firebase.google.com/project/fps-sdk-portal/hosting/sites. If you
don't have access to this project, request it from one of the [CODEOWNERS](../CODEOWNERS).

Firebase access is linked to your Google account. Please log in using your google account:

```
firebase login
```

Run these Firebase commands:

```
firebase use --add
firebase target:apply hosting dec2022 fps-sdk-portal
firebase deploy
```
